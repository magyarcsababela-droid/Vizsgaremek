using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class custom_builds
    {
        public int BuildId { get; set; } 
        public int UserId { get; set; } 
        public string? Name { get; set; }
        public string? Status { get; set; } 
        public decimal TotalPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
