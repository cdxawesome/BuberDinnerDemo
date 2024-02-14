using System.Reflection;
using BuberDinner.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            // services.AddScoped<
            //     IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>,
            //     ValidateRegisterCommandBehavior>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // 将RegisterCommandValidator注册到DI容器中
            /*这种方式的DI，在每次添加了新的Validator时，都要写一次*/
            // services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();

            /*这种方式的DI，是将FluentValidation整合进Asp.Net Core中，只需要写一次DI，就可以任意使用。
            在使用的时候，传入对应的泛型参数即可*/
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
