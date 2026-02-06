package com.matatufleet.app.data.local

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "shifts")
data class ShiftEntity(
    @PrimaryKey val id: String,
    val vehicleId: String,
    val driverId: String,
    val startTime: Long,
    val endTime: Long?,
    val status: String // Active, Closed
)

@Entity(tableName = "collections")
data class CollectionEntity(
    @PrimaryKey val id: String,
    val shiftId: String,
    val amount: Double,
    val paymentMethod: String,
    val timestamp: Long
)
