using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //verdiğmiz password'ün hash'ini oluşturmaya yarar:
        public static void CreatePasswordHash
            (string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac= new System.Security.Cryptography.HMACSHA512()) //BİZİM
            {
                passwordSalt = hmac.Key;  //ilgili algoritmanın o anlık oluşturduğu key'dir. GÜVENLİDİR. anlık üretir
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        //sonrada sisteme girmek isteyen kişinin verdiği password'un ,
        //bizim veritabanındaki passwordHash ile ilgli passwordSalt'a göre eşleşip eşleşmediğini verdiğmiz metot:
        public static bool VerifyPasswordHash
            (string password, byte[] passwordHash, byte[] passwordSalt) // pass işini doğrula
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //oluşan array byte array dir. //hesaplanan hash salt kullanarak yapılıyor.
                for (int i = 0; i < computedHash.Length; i++) // iki arrayin değerleri aynı mı, hesaplanan hash in tüm değerlerini tek tek dolaş
                {
                    if (computedHash[i] != passwordHash[i]) //ikisi karşılaştırılıyor
                    {
                        return false; //eşleşmezse

                    }
                }
                return true; //eşleşirse
            }
        }
    }
    
}
