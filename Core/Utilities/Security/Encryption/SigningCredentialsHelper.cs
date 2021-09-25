using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        
        //JWT servislerinin oluşturulabilmesi için elimizde olanlardır
        //credentials(kimlikler)sistemi kullanabilmek için anahtarımız= bizim anahtarımız ==sisteme girmek için elimizde olanlar
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            //sen hashing yapacaksın anahtar olarak bu anahtarı kullan ve algoritma olarak da bunu kullan
            //çünkü webApının de buna ihtiyacı var
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
            //sen jwt yonetceksın. senin securitykeyin budur algoritman da budur
            //bir siteye parolo ve emaıl ıle girmek gibi hayal et
        }
    }
}
