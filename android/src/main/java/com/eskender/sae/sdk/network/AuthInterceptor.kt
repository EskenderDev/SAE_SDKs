package com.eskender.sae.sdk.network

import okhttp3.Interceptor
import okhttp3.Response

class AuthInterceptor : Interceptor {
    private var token: String? = null
    private var apiKey: String? = null
    private var terminalKey: String? = null

    fun setToken(newToken: String) {
        token = newToken
        apiKey = null
    }

    fun setApiKey(newApiKey: String) {
        apiKey = newApiKey
        token = null
    }

    fun setTerminalKey(newTerminalKey: String?) {
        terminalKey = newTerminalKey
    }

    override fun intercept(chain: Interceptor.Chain): Response {
        val original = chain.request()
        val builder = original.newBuilder()

        token?.let {
            builder.header("Authorization", "Bearer $it")
        }

        apiKey?.let {
            // Remove existing if any, though usually not needed if we control it
            builder.removeHeader("X-API-KEY")
            builder.header("X-API-KEY", it)
        }

        terminalKey?.let {
            builder.removeHeader("X-Terminal-Key")
            builder.header("X-Terminal-Key", it)
        }

        return chain.proceed(builder.build())
    }
}
