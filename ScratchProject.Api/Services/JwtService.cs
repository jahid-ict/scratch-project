using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using ScratchProject.Api.DataTransferObjects;
using ScratchProject.Api.Intefaces;
using ScratchProject.Api.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ScratchProject.Api.Services
{
    public class JwtService : IJwtService
    {
        private  IConfiguration _configuration { get; set; }
        private IMongoDbClientService _mongoDbClientService { get; set; }
        public JwtService(IConfiguration configuration, IMongoDbClientService mongoDbClientService) { _configuration = configuration; _mongoDbClientService = mongoDbClientService; }
        public UserAuthenticationSuccessResponse GenerateAuthenticationToken(UserAuthenticationRequest userRequest)
        {
            var userModel = CreateUser(userRequest);
            DateTime tokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:expiry_minutes"]));
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Name, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, userModel.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, userModel.FirstName),
            };

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audiance"],
                claims,
                expires: tokenExpiration,
                signingCredentials: signingCredentials);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerator);

            return new UserAuthenticationSuccessResponse
            {
                Email = userRequest.Email,
                Token = token,
                ExpiryTime = tokenExpiration,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryMinutes"]))
            };
        }

        private UserModel CreateUser(UserAuthenticationRequest userRequest)
        {
            if(string.IsNullOrEmpty(userRequest.Email))
            {
                throw new Exception("Email not found");
            }
            var userModel = new UserModel { Email = userRequest.Email.ToLowerInvariant(), FirstName = userRequest.FirstName, LastName = userRequest.LastName };
            _mongoDbClientService.Save(userModel);
            return userModel;
        }

        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public ClaimsPrincipal GetPrincipalFromJwtToken(string token)
        {
            var tokenValidationParameter = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audiance"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal =  jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameter, out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken 
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
    }
}
