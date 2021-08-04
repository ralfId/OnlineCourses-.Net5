using Application.Contracts;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security.TokenSecurity
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CreateToken(Users user, List<string> roles)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Name, user.LastName, user.UserName)
            };

            if (roles != null)
            {
                roles.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));
            }



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ud8?xv$K5f7rvJ2=H3E5J*mk!9G"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(27),
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);

        }
    }
}
