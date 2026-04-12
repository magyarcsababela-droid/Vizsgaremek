using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Categories
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string description { get; set; } = null!;
    }
}
