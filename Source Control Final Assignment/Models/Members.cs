using Source_Control_Final_Assignment.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Source_Control_Final_Assignment.Models
{
    public class Members
    {
        public int Id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
    public class MemberRegistration
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("UserName")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Password")]
        [RegularExpression("^(([a-z]|[A-Z]|[0-9]|[!@#$%._]){4,12})$", ErrorMessage = "Password must contain minimum 4 and maximum 12 characters and only contain !@#$%* special characters")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DisplayName("Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }
        [Required]
        [DisplayName("Full Name")]
        [MaxLength(50,ErrorMessage ="Do not enter more than 50 characters")]
        public string Fullname { get; set; }
        [Required]
        [DisplayName("Designation")]
        public string Designation { get; set; }
        [Required]
        [DisplayName("Salary")]
        [Range(0, long.MaxValue, ErrorMessage = "Salary Must be Positive")]
        public decimal Salary { get; set; }
        [Required]
        [DisplayName("Age")]
        [Range(18,120,ErrorMessage ="Age must be in range of 18 to 120")]
        public int Age { get; set; }
        [ExtensionValidator(ErrorMessage ="Upload .jpg, .jpeg, .png files only")]
        public HttpPostedFileBase profilephoto { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [GraduationValidator(ErrorMessage ="Graduation must be after 1-1-2020")]
        public DateTime GraduationDate { get; set; }
    }
    public class ChangePassword
    {
        public int Id { get; set; }
        [Required]
        public string currentPassword { get; set; }
        [Required]
        [RegularExpression("^(([a-z]|[A-Z]|[0-9]|[!@#$._]){4,12})$", ErrorMessage = "Password must contain minimum 4 and maximum 12 characters and only contain !@#$%* special characters")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword",ErrorMessage ="Password didn't match")]
        public string confirmPassword { get; set; }

    }
}