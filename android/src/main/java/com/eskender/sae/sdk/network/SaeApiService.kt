package com.eskender.sae.sdk.network

import com.eskender.sae.sdk.models.*
import retrofit2.Response
import retrofit2.http.*

interface SaeApiService {

    // Auth
    @POST("auth/login")
    suspend fun login(@Body body: Map<String, String>): Response<LoginResult>

    @POST("admin/auth/login")
    suspend fun adminLogin(@Body body: Map<String, String>): Response<LoginResult>

    // Documents
    @POST("documentos/generar")
    suspend fun generarDocumento(@Body documento: GenerarDocumentoRequest): Response<DocumentoResult>

    @POST("v1/hacienda/enviar")
    suspend fun enviarDocumento(@Body documento: GenerarDocumentoRequest): Response<HaciendaEnvioResult>

    @GET("v1/hacienda/estado/{clave}")
    suspend fun consultarEstado(@Path("clave") clave: String): Response<HaciendaEnvioResult>

    @POST("v1/hacienda/emitir")
    suspend fun emitir(@Body request: EnviarDocumentoSimplificadoRequest): Response<HaciendaEnvioResult>

    @POST("v1/hacienda/endosar")
    suspend fun endosarDocumento(@Body request: MensajeEndosoRequest): Response<DocumentoResult>

    @POST("v1/hacienda/aceptar")
    suspend fun confirmarDocumento(@Body request: MensajeEndosoRequest): Response<DocumentoResult>

    // Management
    @POST("tenants/invite")
    suspend fun inviteUser(@Body request: InviteUserRequest): Response<Unit>

    // Hacienda Config
    @GET("hacienda/config")
    suspend fun getHaciendaConfig(): Response<HaciendaConfigResponse>

    @PUT("hacienda/config")
    suspend fun saveHaciendaConfig(@Body config: HaciendaConfigRequest): Response<HaciendaConfigResponse>

    @GET("hacienda/config/environments")
    suspend fun getHaciendaEnvironments(): Response<List<EnvironmentInfoResponse>>

    @GET("hacienda/config/environment/active")
    suspend fun getActiveHaciendaEnvironment(): Response<EnvironmentInfoResponse>

    @POST("hacienda/config/environment/switch")
    suspend fun switchHaciendaEnvironment(@Body body: Map<String, String>): Response<EnvironmentInfoResponse>
}
