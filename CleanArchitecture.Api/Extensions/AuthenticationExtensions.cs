using CleanArchitecture.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void ConfigureAuthenticationPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var _apiUrl = configuration.GetValue<string>("ApplicationURLs:ApiUrl");
            var _clientUrl = configuration.GetValue<string>("ApplicationURLs:ClientUrl");

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = TokenAuthOption.Key,
                    //ValidAudience = _clientUrl,
                    //ValidateAudience = true,
                    ValidIssuer = _apiUrl,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            });

        }
    }
}
