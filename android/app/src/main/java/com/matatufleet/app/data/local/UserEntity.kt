package com.matatufleet.app.data.local

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "users")
data class UserEntity(
    @PrimaryKey val id: String,
    val fullName: String,
    val phoneNumber: String,
    val role: String, // Driver, Conductor, Owner
    val token: String? // Store JWT locally for offline auth check
)
