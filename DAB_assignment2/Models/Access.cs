using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Access
    {
        public string Codes { get; set; }
        public string KeyLocation { get; set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, (new JsonSerializerOptions() {WriteIndented = true}));
        }
    }
}
