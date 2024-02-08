using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors
{
    // 这个是一个MediatR的管道行为，用来验证Request
    /*
    TRequest限定为IRequest<TResponse>，TResponse限定为IErrorOr
    这样做的目的是，只有实现了IRequest<TResponse>的Request才能被验证
    只有实现了IErrorOr的Response才能被返回
    然而RegisterCommand实现了IRequest, ErrorOr<AuthenticationResult>实现了IErrorOr

    这样就实现了一个通用的验证行为，可以验证任何实现了上述接口的请求
    */
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior
    <TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator == null)
            {
                // 如果没有Validator，直接执行下一个Handler
                return await next();
            }
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

                // 因为TResponse无法显示的转换为ErrorOr<AuthenticationResult>，所以使用dynamic
                // 在运行时转换，如果转换不了，会抛出运行时异常
                return (dynamic)errors;
            }
        }
    }
}