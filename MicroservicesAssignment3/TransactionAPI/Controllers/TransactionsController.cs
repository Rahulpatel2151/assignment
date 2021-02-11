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
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpGet]
        [RequestLimit("Transactions")]
        public IEnumerable<Transaction> GetTransactions()
        {
            return new TransactionService().GetTransactions();
        }


        [HttpGet, Route("{id}")]
        [RequestLimit("Transaction")]
        public Transaction GetTransaction(long id)
        {
            return new TransactionService().GetTransaction(id: id);
        }

    }
}
