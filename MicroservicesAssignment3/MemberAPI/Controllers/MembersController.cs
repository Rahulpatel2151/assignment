using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberAPI.Entities;
using MemberAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        [RequestLimit("Members")]
        public IEnumerable<Member> GetMembers()
        {
            return new MemberService().GetMembers();
        }
        [HttpGet, Route("{id}")]
        [RequestLimit("Member")]
        public Member GetMember(long id)
        {
            return new MemberService().GetMember(id: id);
        }
    }
}
