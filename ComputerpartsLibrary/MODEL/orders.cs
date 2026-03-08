using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class orders
    {
        [Key]
        public int OrderId { get; set; } 
        public int UserId { get; set; } 
        public int ShippingAddressId { get; set; } 
        public string? Status { get; set; } 
        public string? PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset PlacedAt { get; set; }
    }
}
