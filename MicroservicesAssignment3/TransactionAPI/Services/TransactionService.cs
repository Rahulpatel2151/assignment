using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionAPI.Entities;

namespace TransactionAPI.Services
{
    public class TransactionService
    {
        public List<Transaction> GetTransactions()
        {
            var transactions = new List<Transaction>();
            for (int i = 1; i <= 3; i++)
            {
                transactions.Add(new Transaction
                {
                    Id = i,
                    MemberId = i,
                    BookId = i,
                    IssueDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14)
                });
            }
            return transactions;
        }

        public Transaction GetTransaction(long id)
        {
            var transaction = GetTransactions().Find(r => r.Id == id);

            return transaction;
        }
    }
}
