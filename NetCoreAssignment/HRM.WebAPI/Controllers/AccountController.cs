using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HRM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("loginuser")]
        public HttpResponsiveViewModel LoginUser([FromBody]UserModel login)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();

            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)   
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
                model.Data = tokenString;
                model.StatusCode = 200;
                model.Result = true;
                model.Message = "Login Successful";
                return model;
            }
            model.Data = null;
            model.StatusCode = 401;
            model.Result = false;
            model.Message = "Login failed";
            return model;
        }
        [HttpGet]
        [Authorize]
        [Route("getuser")]
        public HttpResponsiveViewModel GetUser()
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();
            var currentUser = HttpContext.User;
            Console.WriteLine(currentUser.Identity.Name);
            UserModel user = new UserModel();
            user.Username = currentUser.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            user.Email = currentUser.Claims.Where(x=>x.Type==ClaimTypes.Email).FirstOrDefault().Value;
            model.Data = user;
            model.StatusCode = 200;
            model.Result = true;
            model.Message = "Admin Data fetched successfully";
            return model;
        }
        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Name, userInfo.Username),
                new Claim(ClaimTypes.Email, userInfo.Email)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    
            if (login.Username == "admin" && login.Password=="password")
            {
                user = new UserModel { Username = "admin", Email = "admin@gmail.com" };
            }
            return user;
        }
    }
}