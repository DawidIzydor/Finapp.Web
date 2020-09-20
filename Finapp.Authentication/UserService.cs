using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Finapp.Authentication.Exception;
using Finapp.Authentication.Models;
using Finapp.Database;
using Finapp.Dto;
using Finapp.Dto.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Finapp.Authentication
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly FinappDbContext _finappDbContext;

        public UserService(IOptions<AppSettings> appSettings, FinappDbContext finappDbContext)
        {
            _finappDbContext = finappDbContext;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticationResponseDto> AuthenticateAsync(AuthenticationRequestDto requestModel)
        {
            var dbUser = await _finappDbContext.Users.SingleOrDefaultAsync(u => u.Name == requestModel.Username);

            if (dbUser == null)
            {
                throw new UserNotFoundException {UserName = requestModel.Username};
            }

            if (PasswordManager.VerifyPassword(requestModel.Password, dbUser.PasswordHash) == false)
            {
                throw new WrongPasswordException();
            }

            var token = GenerateJwtToken(dbUser);

            return new AuthenticationResponseDto
            {
                Username = dbUser.Name,
                Token = token
            };
        }

        public async Task<FinappUser> GetByNameAsync(string name)
        {
            var nameLower = name.ToLowerInvariant();
            return await _finappDbContext.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == nameLower);
        }

        private string GenerateJwtToken(FinappUserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Name)}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}