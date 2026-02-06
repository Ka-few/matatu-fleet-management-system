package com.matatufleet.app.ui.dashboard

import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

@Composable
fun DashboardScreen(
    onStartShift: () -> Unit,
    onRecordCollection: () -> Unit,
    onRecordExpense: () -> Unit,
    onEndShift: () -> Unit
) {
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text(
            text = "Matatu Operations",
            style = MaterialTheme.typography.headlineMedium,
            color = MaterialTheme.colorScheme.primary
        )

        Spacer(modifier = Modifier.height(32.dp))

        // Shift Status Card
        Card(
            modifier = Modifier.fillMaxWidth().padding(8.dp),
            colors = CardDefaults.cardColors(containerColor = MaterialTheme.colorScheme.surfaceVariant)
        ) {
            Column(modifier = Modifier.padding(16.dp)) {
                Text(text = "Current Shift: Active", style = MaterialTheme.typography.titleMedium)
                Text(text = "Vehicle: KBA 123A", style = MaterialTheme.typography.bodyMedium)
                Text(text = "Started: 06:00 AM", style = MaterialTheme.typography.bodySmall)
            }
        }

        Spacer(modifier = Modifier.height(24.dp))

        // Action Buttons
        Button(
            onClick = onRecordCollection,
            modifier = Modifier.fillMaxWidth().height(60.dp)
        ) {
            Text("Record Fare Collection")
        }

        Spacer(modifier = Modifier.height(16.dp))

        Button(
            onClick = onRecordExpense,
            modifier = Modifier.fillMaxWidth().height(60.dp),
            colors = ButtonDefaults.buttonColors(containerColor = MaterialTheme.colorScheme.secondary)
        ) {
            Text("Record Expense")
        }

        Spacer(modifier = Modifier.height(32.dp))

        OutlinedButton(
            onClick = onEndShift,
            modifier = Modifier.fillMaxWidth()
        ) {
            Text("End Shift")
        }
    }
}
