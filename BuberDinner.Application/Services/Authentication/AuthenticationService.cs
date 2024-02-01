using BuberDinner.Application.Common.Interface.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Register(string firstname, string lastname, string email, string password)
        {
            // Check if email is already registered

            // Create user(generate unique id)

            // Create JWT Token
            Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(userId, firstname, lastname);
            return new AuthenticationResult(
                userId, firstname, lastname, email, token);
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(
                               Guid.NewGuid(), "John", "Doe", email, "token");
        }
    }
}
