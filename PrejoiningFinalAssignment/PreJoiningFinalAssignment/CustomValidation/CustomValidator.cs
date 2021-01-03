﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace PreJoiningFinalAssignment.CustomValidation
{
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