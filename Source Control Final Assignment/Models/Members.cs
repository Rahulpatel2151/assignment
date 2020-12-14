using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Source_Control_Final_Assignment.Models
{
    public class Members
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class MemberRegistration
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Fullname { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
       
    }
}