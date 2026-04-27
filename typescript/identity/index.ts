export interface IdentityConfig {
  baseUrl: string;
  clientId: string;
  clientSecret: string;
  scopes: string[];
}

export class IdentityClient {
  private token: string | null = null;
  private expiresAt = 0;
  private keyPair: CryptoKeyPair | null = null;

  constructor(private config: IdentityConfig) {}

  private async getKeyPair(): Promise<CryptoKeyPair> {
    if (this.keyPair) return this.keyPair;
    this.keyPair = await crypto.subtle.generateKey(
      { name: 'ECDSA', namedCurve: 'P-256' },
      false,
      ['sign']
    );
    return this.keyPair;
  }

  private async createDPoPProof(method: string, url: string): Promise<string> {
    const keyPair = await this.getKeyPair();
    const publicJwk = await crypto.subtle.exportKey('jwk', keyPair.publicKey);
    
    const header = {
      typ: 'dpop+jwt',
      alg: 'ES256',
      jwk: publicJwk
    };

    const payload = {
      jti: Math.random().toString(36).substring(2),
      htm: method.toUpperCase(),
      htu: url,
      iat: Math.floor(Date.now() / 1000)
    };

    const encodedHeader = btoa(JSON.stringify(header)).replace(/=/g, '').replace(/\+/g, '-').replace(/\//g, '_');
    const encodedPayload = btoa(JSON.stringify(payload)).replace(/=/g, '').replace(/\+/g, '-').replace(/\//g, '_');
    
    const signature = await crypto.subtle.sign(
      { name: 'ECDSA', hash: { name: 'SHA-256' } },
      keyPair.privateKey,
      new TextEncoder().encode(`${encodedHeader}.${encodedPayload}`)
    );

    const encodedSignature = btoa(String.fromCharCode(...new Uint8Array(signature)))
      .replace(/=/g, '').replace(/\+/g, '-').replace(/\//g, '_');

    return `${encodedHeader}.${encodedPayload}.${encodedSignature}`;
  }

  static getDeviceFingerprint(): string {
    // Simple browser/node fingerprint
    const components = [
      typeof navigator !== 'undefined' ? navigator.userAgent : 'node',
      typeof screen !== 'undefined' ? `${screen.width}x${screen.height}` : 'server',
      new Date().getTimezoneOffset().toString()
    ];
    return btoa(components.join('|')).substring(0, 16);
  }

  async getToken(): Promise<string> {
    if (this.token && Date.now() < this.expiresAt) {
      return this.token;
    }

    const res = await fetch(`${this.config.baseUrl}/connect/token`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
      body: new URLSearchParams({
        grant_type: 'client_credentials',
        client_id: this.config.clientId,
        client_secret: this.config.clientSecret,
        scope: this.config.scopes.join(' ')
      })
    });

    if (!res.ok) {
      throw new Error(`Failed to fetch token: ${res.statusText}`);
    }

    const json = await res.json();

    this.token = json.access_token;
    this.expiresAt = Date.now() + (json.expires_in - 60) * 1000;

    return this.token!;
  }

  async getAuthHeaders(method: string, url: string): Promise<Record<string, string>> {
    const token = await this.getToken();
    const headers: Record<string, string> = {
      'Authorization': `Bearer ${token}`,
      'X-SAE-Device-Fingerprint': IdentityClient.getDeviceFingerprint(),
      'X-SAE-Timestamp': Math.floor(Date.now() / 1000).toString()
    };

    try {
      headers['DPoP'] = await this.createDPoPProof(method, url);
    } catch (e) {
      console.warn('Failed to generate DPoP proof', e);
    }

    return headers;
  }
}
