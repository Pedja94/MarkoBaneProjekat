using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Business.DataAccess;
using Business.DTO;

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
                return new ValidationResult("Field can't be empty");
            }
        }
    }

    public class CustomZipCodePickupValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                ZipCodeDTO zipCode = ZipCodes.ReadFromZipCodeString(value.ToString());

                if (zipCode == null)
                {
                    return new ValidationResult("Can\'t move from here. <a href=\" / index / index#contact\">Contact us for more information.</a>");
                }

                HttpContext.Current.Session["ZipCodePickup"] = value.ToString();
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Field can't be empty");
            }
        }
    }

    public class CustomZipCodeDeliveryValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string val1 = (string)HttpContext.Current.Session["ZipCodePickup"];
                if (val1 != null)
                {
                    ZipCodeDTO zipCodeTo = ZipCodes.ReadFromZipCodeString(value.ToString());
                    ZipCodeDTO zipCodeFrom = ZipCodes.ReadFromZipCodeString(val1);

                    if (zipCodeFrom == null || zipCodeTo == null || !ZipCodes.PossibleMoving(zipCodeFrom, zipCodeTo))
                    {
                        return new ValidationResult("Can\'t move here");
                    }

                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Field Pickup Zip code can't be empty");
                }
            }
            else
            {
                return new ValidationResult("Field can't be empty");
            }
        }
    }
}