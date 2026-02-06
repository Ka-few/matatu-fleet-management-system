using System;

namespace Matatu.Domain.Entities
{
    public class Route
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StandardFare { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
