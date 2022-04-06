using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class RoomTimespan
    {
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public Timespan Timespan { get; set; }
        public string TimespanId { get; set; }
    }
}
