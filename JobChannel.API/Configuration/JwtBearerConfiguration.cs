using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JobChannel.API.Configuration
{
    public static class JwtBearerConfiguration
    {
        public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key"))),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return builder;
        }
    }
}
