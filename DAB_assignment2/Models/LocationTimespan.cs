using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class LocationTimespan
    {
        public Location Location { get; set; }
        public string LocationId { get; set; }
        public Timespan Timespan { get; set; }
        public string TimespanId { get; set; }
    }
}
