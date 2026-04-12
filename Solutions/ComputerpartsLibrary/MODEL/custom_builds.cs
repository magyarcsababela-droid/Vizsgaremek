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
        public int build_id { get; set; } 
        public int User_id { get; set; } 
        public string? name { get; set; }
        public string? status { get; set; } 
        // statuses in DB are lowercase like 'draft', 'pending', 'completed'
        public decimal total_price { get; set; }
        // JSON-serialized list of selected components for this build
        public string? components_json { get; set; }
        public DateTimeOffset created_at { get; set; }
    }
}
