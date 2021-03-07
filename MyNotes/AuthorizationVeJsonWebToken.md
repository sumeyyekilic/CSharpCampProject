# Authorization , Web Token, Güvenlikli yapı oluşturma Üzerine


### Güvenlik Üzerinee

- api ' mize bir json web token özelliği kazandırıcaz. bunu businesss tarafında aspect ler ile yöneticez.  

- özellikle api tarafında bulunan  authorize (asp.net core den gelen de bir alışkanlık ) yapılarını kullanmıcaz.
- yetkilendirmeyi api tarafına taşırsanız , tamamen yetkilendirme süreçleri api tarafında kalmış olur . buda başka api veya başka kulllanımlarıda yetkilenrmeyi yeniden ele almamıza sebep olır.  oyuzden yetkilendirmeyi daha geride gerçekleştirmeliyiz. 
- Bu yapacağımız yetkilendirme, bizim web uygulamalarımızda, android uygulamalarımızda veya masaüstü  uygulamalarımızda kullanbileceğimiz bir yöntem olucak.


### JSON ve Web Token bazlı bir güvenlik mekanizması kuracağım.
-  yetkilendirme altyapısınıı bir kere kurup ondan sonra yazılım hayatımızda rahat edebilmek için  altyapıyı kurana kadar kod yazmak gerekiyor..
- konsept : şimdiye kadar aspect yapmıştııkk.
-  **Authorization** da bu sürecin içinde olabilecek bir **cross cutting concerns** 'dür. yani uygulamayı dikine kesen ilgi alanıdır. 
- Validation aspect gibi bizim bunu doğrulama sürecinden geçirmemiz gerekir.
- Mesela :
- **[SecuredOperation]**  :  benim verdiğim bir aspect ismi yani koruma operasyonu.  
-  **SecuredOperation** 'ı çağıracak kişinin mesela "admin yetkisine sahip olması gerekiyor olabilir veya editor yetkisi gibi.."
- yetkilendirme olayı , bizim ihtiyaclarımıza göre , uygulama türüne göre değişen bi yapıdır. 
- mesela bazı uygulamalarda bikaç rol ; admin editor moderatörler gibi yetkiler yeterli olurken ,
-         [SecuredOperation("admin , editor")]
- bazı uygulamalarda operasyon bazında yetkilendirmeler yapmamız gerekebilir. mesela  **product.add** yetkisine sahip olması gerekicek gibi.
-         [SecuredOperation("product.add")]
	- - product.add benim verdiğim bir anahtar. bu anahtar dediğim yapılara **Claim** denir. **claim(iddia etmek demek)**

dalışş...
bir apimiz var  ---- bir client'ımız var(bizim her tarayıcımız client'dır)----
bir operasyon yetki gerektiriyorsa ;  mesela ürün ekleme operasyonu. api bazlı uygulamalarda **json web token** dediğimiz  json bazlı bir  tokendan yararlanıyoruz.
- aslında client ilk etapta istekte bulunuyor.  eğer herhangi bir tokenı yoksa bizim uygulamamız ona senin bir tokenın yok diye http response dönecektir.  
- daha sonra client şöyle bir kullanıcı adım, parolam var, bana token ver derse, apimiz şuna bakacak : clientın gmail ve parolası doğruysa ona json Web Token veriyor olacaktır.  
- client bu json web tokenı, kend hafızasında bir yerde tutuyor olacak. (cookie, mobilde bir yer olabilir gibi..)
- client bundan sonra tüm isteklerini yaparken; örneğin bir ürünekleme işlemi yapacaksa http istek zarfının içine JWT + PRODUCT ile birlikte istekte bulunur.
- Api de bakar bu JWT sahip olan kişiyi veritabanından kontrol eder, böyle bir işlemi yapmaya yetkisi varsa ona göre olumlu veya olumsuz cevap döner.
---
- json formatlı bir metindir. süslü parantezlerle varlık ve karşısına da değerini yazıyor. herkes bu formatı tanır ve okuyabilir. Biz bu formatı dzügün verdiğimizde  bu jsonı angular, react, mobil , desktop gibi uygulamalar bunu tanıyıp okuyabilir...

----
### Güvenlik üzerine kavramsal konular :
- **Encryption  ,  Hashing :** 
- Yukardaki kavramlar bir datayı karşı taraf okuyamasın diye yapılmış çalışmalardır. ör kullanıcı parolalrını vt da açık da tutabiliriz. ama açık tutmak yerine Hash'leriz.
- farkını bilmek gerekir.
- mesela veritabanında parolalrı 1234 gibi açık açık tutmayız. bu güvenlik açığı olur. Bu veri kaynağına birileri ulaşabilir, veya bizim projelerimizde çalışan kişiler görebilir. Oyuzden bu parolalarını bir yerde tutabilriz. Genellikle bu parolalrı haslediğimizde ;
- veri dediğmiz şey 123@123  parolası bir şifreleme algirotması vasıtasıyla  MD5, SHA1 gibi algoritmalarla geri dönüşü olmayacak şekilde hash'ledim.  atıyorum şifreti : BDX3-5gdhsa-dsadfklsaj gibi tutmaktır. 
- - MANTIK : KULLANICI EKRAN ÜZERİNDEN GMAİLİNİ  VE PASSW GİRDİ . BU DATA BİİZİM VERİTABNINA GELDİ VE parolayı eksik girdi . aynı şifreleme alg ile bu datayı şifreliyoruz ve ilgili kişinin bu mailine ait böyle bir hashi var mı yok mu kontrolü yapıyoruz. Hash karşılaştırması gerçekleştiriyoruz.
- hash dataları döndürülebilyor gibi yanlış  bilgiler var. bir saldırgan bir veritabanındaki hash datasına ulaştığında onların ne olduğunu bulabilmek için , kendine gökkuşağı tablosu oluş. ve olabilecek herşeyin hashini oraya koyuyor. Geri döndürme alg ile de  hash lenmiş bir dataya ulaşılabilir ama bu yıllar alır. oyuzden kullanıcıdan güvenli bi parola girmesini istiyoruz. 
- abc.ABC@!454*/-  gibi bir şifre istersek gayet güvnli olr.

- Salting : tuzlama. kullanıcının girdiği parolayı biz biraz daha güçlendiriyoruz.
- **Encryption** : geri dönüşü olan veridir.  öR: sümeyye kılıç verisini encrypt ettiğimizde , aslında **verinin tamamını** encript  etmiş oluyoum.
- Hash de veriyi hash lemiyoruz. girilen veriye göre bir data oluşuyor ve oluşan datanın karşılığı o veri değil. 
- Ama Encryption 'da ilgili datamız encrypt ediliyor. bunu Decrypt ' de edebiliyoruz. 
- **Decrypt**: çözmek
	- Encryption  'ı evin dışında gibi düşün. Anahtarın yoksa o eve giremezsin.  evden çıkarken kapıyıitlersiniz ve eve girerken de aynı anahtarla kapıyı açarsınız. ev aynı ev :)
----
Bu sistemde kullanıcıların Claim leri ve bu kullanıcıları Claim'lerle ilişkilendireceğimiz 3 tane tablo ihtiyacım. var :

- Norhwind veritabanına : 
	- 1.Users isimli bir tablo ekleidm: 	id'yi otomatik artan yapıda : **IsIdentity  True** 
	- 2.[OperationClaims] tablosu : ıd burada da **IsIdentity  True** . (projede oluştrduğum tüm claimler bunun içinde oluşturcaz.) mesela admin editor moderator user gibi rolleri burada oluşturcam. bunlar dışında metot yetkilendirme seviyesi de burada yapılacak.
	- 3.[UserOperationClaims] : o kullanıcının operasyon kayıtlari.

-  bunların Entity'leri : user claim gibi şeyleri normalde entity katmanında oluşturabilriiz normalde.  ama bizim bu noktada oluş varlılar bütün projelerimizde kullanılabilri. o yuzden yetkilendirmeyle ilgili bir sistem yazıcaz ve bunu tüm projelerimizde kullanabileceğiz :)

-----
###  ilgili vt 'a karşılık gelen entity'leri oluşturma :
- **Core -> Entiities ->Concrete  ->   User.cs**  (VT daki alanlarımı burada da oluşturdum.) (class'lar tekil olur.)
-  **Core -> Entiities ->Concrete  -> OperationClaim**
- **Core -> Entiities ->Concrete  ->   UserOperationClaim**

-----
**appsettings** : proejnin ayarlarını öl leri kontrol ettiğim yer. collection stringler, loglama ileilgili konf koyulmuş. Bizde  json web token konfigurasasyonu koyacağız.

    {
      "TokenOptions":
       {
    		    "Audience": "sumeyye@sumeyye.com",
    		    "Issue": "sumeyye@sumeyye.com",
    		    "AccessTokenExpration": 10,
    		    "SecurityKey" :  "mysupersecretkeymysupersecretkey"
      },
      ...
      }

 -  **Audience**  :kitle demek ,**Issue** : bunu uygulayan , **AccessTokenExpration** : bizim verdiğimiz token'ın dakika cinsinden geçerlilik süresi, **SecurityKey** :tokenı oluştururken asp.netin kullanacağı anahtarın ismi.(belli dönemlerde bunu değiştirin--uzun olması iyi)
