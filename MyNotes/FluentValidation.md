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

