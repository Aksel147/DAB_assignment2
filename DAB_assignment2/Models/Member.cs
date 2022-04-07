using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB_assignment2.Models
{
    public class Member
    {
        public int Id { get; set; }
        public List<MemberSociety> Societies { get; set; }
        public KeyResponsible? KeyResponsible { get; set; }
        public Chairman? Chairman { get; set; }
    }
}
