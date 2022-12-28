using System;
using FluentValidation;
using FluentValidation.Validators;
using JobChannel.BLL.Validation.Validators;
using JobChannel.DAL.UOW.Repositories.JobRepositories;

namespace JobChannel.BLL.Extensions
{
    public static class RuleBuilderExtension
    {
        public static IRuleBuilderOptions<T, TProperty> Date<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new DateValidator<T, TProperty>());
        }

        public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new RegularExpressionValidator<T>("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)")).WithErrorCode("UrlInvalid");
        }

        public static IRuleBuilderOptions<T, string?> CodeRome<T>(this IRuleBuilder<T, string?> ruleBuilder, IJobRepository jobRepository)
        {
            return ruleBuilder.MustAsync(async (a, c) => a is not null && await jobRepository.GetByRomeCode(a) != null).WithErrorCode("JobNotInDB");
        }
    }
}
