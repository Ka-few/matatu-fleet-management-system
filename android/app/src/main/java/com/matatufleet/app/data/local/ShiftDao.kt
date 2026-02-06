package com.matatufleet.app.data.local

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import kotlinx.coroutines.flow.Flow

@Dao
interface ShiftDao {
    @Query("SELECT * FROM shifts WHERE status = 'Active' LIMIT 1")
    fun getActiveShift(): Flow<ShiftEntity?>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertShift(shift: ShiftEntity)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertCollection(collection: CollectionEntity)
    
    @Query("SELECT * FROM collections WHERE shiftId = :shiftId")
    fun getCollectionsForShift(shiftId: String): Flow<List<CollectionEntity>>
}
