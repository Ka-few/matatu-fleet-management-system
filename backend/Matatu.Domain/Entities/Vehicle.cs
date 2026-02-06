using System;

namespace Matatu.Domain.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Type { get; set; } = "Matatu";
        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
