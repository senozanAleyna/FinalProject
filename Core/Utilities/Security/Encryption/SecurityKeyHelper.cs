using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    //şifreleme olan sistemlerde her şeyi bytearray formatında veriyor olmamız gerekiyor
    //mysupersecretkey 'i asp.net jwt nin anlayacagı hale getırmemız gerekıyor
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            //simetrik key oluştur
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
