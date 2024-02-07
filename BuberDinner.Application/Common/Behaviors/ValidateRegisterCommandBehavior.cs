using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Authentication.Command.Register;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors
{
    // 这个是一个MediatR的管道行为，用来验证RegisterCommand
    public class ValidateRegisterCommandBehavior : IPipelineBehavior
    <RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IValidator<RegisterCommand> _validator;

        public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator)
        {
            _validator = validator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(
            RegisterCommand request,
            RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
            CancellationToken cancellationToken)
        {
            // 使用FluentValidation验证RegisterCommand
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                // 如何验证通过，继续执行下一个Handler
                return await next();
            }
            else
            {
                // 将验证失败的错误信息转换成ErrorOr<AuthenticationResult>
                var errors = validationResult.Errors
                    .Select(err => Error.Validation(err.PropertyName, err.ErrorMessage))
                    .ToList();
                return errors;
            }
        }
    }
}