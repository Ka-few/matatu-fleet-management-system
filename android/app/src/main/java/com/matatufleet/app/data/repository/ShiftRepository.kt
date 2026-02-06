package com.matatufleet.app.data.repository

import com.matatufleet.app.data.local.CollectionEntity
import com.matatufleet.app.data.local.ShiftDao
import com.matatufleet.app.data.local.ShiftEntity
import javax.inject.Inject
import javax.inject.Singleton
import kotlinx.coroutines.flow.Flow
import java.util.UUID

@Singleton
class ShiftRepository @Inject constructor(
    private val shiftDao: ShiftDao
    // private val workManager: WorkManager // For sync later
) {
    val activeShift: Flow<ShiftEntity?> = shiftDao.getActiveShift()

    suspend fun startShift(vehicleId: String, driverId: String) {
        val shift = ShiftEntity(
            id = UUID.randomUUID().toString(),
            vehicleId = vehicleId,
            driverId = driverId,
            startTime = System.currentTimeMillis(),
            endTime = null,
            status = "Active"
        )
        shiftDao.insertShift(shift)
        // TODO: Schedule sync
    }

    suspend fun recordCollection(shiftId: String, amount: Double, method: String) {
        val collection = CollectionEntity(
            id = UUID.randomUUID().toString(),
            shiftId = shiftId,
            amount = amount,
            paymentMethod = method,
            timestamp = System.currentTimeMillis()
        )
        shiftDao.insertCollection(collection)
        // TODO: Schedule sync
    }

    // TODO: End Shift, Record Expense
}
