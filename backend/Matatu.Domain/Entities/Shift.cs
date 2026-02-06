using System;
using Matatu.Domain.Enums;

namespace Matatu.Domain.Entities
{
    public class Shift
    {
        public Guid Id { get; set; }
        
        public Guid VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        
        public Guid RouteId { get; set; }
        public Route? Route { get; set; }
        
        public Guid DriverId { get; set; }
        public User? Driver { get; set; }
        
        public Guid ConductorId { get; set; }
        public User? Conductor { get; set; }
        
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public ShiftStatus Status { get; set; } = ShiftStatus.Active;
    }
}
