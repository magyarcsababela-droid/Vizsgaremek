using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Products
    {
        [Key]
        public int id { get; set; }
        public int category_id { get; set; }
        public string name { get; set; } = null!;
        public string sku { get; set; } = null!;
        public decimal base_price { get; set; }
        public string image_url { get; set; } = null!;
        public string description { get; set; } = null!;
    }
}
