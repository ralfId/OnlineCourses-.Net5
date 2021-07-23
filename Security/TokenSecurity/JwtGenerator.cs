using Application.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.TokenSecurity
{
    public class JwtGenerator : IJwtGenerator
    {
        public Task<string> CreateTokenAsync(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
