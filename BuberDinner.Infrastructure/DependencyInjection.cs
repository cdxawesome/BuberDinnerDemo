using System.Text;
using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Application.Common.Interface.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
                this IServiceCollection services,
                ConfigurationManager configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAuth(configuration);
            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 将配置文件中的 JwtSettings 配置绑定到 JwtSettings 对象上
            var jwtsettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtsettings);

            // 将 JwtSettings 对象注册为单例服务
            services.AddSingleton(Options.Create(jwtsettings));

            // 这里需要引入包 Microsoft.Extensions.Options.ConfigurationExtensions
            // This allows you to access these settings throughout your application using the IOptions<JwtSettings>
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtsettings.Issuer,
                        ValidAudience = jwtsettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(jwtsettings.Secret))
                    };
                });
            return services;
        }
    }
}
