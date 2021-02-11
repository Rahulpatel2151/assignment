using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Entities;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [RequestLimit("Books")]
        public IEnumerable<Books> GetBooks()
        {
            return new BookService().GetBooks();
        }


        [HttpGet, Route("{id}")]
        [RequestLimit("Book")]
        public Books GetBook(long id)
        {
            return new BookService().GetBook(id: id);
        }
    }
}
