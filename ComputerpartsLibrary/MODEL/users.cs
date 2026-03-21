using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.MODEL
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; } = null!;
        public string email { get; set; } = null!;
        [Column(TypeName = "nvarchar(max)")]
        public string password_hash { get; set; } = null!;
        public DateTimeOffset created_at { get; set; }
        public string? role { get; set; } // Optional: "Admin", "User", etc.
    }
}
