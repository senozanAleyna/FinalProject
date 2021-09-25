using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken //erişim anahtarı olan bir nesne
    {
        //tokenin kendisini ve ne kadar geçerli oldugunu burada tutuyoruz
        public string Token { get; set; } //bir jeton
        public DateTime Expiration { get; set; }// ve bir bitiş süresi
    }
}
