using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Chairman
    {
        public string CPR { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Society> Societies { get; set; }
    }
}
