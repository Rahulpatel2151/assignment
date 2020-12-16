using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Source_Control_Final_Assignment.CustomValidations
{
    public class GraduationValidator : ValidationAttribute,IClientValidatable
    {

        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime >= new DateTime(2020, 1, 1);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ErrorMessage = base.ErrorMessage;
            rule.ValidationType = "gradval";
            rule.ValidationParameters.Add("max", "2020-01-01");
            yield return rule;
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