package com.eskender.sae.sdk.models

import com.squareup.moshi.Json
import com.squareup.moshi.JsonClass
import java.math.BigDecimal

@JsonClass(generateAdapter = true)
data class LoginResult(
    @Json(name = "token") val token: String = ""
)

@JsonClass(generateAdapter = true)
data class DocumentoResult(
    @Json(name = "xmlFirmado") val xmlFirmado: String = "",
    @Json(name = "clave") val clave: String = ""
)

@JsonClass(generateAdapter = true)
data class HaciendaEnvioResult(
    @Json(name = "id") val id: String, // Guid as String
    @Json(name = "clave") val clave: String,
    @Json(name = "tipoDocumento") val tipoDocumento: String,
    @Json(name = "estado") val estado: Int, // Enum mapping needed
    @Json(name = "mensajeEstado") val mensajeEstado: String?,
    @Json(name = "httpStatus") val httpStatus: Int?,
    @Json(name = "responseJson") val responseJson: String?
)

@JsonClass(generateAdapter = true)
data class HaciendaConfigResponse(
    @Json(name = "isConfigured") val isConfigured: Boolean,
    @Json(name = "isActive") val isActive: Boolean? = null,
    @Json(name = "enabled") val enabled: Boolean? = null,
    @Json(name = "idCia") val idCia: String?,
    @Json(name = "environment") val environment: String,
    @Json(name = "baseUrl") val baseUrl: String? = null,
    @Json(name = "clientId") val clientId: String? = null,
    @Json(name = "idpUsername") val idpUsername: String? = null,
    @Json(name = "hasCertificate") val hasCertificate: Boolean? = null,
    @Json(name = "certFileName") val certFileName: String? = null,
    // Ubicación
    @Json(name = "provinceCode") val provinceCode: String? = null,
    @Json(name = "cantonCode") val cantonCode: String? = null,
    @Json(name = "districtCode") val districtCode: String? = null,
    @Json(name = "addressDetails") val addressDetails: String? = null,
    @Json(name = "defaultCurrency") val defaultCurrency: String? = null,
    @Json(name = "defaultCurrencyRate") val defaultCurrencyRate: BigDecimal? = null,
    @Json(name = "economicActivityCode") val economicActivityCode: String? = null,
    @Json(name = "updatedAt") val updatedAt: String?
)

@JsonClass(generateAdapter = true)
data class EnvironmentInfoResponse(
    @Json(name = "environment") val environment: String,
    @Json(name = "environmentName") val environmentName: String,
    @Json(name = "isConfigured") val isConfigured: Boolean,
    @Json(name = "isActive") val isActive: Boolean,
    @Json(name = "baseUrl") val baseUrl: String
)

@JsonClass(generateAdapter = true)
data class PasswordResetResponse(
    @Json(name = "success") val success: Boolean,
    @Json(name = "message") val message: String
)

@JsonClass(generateAdapter = true)
data class LicenseResponse(
    @Json(name = "id") val id: String,
    @Json(name = "tenantId") val tenantId: String,
    @Json(name = "licenseKey") val licenseKey: String,
    @Json(name = "machineId") val machineId: String? = null,
    @Json(name = "type") val type: String, // Or use Enum if mapped
    @Json(name = "platform") val platform: String,
    @Json(name = "status") val status: String,
    @Json(name = "expirationDate") val expirationDate: String? = null,
    @Json(name = "lastCheckIn") val lastCheckIn: String? = null,
    @Json(name = "appVersion") val appVersion: String? = null,
    @Json(name = "lastIpAddress") val lastIpAddress: String? = null,
    @Json(name = "deviceName") val deviceName: String? = null,
    @Json(name = "appIdentifier") val appIdentifier: String? = null,
    @Json(name = "appName") val appName: String? = null,
    @Json(name = "offlineToken") val offlineToken: String? = null,
    @Json(name = "terminalId") val terminalId: String? = null,
    @Json(name = "terminalName") val terminalName: String? = null,
    @Json(name = "terminalCode") val terminalCode: String? = null,
    @Json(name = "branchCode") val branchCode: String? = null,
    @Json(name = "modules") val modules: List<String>? = null
)
