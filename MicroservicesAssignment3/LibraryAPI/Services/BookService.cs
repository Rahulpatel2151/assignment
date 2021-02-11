using LibraryAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class BookService
    {
        public List<Books> GetBooks()
        {
            var books = new List<Books>();
            for (int i = 1; i <= 7; i++)
            {
                if (i % 2 == 0)
                {
                    books.Add(new Books
                    {
                        Id = i,
                        Name = $"Book_{i}",
                        Author = $"Author_{i}",
                        PublicationYear = $"200{i}",
                        price = i * 100,
                        status = "Available"
                    });
                }
                else
                {
                    books.Add(new Books
                    {
                        Id = i,
                        Name = $"Book_{i}",
                        Author = $"Author_{i}",
                        PublicationYear = $"200{i}",
                        price = i * 100,
                        status = "Not Available"
                    });
                }
            }
            return books;
        }
        public Books GetBook(long id)
        {
            return GetBooks().Find(x => x.Id == id);
        }
    }
}
