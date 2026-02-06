package com.matatufleet.app.data.repository

import com.matatufleet.app.data.local.UserDao
import com.matatufleet.app.data.local.UserEntity
import com.matatufleet.app.data.remote.ApiService
import com.matatufleet.app.data.remote.LoginRequest
import com.matatufleet.app.data.remote.RegisterRequest
import javax.inject.Inject
import javax.inject.Singleton
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext

@Singleton
class AuthRepository @Inject constructor(
    private val apiService: ApiService,
    private val userDao: UserDao
) {

    suspend fun login(phoneNumber: String, pin: String): Result<UserEntity> {
        return withContext(Dispatchers.IO) {
            try {
                val response = apiService.login(LoginRequest(phoneNumber, pin))
                val user = UserEntity(
                    id = "temp-id", // JWT usually contains ID, but for MVP we might need to decode it or return it from API
                    fullName = response.fullName,
                    phoneNumber = phoneNumber,
                    role = response.role,
                    token = response.token
                )
                userDao.insertUser(user)
                Result.success(user)
            } catch (e: Exception) {
                Result.failure(e)
            }
        }
    }

    suspend fun register(fullName: String, phoneNumber: String, pin: String): Result<UserEntity> {
        return withContext(Dispatchers.IO) {
            try {
                val response = apiService.register(RegisterRequest(fullName, phoneNumber, pin))
                val user = UserEntity(
                    id = "temp-id",
                    fullName = response.fullName,
                    phoneNumber = phoneNumber,
                    role = response.role,
                    token = response.token
                )
                userDao.insertUser(user)
                Result.success(user)
            } catch (e: Exception) {
                Result.failure(e)
            }
        }
    }
    
    suspend fun getCurrentUser(): UserEntity? {
        return withContext(Dispatchers.IO) {
            userDao.getCurrentUser()
        }
    }

    suspend fun logout() {
        withContext(Dispatchers.IO) {
            userDao.clearUser()
        }
    }
}
