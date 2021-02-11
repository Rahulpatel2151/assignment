using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        [RequestLimit("Gateway")]
        public ActionResult GetGatewayMessage()
        {
            return Ok("You are accessing API Gateway");
        }
        //Ocelot is used for API Gateway
        //open ocelot.json to get the endpoints of all services then browse
        // API gateway Url : https://localhost:44398/
        //ex. https://localhost:44398/api/books will call LibraryAPI to get books
    }
}
