using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PreJoiningFinalAssignment.CustomValidation;
namespace PreJoiningFinalAssignment.Models
{
    public class ProductView
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string Category { get; set; }
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Price must be Positive")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Price must be Positive")]
        public int Quantity { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [Required]
        [ExtensionValidator(ErrorMessage ="Upload only Image")]
        public HttpPostedFileBase SmallImage { get; set; }
        [ExtensionValidator(ErrorMessage = "Upload only Image")]
        public HttpPostedFileBase LargeImage { get; set; }
    }
}