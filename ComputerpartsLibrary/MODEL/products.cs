using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public categories Category { get; set; } = null!;
    }
}
