package com.matatufleet.app.data.remote

import retrofit2.http.Body
import retrofit2.http.POST

interface ApiService {
    @POST("auth/login")
    suspend fun login(@Body request: LoginRequest): AuthResponse

    @POST("auth/register")
    suspend fun register(@Body request: RegisterRequest): AuthResponse
    
    // TODO: Add other endpoints (Vehicles, Shifts, Collections)
}

data class LoginRequest(val phoneNumber: String, val pin: String)
data class RegisterRequest(val fullName: String, val phoneNumber: String, val pin: String)
data class AuthResponse(val token: String, val fullName: String, val role: String)
