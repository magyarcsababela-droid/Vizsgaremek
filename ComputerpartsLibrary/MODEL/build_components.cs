using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Build_components
    {
        [Key]
        public int build_id { get; set; } 
        public int component_id { get; set; } 
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
    }
}
