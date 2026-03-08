using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Custom_builds
    {
        [Key]
        public int BuildId { get; set; } 
        public int UserId { get; set; } 
        public string? Name { get; set; }
        public string? Status { get; set; } 
        public decimal TotalPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
