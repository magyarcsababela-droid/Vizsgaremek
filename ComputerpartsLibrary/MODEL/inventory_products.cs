using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Inventory_products
    {
        [Key]
        public int ProductId { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
