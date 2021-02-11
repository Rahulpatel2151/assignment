using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionAPI.Entities;
using TransactionAPI.Services;

namespace TransactionAPI.Controllers
{
    [Route("api/fines")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        [RequestLimit("Fines")]
        public IEnumerable<Fine> GetFines()
        {
            return new FineService().getFines();
        }


        [HttpGet, Route("{id}")]
        [RequestLimit("Fine")]
        public Fine GetFine(long id)
        {
            return new FineService().getFine(id: id);
        }
        [HttpGet, Route("member/{id}")]
        [RequestLimit("FineByMember")]
        public List<Fine> GetFinesByMemberId(long id)
        {
            return new FineService().getFineByMemberId(id: id);
        }
    }
}
