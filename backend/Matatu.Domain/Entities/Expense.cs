using System;

namespace Matatu.Domain.Entities
{
    public class Expense
    {
        public Guid Id { get; set; }
        public Guid ShiftId { get; set; }
        public Shift? Shift { get; set; }
        
        public decimal Amount { get; set; }
        public string Category { get; set; } = "Misc";
        public string Description { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
