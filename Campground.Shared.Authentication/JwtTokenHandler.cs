using Azure;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Shared.Authentication.Models;
using Campground.Shared.Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Campground.Shared.Authentication
{
    public class JwtTokenHandler(UserAccountService userAccountService, IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserAccountService _userAccountService = userAccountService;
        public const string JWT_SECURITY_KEY = "Bl8xp5CEFM22b1XvoXR6j04MSOk1sMRFKC62HX+5lFg=";
        private const int JWT_TOKEN_VALIDITY_MINS = 60;

        public async Task<AuthenticationResponse?> GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if(string.IsNullOrWhiteSpace(authenticationRequest.Username) || string.IsNullOrWhiteSpace(authenticationRequest.Password)) return null;

            var userAccount = await _userAccountService.AuthenticateUser(authenticationRequest);
            if (userAccount == null) return null;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
{
                            new Claim("Id", userAccount.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Name, userAccount.Username!),
                            new Claim(JwtRegisteredClaimNames.Email, userAccount.Email!)
                        }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey") ?? _configuration["Jwt:Key"]!)),
                                        SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return new AuthenticationResponse
            {
                Id = userAccount.Id,
                Username = userAccount.Username!,
                FirstName = userAccount.FirstName!,
                LastName = userAccount.LastName!,
                ExpiresIn = (int) DateTime.UtcNow.AddDays(1).Subtract(DateTime.UtcNow).TotalSeconds,
                JwtToken = jwtToken
            };
        }
    }
}
