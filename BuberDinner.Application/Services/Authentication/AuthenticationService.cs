using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using FluentResults;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Register(string firstname, string lastname, string email, string password)
        {
            // 1. Check if the user already exists
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // 2. If not, create a new user(generate unique id) & Persistence to DB
            var user = new User
            {
                Id = Guid.NewGuid(),
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = password
            };
            _userRepository.AddUser(user);

            // 3. Generate a token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user, token);
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // 1. Check if the user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 2. Validate the password is correct
            if (user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 3. Generate a token
            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(
                              user, token);
        }
    }
}
