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
    //ITokenHelper :ilgili kullanıcı için ,  ilgili kullanıcının claim'lerini içerecek bir token üretecek : 
    public class JwtHelper : ITokenHelper  
    {
        public IConfiguration Configuration { get; }  //IConfiguration bizim apimizdeki appsettigs 'i okumama yarıyor
        private TokenOptions _tokenOptions;  //TokenOptions : IConfiguration ile okudğum appsettigs deki değerleri bir nesneye atayacağım. TokenOptions diye bir nesne oluştur. 
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //benim değerlerim conf daki alanı  bul. appsetting içindeki token options bölümü al.ve onu TokenOptions sınıfın değerlerini kullanarak maple.

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);// 10 dk sonra aktif olcaktı
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);  //ilgili Credentials kullanarak claimleri içerek  metot : CreateJwtSecurityToken
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken( //gerekli bilgiler oluşturuluyor
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),  //Claim ler için de bir metot yazılmış.
                signingCredentials: signingCredentials
            );
            return jwt;
        }
        //yetki , hatta yetkiden daha fazlası. json web tokenda kullanıcıya karşılık gelen biçok şey burda. 
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}"); //iki stringi yan yana göstermek.
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray()); //kullanıcı rolleri name leri çekip array'a basıyorum

            return claims;
        }
    }
}
