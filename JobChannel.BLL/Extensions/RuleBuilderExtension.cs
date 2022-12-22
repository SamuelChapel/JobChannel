using FluentValidation;
using JobChannel.BLL.Validation.Validators;

namespace JobChannel.BLL.Extensions
{
    public static class RuleBuilderExtension
    {
        public static IRuleBuilderOptions<T, TProperty> Date<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new DateValidator<T, TProperty>());
        }
    }
}
