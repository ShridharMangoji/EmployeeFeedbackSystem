using DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BAL.Util
{
    public class JwtToken
    {
       
        public static string GenerateJwtToken(RegisteredDevice device)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Name, device.User.Name),
                new Claim(ClaimTypes.Authentication, device.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(issuer: "Test",
             audience: "Test2",
             claims: claims,
             expires: DateTime.Now.AddMinutes(60),
             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static long GetUserID(string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
                var handler = new JwtSecurityTokenHandler();
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                      
                };
                var claims = handler.ValidateToken(token, validations, out var tokenSecure).Claims.ToList();
                var userID = claims.FirstOrDefault(c => c.Type.Contains("authentication")).Value??"0";
                return long.Parse(userID);
            }
            catch(Exception es)
            {
                return 0;
            }
        }



        
   
    }

}
