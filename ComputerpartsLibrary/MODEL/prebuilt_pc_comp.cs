using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Prebuilt_pc_comp
    {
        [Key]
        public int PcId { get; set; } 
        public int ComponentId { get; set; } 
        public int Quantity { get; set; }
    }
}
