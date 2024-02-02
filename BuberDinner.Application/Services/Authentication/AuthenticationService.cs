using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Domain.Entities;
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
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Register(string firstname, string lastname, string email, string password)
        {
            // 1. Check if the user already exists
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email already exists");
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

        public AuthenticationResult Login(string email, string password)
        {
            // 1. Check if the user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email does not exist");
            }

            // 2. Validate the password is correct
            if (user.Password != password)
            {
                throw new Exception("Invalid password");
            }

            // 3. Generate a token
            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(
                              user, token);
        }
    }
}
