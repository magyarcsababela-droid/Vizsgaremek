using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Prebuilt_pcs
    {
        [Key]
        public int pc_id { get; set; }
        public int product_id { get; set; } 
        public decimal assembly_fee { get; set; }
    }
}
