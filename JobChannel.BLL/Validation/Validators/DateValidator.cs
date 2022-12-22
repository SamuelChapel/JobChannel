using System;
using System.Globalization;
using FluentValidation.Validators;
using FluentValidation;

namespace JobChannel.BLL.Validation.Validators
{
    public class DateValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override string Name => "DateValidator";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            return value switch
            {
                not null and not (string or DateTime) => false,
                string stringValue => DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out _),
                DateTime => true,
                _ => true
            };
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "'{PropertyName}' has invalid date.";
        }
    }
}
