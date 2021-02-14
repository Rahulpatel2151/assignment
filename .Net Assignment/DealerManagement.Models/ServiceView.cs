using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.Models
{
    public class ServiceView
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public Nullable<int> Duration { get; set; }
        public string Status { get; set; }
    }
}
