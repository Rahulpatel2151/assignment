using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SourceControlAssignment1.Models
{
    public class Employee
    {
        [Required]
        [DisplayName("FirstName")]
        public String firstName { get; set; }
        [Required]
        [DisplayName("LastName")]
        public String lastName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("MobileNo.")]
        public String mobileNo { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public String email { get; set; }
        [Required]
        [Range(1,130)]
        [DisplayName("Age")]
        public int age { get; set; }
        [Required]
        [MaxLength(500)]
        [DisplayName("Address")]
        public String address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Birthdate")]
        public DateTime birthDate { get; set; }
        [Required]
        [DisplayName("Profile Photo")]
        [FileExtensions(Extensions =".jpg .jpeg .png")]
        public HttpPostedFileBase profilePhoto { get; set; }
    }
}