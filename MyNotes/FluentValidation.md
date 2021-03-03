# Fluent Validayon ve AOP ile buunun yapısını kurma

## Fluent validasyon
- aydınlatıcı bir teknik :) uygulayıp AOP ile birleştircem.

- **validasyona neden iht var ?**
- ProductManager Add metodunda  : ürün isminin min 2 karakter olmasını söylemiştik. burada biraz validasyon yapacağız.
- **Validasyonla iş kodu ayrımı iyi yapılmalıdır** .
- **Validation** ( **doğrulama**)  eklemeye çalıştığınız product'ı iş kurallarına dahil etmek için, bu nesnenin yapısal olarak uygun olup olmadığın ı kontrol ediyor.
	-  şifre şuna uygun olmalı, isin min 2 kr olmalı gibi kurallar o nesnenin , productın  , product için girdiğiniz verinin yapısal uyumu ile alaklı olan her şey doğrulamadır.
- Business code (**iş kuralı**) ise bizim iş ihtiyclarımızın uygunluğuduır.
	- Mesela bi bankada kişiye kredi veririken,  kişinin o kredi almaya uygun olup olmadığı kontrolü **iş kuralı**nda yapılır. ÖR; kişinin finansal puanına bakmak iş kodudur.  Buraya eklenecek nesnenin yapısıyla ilgili olan şeyler ise  validation'dır.
- validasyonları iş kodlarının arasına yazmaktansa, belli bir noktadan verebiliriz.
- yanlış kullanım: [Required]  [MinLenght(2)]
- (Single resp. :tek sorumluluk
- validasyonları entity e klersek SOLİD'e aykırı iş yapmış oluruz.
- Business içerisinde ValidationRules adlı klasör oluştrdm.
	-  Validation için bir paket kullanacağım.  : **FluentValidation**  (Manage nuget packages 'den de FluentValidation yükledm.)
		- Product için bir validasyon yazacağım için ona ProductValidator.cs dedim.
		- doğrulama kurallarını nasıl yapacağız.
		- :**AbstractValidator**  , FluentValidasyon'dan geliyor.   `public class ProductValidator :AbstractValidator<Product>`
		
		- bu kurallar bir constructor içerisine yazılır.
		- - RuleFor : kim için kural ? verdiğimiz  'Product> nesnesi için.
		- Fluent in güzel yanı , unit price 0'dan büyük olmalı diyorum . ama içecek kategorisinde ise içecek kat ürünlerinin fiyatı min 10 ollmalı : 
		- >     RuleFor(p => p.UnitPrice).NotEmpty();
		  >                 RuleFor(p => p.UnitPrice).GreaterThan(0);
		  >                 RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1); 
------------------------
-ProductManager.cs de yazdığım bu yaapı aslında standart

    var context = new ValidationContext<Product>(product);
                ProductValidator productValidator = new ProductValidator();
                var result = productValidator.Validate(context);
                if (!result.IsValid) //sonuc geçersizse
                {
                    throw new ValidationException(result.Errors);
                }

Business tool haline getirmek ve tüm projelerimde bunu kullanmka istiyorsam, core katmanına gidip orada yeni bir

Cross cutting Concerns  farklı katmanlarda farklı versiyonları yapılabilir . nelerdir bunlar : cache , transaction. katmanlı mimarilerde bu yapılara Cross cutting Concerns diyoruz. 

Core katmanında : Cross cutting Concerns klasörü oluşturdum onun içerisine de cache yapacaksam cache klasörü, gb gbii. yapı oluşturacağım.

-aop metotlarımı loglamak istiyordum. log ne zaman yapılır :  bir metoto ya başında ya sonunda yada hata verdiğinde loglanır. burada çalışmasını istediğim kodları AOP ile desing edebilirim. her yerde **try cache** demek zorunda kalmam yada her yerde log log log gibi demek zorunda kalmam.. :)
bu yönteme **interceptors** denir . **"araya girmek" demek.**

attribute : log cache gibi şeyleri biz metodun üstüne attribute olarak koyuyoruz . attribute lar sen bi kodu çağıracağın zaman git üstüne bak belli kalıba uyan log gibi kurallar var mı varsa bu metot çalışmadan önce loglama kodları çalışacak diyoruz.   class lara metotlara anlam yükleyen yapılardır.


- Core katmanına ;  **Autofac , Autofac.Extras.DynamicProxy**  install ettim . AOP yapısını sağlayacak olan altyapı bu arkadaşlardan geliyor. 

- Priority :öncelik demek. mesela önce loglama gibi..
-  **OnBefore** : metodun başında çalıştırır.
**OnSuccess** : metot başarılı old çalışsın
**OnException**  : hata aldığında
oNaFTER : METOTDAN SONRA ÇALIŞSIN

- %90 ONBEFORE VE ONECEPTİON kullanırız..
- heryere gidip kodları spagettiye çevirip her yere try cache yazmamak için bu yapılır..
- kendime temel bir **try cache** altyapısı oluşturucam. yukardakilerden hangisini doldurursam o çalışıcak.
- bizim metotlarımzın çatısı burasıdır. Getall çalışrrıyor gibi mettotlarımın hepsi bu kurallardan geçecek. buraya dahil olacak. (aspect de hangisini doldurrusam o çalışacak)

--
- cross cutting yapmak :
	- Aspect -Autofac - Validation klasörleme yaptım bunn altyappısı için
-	reflaction çalışma anında birşeyleri çalıştırabilmemizi sağlar. mesela birşeyleri newlemeyi çalıştırma anında yapmak istiyorum 
-	**Activator.CreateInstance(_validatorType);** bu bir reflection.
-	invocation metot demek.. unutma!
-	iş katmanı ADD metodumda validasyon yok ama  ; Aspect ekledim şu şkeill:  **[ValidationAspect(typeof(ProductValidator))]**



Tekrar Business Autofac içerisinde : register lar bizim kayıt ettiğimiz sınıflardır.
- Autofac sizin tüüm sınıflarınız için, önce gidi bak acpecti varmı  diyor. aynı zamanda intercapter görevi görür.
