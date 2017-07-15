using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ZipMoving.Models
{
    public class CustomEmailPhoneValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string email = value.ToString();

                if (!Regex.IsMatch(email, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", RegexOptions.IgnoreCase)
                    && !Regex.IsMatch(email, @"^([0-9\(\)\/\+ \-]*)$", RegexOptions.IgnoreCase))
                {
                    return new ValidationResult("Please Enter a Valid E-mail Address or Phone Number.");
                }

                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("This field is required");
            }
        }
    }
}