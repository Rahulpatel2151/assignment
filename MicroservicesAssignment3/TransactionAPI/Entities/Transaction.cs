using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionAPI.Entities
{
    public class Transaction
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public long BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
