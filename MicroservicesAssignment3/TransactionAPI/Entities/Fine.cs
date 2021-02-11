using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionAPI.Entities
{
    public class Fine
    {
        public long Id { get; set; }
        public long TransactionId { get; set; }
        public Transaction transaction { get; set; }
        public long Amount { get; set; }

    }
}
