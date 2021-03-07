# Auth. Security yapısı

-core katmanına ->Security yapısı oluşturdum (Hashing, Encription , JWT)


1. **Core ->Security -> Hashing ->** **HashingHelper.cs** oluşturdum
-  Bir hash'leme aracı yazdık. bundan sonra parola has leme ihtiyacımız olusa bu durumda bunu kullanabilmek için . (HashingHelper)
- bir şifreyi çözerken salt lazım oalcak.
		

         //verdiğmiz password'ün hash'ini oluşturmaya yarar:
           public static void **CreatePasswordHash**
                (string password, out byte[] passwordHash,out byte[] passwordSalt)
            {
                using (var hmac= new System.Security.Cryptography.HMACSHA512()) //BİZİM
                {
                    passwordSalt = hmac.Key;  //ilgili algoritmanın oanlık oluşturduğu key'dir. GÜVENLİDİR. anlık üretir
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
            }
	- **CreatePasswordHash** : ona verdiğimiz bir password'ün hash 'ini ve salt'ını soluşturacak. (out dışarıya verilecek değer gibi düşün)
	- .net in cryptographysınıfların yararlanıp  algoritma seçip ona göre hash değerimizi oluşturucaz. Bunuda disposable pattern ile hızlıca yapıcaz..
- **System.Security.Cryptography.HMACSHA512()**      ---> Burada HMACSHA512 gibi bir sürü alg var. ezberleme kafasına girmeyin, ne yaptığını bilmek  yada kullanırken araştır. :)
- password parametrsinin(string) ,,  byte değeri şu şekilde alınır : 
	- `Encoding.UTF8.GetBytes(password)`
-  //sonrada sisteme girmek isteyen kişinin verdiği password'un ,
        //bizim veritabanındaki passwordHash ile ilgli passwordSalt'a göre eşleşip eşleşmediğini verdiğmiz metot:
        
-     public static bool VerifyPasswordHash
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

 2. **Core ->Security -> Encryption->**SecurityKeyHelper.cs**
işin içinde şifreleme olan sistemleride, bizim herşeyi byte array formatında oluşturmamız gerekiyor. basit bir String formatı ile key oluşturulmuyor.. asp.net web token servislerinin 'ın anlayacağı dile getimem gerekiyor.
- byte'ını alıp onu SymmetricSecurityKey haline getiriyor.
- json web token ımızın ihtyaç duyyduğu yapılardır.
	 - core katmanına using Microsoft.IdentityModel.Tokens;

**SigningCredentialsHelper.cs** : json web token sisteminin yönettilebilmesiiçin bu metoda ise güvenlik anhtarını verip,  algoritmasını veriyoruz.
**credential** : elimizde olanlar , anahtarlar. **kulllanıcı giriş bilgilerin.**


note://if veya for yazmak demek orda kötü bişey yapıyorsun demek değil. suistimal etmek kötü koda girer..
3. json web token için utilities : **Core ->Security -> JWT->**AccessToken.cs**
Access token : erişim anahtarı. alamsız karakterlerden oluşur. (jeton gibi)