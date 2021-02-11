using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Entities
{
    public class Librarian
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Shift { get; set; }
        public long Salary { get; set; }
    }
}
