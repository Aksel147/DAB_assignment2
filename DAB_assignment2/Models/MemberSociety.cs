using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class MemberSociety
    {
        public Society Society { get; set; }
        public string SocietyId { get; set; }
        public Member Member { get; set; }
        public int MemberId { get; set; }
    }
}
