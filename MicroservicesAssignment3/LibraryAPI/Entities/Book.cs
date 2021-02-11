using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Entities
{
    public class Books
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string PublicationYear { get; set; }
        public long price { get; set; }
        public string status { get; set; }
    }
}
