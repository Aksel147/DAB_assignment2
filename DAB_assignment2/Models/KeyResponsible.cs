using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class KeyResponsible
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Identification { get; set; }
        public int MemberId { get; set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, (new JsonSerializerOptions() {WriteIndented = true}));
        }
    }
}
