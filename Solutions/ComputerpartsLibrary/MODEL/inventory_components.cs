using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Inventory_components
    {
        [Key]
        public int component_id { get; set; }
        public int quantity_available { get; set; }
    }
}
