using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //Api'deki appsetting.json'u okumamızı sağlar.
        private TokenOptions _tokenOptions;//appsettingdeki tokenoptions kısmını okuyacağım ordaki değerleri bu nesneye atıcam
        private DateTime _accessTokenExpiration;//tokenin geçerli olacak süresi için
        public JwtHelper(IConfiguration configuration)//konfigürasyonu enjekte ettik
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//konfigürasyondaki değerleri bul
            //appsettings'içindeki tokenoptions section(bölümünü) al ve onu şu sınıfın değerlerini alarak eşleştir (maple)

        }

        //user ve claime göre bu kullanıcı için bir tane token üretiyor olucaz
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); //10 dk sonra sonlancak
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);//token oluştururken güvenlik anahtarına ihtiyacım var
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//hangi anahtar(security key) ve algoritmayı kullanıcam
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//bunları kullanarak token oluşturucam
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);//elimde token bilgisini yazdırıyorum

            return new AccessToken //artık bir access token döndürmem gerekiyor
            {
                //bunun zaten iki tane bilgisi vardı
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        //token optionları kullanarak ilgili kullanıcı ve creditialsı kullanrak bu adama atanacak yetkileri içeren bir tane jwt döndürür
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        //claimlerin (yetki ve daha fazlası)oluşturulması
        //jwt nin içinde sadece yetkiler olmaz bir çok şey daha var
        //claim .net in içinde olan bir şey biz ona yeni metotlar eklicez
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            //bana bu parametreleri ver ben ona göre claim üreteyim
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}"); //başına dolar koyunca string içine kod yazabiliyorsun
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());//rol ekleme
            //normalde claimde addemail vs yok bu yuzden extension hazırladık
            return claims;
        }
    }
}
