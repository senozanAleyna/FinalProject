using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //appsettinsjson'daki bilgileri nesne haline getirmiş olduk.
    public class TokenOptions //bu bir helperdır entity ya dto değil o yuzden çoğul isim
    {
        public string Audience { get; set; } //tokenin kullanıcı kitlesi
        public string Issuer { get; set; } //imzalayan
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
