#!/bin/bash

BASE_URL="http://localhost:5179"

echo "1. Registering User..."
REGISTER_RESPONSE=$(curl -s -X POST "$BASE_URL/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "John Doe",
    "phoneNumber": "0712345678",
    "pin": "1234"
  }')
echo "Register Response: $REGISTER_RESPONSE"

echo -e "\n2. Logging in..."
LOGIN_RESPONSE=$(curl -s -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "phoneNumber": "0712345678",
    "pin": "1234"
  }')
echo "Login Response: $LOGIN_RESPONSE"

TOKEN=$(echo $LOGIN_RESPONSE | jq -r '.token')
USER_ID=$(echo $LOGIN_RESPONSE | jq -r '.id')
echo "Token: $TOKEN"
echo "User ID: $USER_ID"

if [ "$TOKEN" == "null" ] || [ -z "$TOKEN" ]; then
  echo "Login failed, cannot proceed."
  exit 1
fi

echo -e "\n3. Creating Vehicle..."
VEHICLE_RESPONSE=$(curl -s -X POST "$BASE_URL/api/vehicles" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d "{
    \"registrationNumber\": \"KBA 123A\",
    \"type\": \"Matatu\",
    \"ownerId\": \"$USER_ID\"
  }")
echo "Vehicle Response: $VEHICLE_RESPONSE"
VEHICLE_ID=$(echo $VEHICLE_RESPONSE | jq -r '.id')
echo "Vehicle ID: $VEHICLE_ID"

if [ "$VEHICLE_ID" == "null" ] || [ -z "$VEHICLE_ID" ]; then
  echo "Vehicle creation failed."
  exit 1
fi

echo -e "\n4. Starting Shift..."
SHIFT_RESPONSE=$(curl -s -X POST "$BASE_URL/api/shifts/start" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d "{
    \"vehicleId\": \"$VEHICLE_ID\",
    \"driverId\": \"00000000-0000-0000-0000-000000000000\"
  }")
echo "Shift Response: $SHIFT_RESPONSE"
SHIFT_ID=$(echo $SHIFT_RESPONSE | jq -r '.id')
echo "Shift ID: $SHIFT_ID"

if [ "$SHIFT_ID" == "null" ] || [ -z "$SHIFT_ID" ]; then
  echo "Shift start failed."
  exit 1
fi

echo -e "\n5. Recording Collection..."
COLLECTION_RESPONSE=$(curl -s -X POST "$BASE_URL/api/collections" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d "{
    \"shiftId\": \"$SHIFT_ID\",
    \"amount\": 500,
    \"paymentMethod\": \"Cash\"
  }")
echo "Collection Response: $COLLECTION_RESPONSE"
