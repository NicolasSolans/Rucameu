using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IRepositoryBase<User> _userRepositoryBase;
        private readonly IConfiguration _config;

        public AuthenticationService(IRepositoryBase<User> userRepositoryBase, IConfiguration config)
        {
            _config = config;
            _userRepositoryBase = userRepositoryBase;
        }

        private async Task<User?> ValidateUser(string email, string password)
        {
            var user = await _userRepositoryBase.GetAllAsync();

            var findUser = user.Where(u => u.Email == email).FirstOrDefault();
            if (findUser == null) throw new Exception("El usuario no existe");

            if (findUser.Password != password)
            {
                return null;
            }

            return findUser;
        }

        public async Task<string> Authenticate(AuthenticationRequestDTO authenticationRequestDTO)
        {
            // primero validamos las credenciales
            var validatedUser = await ValidateUser(authenticationRequestDTO.Email, authenticationRequestDTO.Password);

            if (validatedUser is null)
            {
                throw new InvalidCredentialsException("Credenciales inválidas");
            }

            // luego comenzamos el proceso de creación del token

            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", validatedUser.Id.ToString()));
            claimsForToken.Add(new Claim("role", validatedUser.Role.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(15),
                signature
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }
    }
}