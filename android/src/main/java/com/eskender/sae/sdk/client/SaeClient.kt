package com.eskender.sae.sdk.client

import com.eskender.sae.sdk.models.*
import com.eskender.sae.sdk.network.AuthInterceptor
import com.eskender.sae.sdk.network.SaeApiService
import com.microsoft.signalr.HubConnection
import com.microsoft.signalr.HubConnectionBuilder
import com.microsoft.signalr.HubConnectionState
import com.squareup.moshi.Moshi
import com.squareup.moshi.kotlin.reflect.KotlinJsonAdapterFactory
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.moshi.MoshiConverterFactory
import java.util.concurrent.TimeUnit
import kotlinx.coroutines.flow.MutableSharedFlow
import kotlinx.coroutines.flow.asSharedFlow
import io.reactivex.rxjava3.core.Single
// Microsoft SignalR Java client uses RxJava for some parts but mostly standard Java callbacks.

class SaeClient(
    baseUrl: String = "https://localhost:5000",
    client: OkHttpClient? = null
) {
    private val _baseUrl = baseUrl.trimEnd('/')
    private val _hubUrl = "$_baseUrl/hubs/hacienda" // Verify hub path

    private val authInterceptor = AuthInterceptor()

    private val okHttpClient = client?.newBuilder()
        ?.addInterceptor(authInterceptor)
        ?.build()
        ?: OkHttpClient.Builder()
            .addInterceptor(authInterceptor)
            .addInterceptor(HttpLoggingInterceptor().apply { level = HttpLoggingInterceptor.Level.BODY })
            .readTimeout(60, TimeUnit.SECONDS)
            .connectTimeout(60, TimeUnit.SECONDS)
            .build()

    private val moshi = Moshi.Builder()
        .add(KotlinJsonAdapterFactory())
        .build()

    private val retrofit = Retrofit.Builder()
        .baseUrl(if (_baseUrl.endsWith("/api")) "$_baseUrl/" else "$_baseUrl/api/")
        .client(okHttpClient)
        .addConverterFactory(MoshiConverterFactory.create(moshi))
        .build()

    private val api = retrofit.create(SaeApiService::class.java)

    // SignalR
    private var hubConnection: HubConnection? = null
    private var _token: String? = null
    private var _apiKey: String? = null
    private var _tenantId: String? = null

    // Events (using Flows for Kotlin idiom)
    private val _onEnvioIniciado = MutableSharedFlow<HaciendaEnvioResult>() // Using Result for now, check event type
    val onEnvioIniciado = _onEnvioIniciado.asSharedFlow()

    private val _onRespuestaRecibida = MutableSharedFlow<HaciendaEnvioResult>()
    val onRespuestaRecibida = _onRespuestaRecibida.asSharedFlow()

    fun setToken(token: String) {
        _token = token
        authInterceptor.setToken(token)
    }

    fun setApiKey(apiKey: String) {
        _apiKey = apiKey
        authInterceptor.setApiKey(apiKey)
    }

    fun setTenantId(tenantId: String) {
        _tenantId = tenantId
    }

    fun setTerminalKey(terminalKey: String?) {
        authInterceptor.setTerminalKey(terminalKey)
    }

    suspend fun login(email: String, password: String): String {
        val response = api.login(mapOf("email" to email, "password" to password))
        if (response.isSuccessful && response.body() != null) {
            val token = response.body()!!.token
            setToken(token)
            return token
        } else {
            throw Exception("Login failed: ${response.code()} ${response.errorBody()?.string()}")
        }
    }
    
    suspend fun adminLogin(email: String, password: String): String {
         val response = api.adminLogin(mapOf("email" to email, "password" to password))
        if (response.isSuccessful && response.body() != null) {
            val token = response.body()!!.token
            setToken(token)
            return token
        } else {
            throw Exception("Admin Login failed: ${response.code()} ${response.errorBody()?.string()}")
        }
    }

    suspend fun generarDocumento(request: GenerarDocumentoRequest): DocumentoResult {
        val response = api.generarDocumento(request)
        if (response.isSuccessful && response.body() != null) {
            return response.body()!!
        } else {
             throw Exception("Error generando documento: ${response.code()} ${response.errorBody()?.string()}")
        }
    }

    suspend fun enviarDocumento(request: GenerarDocumentoRequest): HaciendaEnvioResult {
        val response = api.enviarDocumento(request)
        if (response.isSuccessful && response.body() != null) {
            return response.body()!!
        } else {
            throw Exception("Error enviando documento: ${response.code()} ${response.errorBody()?.string()}")
        }
    }

    suspend fun emitir(request: EnviarDocumentoSimplificadoRequest): HaciendaEnvioResult {
        val response = api.emitir(request)
        if (response.isSuccessful && response.body() != null) {
            return response.body()!!
        } else {
            throw Exception("Error emitiendo documento: ${response.code()} ${response.errorBody()?.string()}")
        }
    }

    suspend fun consultarEstado(clave: String): HaciendaEnvioResult {
         val response = api.consultarEstado(clave)
        if (response.isSuccessful && response.body() != null) {
            return response.body()!!
        } else {
            throw Exception("Error consultando estado: ${response.code()} ${response.errorBody()?.string()}")
        }
    }

    suspend fun endosarDocumento(request: MensajeEndosoRequest): DocumentoResult {
        val response = api.endosarDocumento(request)
        if (response.isSuccessful && response.body() != null) {
            return response.body()!!
        } else {
            throw Exception("Error endosando documento: ${response.code()} ${response.errorBody()?.string()}")
        }
    }

    suspend fun confirmarDocumento(request: MensajeEndosoRequest): DocumentoResult {
        val response = api.confirmarDocumento(request)
        if (response.isSuccessful && response.body() != null) {
            return response.body()!!
        } else {
            throw Exception("Error confirmando documento: ${response.code()} ${response.errorBody()?.string()}")
        }
    }

    // SignalR Methods
    fun startRealtime() {
        if (hubConnection?.connectionState == HubConnectionState.CONNECTED) return

        var fullUrl = _hubUrl
        val queryParams = mutableListOf<String>()
        _tenantId?.let { queryParams.add("tenantId=$it") }
        _apiKey?.let { queryParams.add("apiKey=$it") }

        if (queryParams.isNotEmpty()) {
            fullUrl += "?" + queryParams.joinToString("&")
        }
        
        hubConnection = HubConnectionBuilder.create(fullUrl)
            .withAccessTokenProvider(Single.defer { Single.just(_token ?: "") }) 
            // Note: SignalR Java client might handle headers differently. 
            // Ideally we pass headers in options, but the Java client is limited. 
            // AccessTokenProvider is the standard way for Bearer tokens.
            .build()
            
         // Listeners
         // Note: We need to match the object types sent by the server. 
         // Assuming server sends similar objects to HaciendaEnvioResult for events.
         // If "HaciendaEnvioIniciadoEvent" is a specific class, we need to map it.
         // For now using HaciendaEnvioResult as placeholder or Generic map.
         
         hubConnection?.on("HaciendaEnvioIniciado", { data: HaciendaEnvioResult ->
             // Emit to flow
             // Since this is a callback, we need a scope to emit. 
             // Ideally we shouldn't block.
             // For simplicity in this non-suspend context, we might expose a listener interface 
             // or use a GlobalScope (discouraged) or just log for now.
             // Better approach: Expose a proper callback interface like the C# SDK.
         }, HaciendaEnvioResult::class.java)

         hubConnection?.start()?.blockingAwait() // basic start
    }
    
    fun stopRealtime() {
        hubConnection?.stop()
    }
}
