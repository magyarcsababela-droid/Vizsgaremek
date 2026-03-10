using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Orders
    {
        [Key]
        public int order_id { get; set; } 
        public int user_id { get; set; } 
        public int shipping_address_id { get; set; } 
        public string? status { get; set; } 
        public string? payment_method { get; set; }
        public decimal total_amount { get; set; }
        public DateTimeOffset placed_at { get; set; }
    }
}
