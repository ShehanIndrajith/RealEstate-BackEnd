using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Core.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public int UserID { get; set; }
        public int? PropertyID { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        public string TransactionID { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string StripeChargeId { get; set; }
        public string Metadata { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public Property Property { get; set; }
    }
}
