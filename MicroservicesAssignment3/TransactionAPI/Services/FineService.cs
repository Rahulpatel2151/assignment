using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionAPI.Entities;

namespace TransactionAPI.Services
{
    public class FineService
    {
        public List<Fine> getFines()
        {
            var fines = new List<Fine>();
            var transactions = new TransactionService().GetTransactions();
            int x = 1;
            foreach (var transaction in transactions)
            {
                for (int i = 0; i <= 3; i++)
                {
                    fines.Add(new Fine
                    {
                        Id = i + x,
                        TransactionId = transaction.Id,
                        transaction = transaction,
                        Amount = 200,
                    });
                }
                x += 4;
            }
            return fines;
        }
        public Fine getFine(long id)
        {
            var fine = getFines().Find(x => x.Id == id);
            return fine;
        }
        public List<Fine> getFineByMemberId(long id)
        {
            var fines = getFines().FindAll(x => x.transaction.MemberId == id);
            return fines;
        }
    }
}
