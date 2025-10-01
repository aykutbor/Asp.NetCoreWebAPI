using Core.Entities;
using Core.Utilities.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.JWT
{
    public class JwtHelper : ITokenHelper
    {

        private readonly TokenOptions tokenOptions;

        public JwtHelper(TokenOptions tokenOptions)
        {
            this.tokenOptions = tokenOptions;
        }

        public AccessToken CreateToken(BaseUser user)
        {
            // Özellikleri oku ve token'i yaz.
            DateTime expirationTime = DateTime.Now.AddMinutes(tokenOptions.ExpirationTime); // Tokenin geçerliliğini yitireceği zamanı ayarlama.
            SecurityKey key = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey); // Token'i imzalayacağımız key. 

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); // Giriş bilgileri

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: null,
                notBefore: DateTime.Now,
                expires: expirationTime,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();    // JWT'yi string'e çevirir. Çünkü createToken'in tipi string'dir.

            string jwtToken = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken() { Token = jwtToken, ExpirationTime = expirationTime };
        }
    }
}

// Burada üretilen token ile doğruladığımız token'ın (appsettings.developer içerisinde) aynı özellikleri paylaşması lazım. Bunun için bu class'ın appsetting.developer içerisini okuyabilmesi lazım.
