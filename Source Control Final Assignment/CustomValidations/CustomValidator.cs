using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Source_Control_Final_Assignment.CustomValidations
{
    public class GraduationValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime >= new DateTime(2020, 1, 1);
        }
    }
    public class ExtensionValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            FileExtensionsAttribute fileExtensionsAttribute = new FileExtensionsAttribute();
            fileExtensionsAttribute.Extensions = ".jpg,.jpeg,.png";
            if (file != null)
            {
                bool ext = fileExtensionsAttribute.IsValid(Path.GetExtension(file.FileName));
                return ext;
            }
            else
            {
                return true;
            }

        }
    }
}