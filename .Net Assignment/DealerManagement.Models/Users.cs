using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealerManagement.Models
{
    public partial class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }



    }
    public class Login
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DisplayName("Password")]
        [RegularExpression("^(([a-z]|[A-Z]|[0-9]|[!@#$%._]){4,12})$", ErrorMessage = "Password must contain minimum 4 and maximum 12 characters and only contain !@#$%* special characters")]
        public string Password { get; set; }
    }
    public class UserRegistration
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Password")]
        [RegularExpression("^(([a-z]|[A-Z]|[0-9]|[!@#$%._]){4,12})$", ErrorMessage = "Password must contain minimum 4 and maximum 12 characters and only contain !@#$%* special characters")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password didn't match")]
        [RegularExpression("^(([a-z]|[A-Z]|[0-9]|[!@#$._]){4,12})$", ErrorMessage = "Password must contain minimum 4 and maximum 12 characters and only contain !@#$%* special characters")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DisplayName("Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PinCode { get; set; }
        [Required]
        public string City { get; set; }
    }
    public class ChangePassword
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^(([a-z]|[A-Z]|[0-9]|[!@#$._]){4,12})$", ErrorMessage = "Password must contain minimum 4 and maximum 12 characters and only contain !@#$%* special characters")]
        public string currentPassword { get; set; }
        [Required]
        [RegularExpression("^(([a-z]|[A-Z]|[0-9]|[!@#$._]){4,12})$", ErrorMessage = "Password must contain minimum 4 and maximum 12 characters and only contain !@#$%* special characters")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Password didn't match")]
        [RegularExpression("^(([a-z]|[A-Z]|[0-9]|[!@#$._]){4,12})$", ErrorMessage = "Password must contain minimum 4 and maximum 12 characters and only contain !@#$%* special characters")]
        public string ConfirmPassword { get; set; }

    }
}