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
        public int pc_id { get; set; } 
        public int component_id { get; set; } 
        public int quantity { get; set; }
    }
}
