using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Society
    {
        public string CVR { get; set; }
        public string Activity { get; set; }
        public string Address { get; set; }
        public List<MemberSociety> Members { get; set; }
        public List<Booking> Bookings { get; set; }
        public Chairman Chairman { get; set; }
        public string ChairmanId { get; set; }
        public KeyResponsible? KeyResponsible { get; set; }
    }
}
