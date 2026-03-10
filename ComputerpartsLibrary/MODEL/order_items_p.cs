using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Order_items_p
    {
        [Key]
        public int item_id { get; set; }
        public int order_id { get; set; }
        public int build_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_price => quantity * unit_price;
    }
}
