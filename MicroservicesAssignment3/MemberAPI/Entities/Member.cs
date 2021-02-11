using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberAPI.Entities
{
    public class Member
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public byte IssueLimit { get; set; }
    }
}
