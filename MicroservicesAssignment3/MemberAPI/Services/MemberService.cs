using MemberAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberAPI.Services
{
    public class MemberService
    {
        public List<Member> GetMembers()
        {
            var members = new List<Member>();
            for (int i = 1; i <= 7; i++)
            {

                members.Add(new Member
                {
                    Id = i,
                    Name = $"Name_{i}",
                    Address = $"Address_{i}",
                    MobileNo = $"12345678{i}",
                    IssueLimit = 5,
                });


            }
            return members;
        }
        public Member GetMember(long id)
        {
            return GetMembers().Find(x => x.Id == id);
        }
    }
}
