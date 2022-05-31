using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NetCoreWebGoat.Auth
{
    public class JwtTokenBuilder
    {
        private readonly int expiryInMinutes;

        private readonly SecurityKey key;

        private List<Claim> claims;

        public JwtTokenBuilder(string key, int expiryInMinutes, List<Claim> claims)
        {
            this.key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            this.claims = claims;
            this.expiryInMinutes = expiryInMinutes;
        }

        public JwtToken Build()
        {
            var token = new JwtSecurityToken(
              claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                )
            );
            return new JwtToken(token);
        }
    }
}