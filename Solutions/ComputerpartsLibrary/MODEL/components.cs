using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Components
    {
        [Key]
        public int id { get; set; }
        public int type_id { get; set; }
        public string? sku { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
    }
}
