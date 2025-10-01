using Core.Entities;
using Core.Utilities.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.JWT
{
    public class JwtHelper
    {

        private readonly TokenOptions tokenOptions;

        public JwtHelper(TokenOptions tokenOptions)
        {
            this.tokenOptions = tokenOptions;
        }

        public string CreateToken(BaseUser user)
        {
            // Özellikleri oku ve token'i yaz.
            DateTime expirationTime = DateTime.Now.AddMinutes(tokenOptions.ExpirationTime); // Tokenin geçerliliğini yitireceği zamanı ayarlama.
            SecurityKey key = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey);

            // Token oluştur.

            return "";
        }
    }
}

// Burada üretilen token ile doğruladığımız token'ın (appsettings.developer içerisinde) aynı özellikleri paylaşması lazım. Bunun için bu class'ın appsetting.developer içerisini okuyabilmesi lazım.
