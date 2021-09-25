using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //ona verdiğimiz bir passwordun hash'ini olusturur
        //buraya bir password yollayacağız, diğer ikisini sistemden çıkartacağız
        //out yapılan değişiklikleri sışarı cıkartabılır
        public static void CreatePasswordHash
            (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {   //kriptogrif bir sınıfa karsılık gelir (terim)
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            { //her kullanıcı için başka bir key olusturur.
                passwordSalt = hmac.Key; //hmacde bulunan saltı verdik
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//bayt şeklinde ister
                //verdiğimiz passworde göre salt ve hash olusturduk.
            }
        }

        //bu iki metod birbiriyle eşleşecek

        // password hash'ini doğrular.
        //burada out'a gerek yok cunku bunları bız verıcez
        //bu kullanıcının passwordunu yıne aynı algoyu kullanarak hashleseydın karsına aynı sey cıkarmıydı
        public static bool VerifyPasswordHash
            (string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) //doğrulama yapcagı ıcın saltı ıstıyor
            {
                //hesaplanan hash bu saltı kullanarak gerçekleştiriliyor.
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) //iki hash i karsılastırcaz
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
