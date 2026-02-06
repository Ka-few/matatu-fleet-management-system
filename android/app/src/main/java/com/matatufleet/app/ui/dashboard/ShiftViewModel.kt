package com.matatufleet.app.ui.dashboard

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.matatufleet.app.data.repository.ShiftRepository
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch

@HiltViewModel
class ShiftViewModel @Inject constructor(
    private val repository: ShiftRepository
) : ViewModel() {

    // Expose active shift state to UI
    val activeShift = repository.activeShift.stateIn(
        viewModelScope,
        SharingStarted.WhileSubscribed(5000),
        null
    )

    fun startShift(vehicleId: String, driverId: String) {
        viewModelScope.launch {
            repository.startShift(vehicleId, driverId)
        }
    }

    fun recordCollection(amount: Double) {
        val shift = activeShift.value ?: return
        viewModelScope.launch {
            repository.recordCollection(shift.id, amount, "Cash")
        }
    }
}
