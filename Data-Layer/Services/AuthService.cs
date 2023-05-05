using Data_Layer.Interface;
using Data_Layer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Services
{
    public class AuthService : IAuthService
    {


        private readonly DatabaseContext _dbContext;

        public AuthService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AuthModel> AuthenticateUser(string username, string password)
        {
            var user = await _dbContext.Users.Where(u => u.UserName == username).FirstOrDefaultAsync();
            if (user is null)
            {
                return new AuthModel { isAuthenticated  = false, CurrentUser = null};
            }
            if(user.Password == password)
            {
                return new AuthModel { isAuthenticated = true, CurrentUser = user};
            }
            return new AuthModel {   isAuthenticated = false, CurrentUser = null};

        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public JwtSecurityToken GenerateToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sampleScretKey@1234"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5187",
                audience: "https://localhost:5188",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            return tokeOptions;
        }

        public async Task<User> getCurrentUser(string username)
        {
            var user = await _dbContext.Users.Where(u => u.UserName == username).FirstOrDefaultAsync();
            return user;

        }

        public async Task<User> GetCurrentUserFromExpiredToken(string token, string username)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("sampleScretKey@1234")
                    ),
                ValidateLifetime = false,
                ValidIssuer = "http://localhost:5187",
                ValidAudience = "https://localhost:5188"
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");  
            }

            User currentUser = await getCurrentUser(username);

            return currentUser;
        }

    }
}
