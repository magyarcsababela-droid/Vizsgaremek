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
        public int PcId { get; set; }
        public int ProductId { get; set; } 
        public decimal AssemblyFee { get; set; }
    }
}
