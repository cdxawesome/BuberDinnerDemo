﻿using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Services;
using BuberDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly JwtSettings _jwtOptions;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            this._jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)), SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer, audience: _jwtOptions.Audience, claims: claims, expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes), signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
            // 这是一个注释
            // "This is a comment"
        }
    }
}
