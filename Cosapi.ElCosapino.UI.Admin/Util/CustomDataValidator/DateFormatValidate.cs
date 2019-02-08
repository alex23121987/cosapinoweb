using Cosapi.ElCosapino.CrossCutting.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Util.CustomDataValidator
{
    public class DateFormatValidate : ValidationAttribute, IClientValidatable
    {
        private readonly string _dateformat;

        public DateFormatValidate(string dateformat)
            : base("{0} format is incorrect.")
        {
            _dateformat = dateformat;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();

                if (!Utiles.IsValidStringToDate(_dateformat, valueAsString))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
        //new method
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule { ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()) };
            rule.ValidationParameters.Add("format", _dateformat);
            rule.ValidationType = "date";
            yield return rule;
        }
    }
}