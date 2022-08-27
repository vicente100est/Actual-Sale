using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WSSale.Models;
using WSSale.Models.Common;
using WSSale.Models.Request;
using WSSale.Models.Response;
using WSSale.Tools;

namespace WSSale.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest modelRequest)
        {
            UserResponse userResponse = new UserResponse();

            using (var db = new ActualSaleContext())
            {
                string spassword = Encrypt.GetSHA256(modelRequest.Password);

                var user = db.Users.Where(d => d.EmailUser == modelRequest.Email &&
                    d.PasswordUser == spassword).FirstOrDefault();

                if (user == null)
                    return null;

                userResponse.Email = user.EmailUser;
                userResponse.Token = GetTocken(user);
            }

            return userResponse;
        }

        private string GetTocken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                        new Claim(ClaimTypes.Email, user.EmailUser)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
