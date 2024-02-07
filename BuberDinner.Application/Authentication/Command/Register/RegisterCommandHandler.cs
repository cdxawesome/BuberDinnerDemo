using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;
using BuberDinner.Domain.Common.Errors;

namespace BuberDinner.Application.Authentication.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            // 1. Check if the user already exists
            if (_userRepository.GetUserByEmail(request.Email) is not null)
            {
                // 返回自定义错误
                return Errors.User.DuplicateEmail;
            }

            // 2. If not, create a new user(generate unique id) & Persistence to DB
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };
            _userRepository.AddUser(user);

            // 3. Generate a token
            var token = _jwtTokenGenerator.GenerateToken(user);

            await Task.CompletedTask;
            return new AuthenticationResult(
                user, token);
        }
    }

}
