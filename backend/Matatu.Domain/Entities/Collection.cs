using System;

namespace Matatu.Domain.Entities
{
    public class Collection
    {
        public Guid Id { get; set; }
        public Guid ShiftId { get; set; }
        public Shift? Shift { get; set; }
        
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; // Cash, Mpesa
        public string? TransactionReference { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
