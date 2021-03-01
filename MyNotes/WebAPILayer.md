
## Web Api Geliştirmesi : 

- Amaç : sektördeki doğru implamentasyonu yapmak.
- asp.net : microsoft'un web geliştirme ortamının ve bunları içeren kütüphanelerin yer aldığı ortamının yer aldığı yapıdır.
- .net -> frameworkü,  asp.net ise -> o framework içerisinde geliştirrebileceğimiz proje türünü anlatır.
- **asp.net** .net projelerinde , **web projelerine** verilen isimdir.

**`api :`** bir rest ful mimariyi destekler, restful mimari ile çalışma ortamı sunar.  
- RESTFUL mimari bizim geliştirdiğmiz **.net'i** tanımayan bir django ,java, kotlin , ios uygulamasını bizim sistemimiz ile iletişim kurabilmesini  sağlayan bir ortamdırr.

restful'un karşılığı, django tarafında, python tarafında , java tarafında olabilir.  .NET'te karşılığı asp.net api'dir.


- asp.net core ile yeni bir proje oluşturdum:  **WebAPI**.  (API şablonu: veri transferi için kullıyoruz. Eğer web app seçerek oluştutrsanız, istek sonucu bir arayuz oluşturur
ve tarayıcıya verir. orda herşey server da oluyor. Ama API de data yönetimi api de oluyor bunu client side da kullanıyoruz. 
lient side kim olabiliyor ? React, vue, futter, kotlin, android, gibi olabilir.

- (postman : test ortamım) -- https://www.postman.com/downloads/
- şuana kadar tüm testleri konsolda gerçekleştirdim. backend geliştirme kültürü için testler için unit test yazıyoruz.
- set as startup project : proje o katmanda yada o proje ile çalışır. sanal bir ııs ortamı kurulur. 
----
**controllers** : kontroller diyebiliriz. havalimanındaki kuleye benzetiyorum :) gelen tüm isteklri bu controller karşılıyor ! 

- gelen istek ?  bizim client lar ne yapar ?  mewsela biz bir etic sitesinin mobilini yapıyorum, mobil uygulamada ürünleri listelemek ister,ürünleri fiyat aralığında etirmek isteyebilriiz veya ürün eklemek isteyebiliriz. bunların tamamını birer istek haline getiricez. 
- RestFull yapılar karşımıza HTTP protokolü ile gelir
- HTTP : bir kaynağa ulaşmak için izlediğimiz yol gibi;  
- (Genel bilgi : Embeded yapı isterseniz iki cihazı birbiri ile görüştüreceğizdir mesela kablo ile orada ise  TCP protokolleri karşımıza çıkar)
RESTFUL --> HTTP --> 

- sisteme istkte bulunak istediğinizde controller'da, size yapılabilecek istekleri kodluyorsunuz, tasarlıyorsunuz.  Mesela bizim sistemimize httpGet isteğinde bulunabilirler diyorsunuz.
- Controller bizim uygulamamızı kullanmak isteyen clientlar (djsngo ract vue uygulamarı olabilir ) , bize **" hangi opeasyonlar için ve nasıl istekte bulunabiliri"** 'i kodluyorsunuzz....  mvc yapısında bir web uygulaması yazıldıysa, ilk olarak sizin siteyi açarken controllera istek atılır. ör:  github.com a  enter dediğinizde sizi ilk kontroller karşılar.ve ne yapılacağını orda belirlersiniz.

-----
- clientların biizim controllerımıza istekte bulunabileceği  controller'ı yazacağız.
controllers--> Comman-> API -> API Controller - empty seçilerek

* bir controller ın controller olabilmesi içi onun
  -  **: ControllerBase** 'den inherit edilmesi gerekir. Ve  **[ApiController]**  -> attribute den.
  - " attribute " : bir class ile ilgili bilgi verme ve onu imzalama yöntemidir.
  - yani kısaca burada **" bu class bir controllerdır ve o yuzden kendini ona göre yapılandır"**  diyoruz .net'e  


> ** breakpoint : (breakpoint konulan satırda program durur, bütün global/local değişkenler listelenir. Bunun dışında program durduğu
> anda bir çok detayı görebilirsiniz.) Yazdığınız kodlama tekniğini test
> ile yazarsanız buna ihtiyac duymazsınız..

- root : bu isteği yaparken insanlar bize nasıl ulaşsın ?
	-     [Route("api/[controller]")]
 - web  api uygulamasını çalıştırdığımda beni karşılayan domain:  https://localhost:44362/weatherforecast
 -  mesela bu apiyi  sumeyyekilic.com altında yayınlasaydım : `sumeyyekilic.com/api/products` olurdu.
 - neden products geldi derseniz ? controller classının ismi ProductsController.cs olduğu için.
 - https://localhost:44362/api/products  yazıp enter ladığımda berakpoint koyduğum yere düşüyor ve çılgın selamm mesajım sizi tarayıcıda karışılar :)
--
-web api projesine  "referans olarak " :  Business , Core, Entities, dataAccess ekledim.

  - - json formatı : süslü parantez içerisinde özellik ve karşısında değeri bulucak şekilde bize format verir. özellikler çift tırnak içerisindedir.
- restful mimariler %99.999 json formatı üzerinden ilerler.
- bağımlılığı nasıl kadırırm ?  injection yaparak.

- **şu an sistemimde hiç bir katman diğerini new'lemiyor. veya somut sınıf üzerinden gitmiyoruz.** (console tamamen test içindi.)
-  **dal (dataaccess**)
- **business**
- **api**  : içerisindeyim ve içerisinde herhangi bir dal veya business somut sınıfı yok.
- aynı şekilde manager'a (business layerda) gidince de herhangi bir dal görmüyoruz, soyut dışında.

- çözümlemek demek :  mesela IProductService'e bağlı bir sınıfı new lemek demek.
- IoC Container -- Inversion of Controll   : bir kutu gibi düşün. bellekteki bir yer. bir listenin içerisine **`new PM()` `new efPD()`** gibi referanslar koyayım içine ve kimin ihtiyacı varsa onu ona verelim.   IoC container ile beraber ; ProductController'ın IProductService ihtiyacı varsa,  ben bunu bellekte senin için newledim ve sana onu veriyorum diyorum..
	- yani asp.net web api  bizim yerimize gidip  IoC container'a gidip IProductService  karşılık gelen bişey var mı diyor ve onu kullanıyor. bu bir konfigurasyon yani.


- IoC her türlü proje de kullanabiliriz.
- .net in kendi içinde  IoC container yapısı var.

- Startup.cs 'e configuration service üzerinde :
	- services.AddSingleton  ->  bana arka planda bir referans oluştur. new'lemeyi yapar ve constructor'a verir bizim yerimize.

	- **singleton** tüm bellekte bir tane prodtc manager oluşturu. isterse bir milyon tane client gelsin hepsine aynı instance 'ı veriyor. yani bir milyon tane instance'dan kurtulmuş oldukk :)
	- peki **AddSingleton**' ı ne zaman kullanıcaz ?  içerisinde data tutmuyorsak.
-  services.AddSingleton<IProductService, ProductManager>();
birisi senden IProductService isterse arka planda bir ProductManager oluştur ve onu ver. (yani bu tipte bir bağımlılık görürsen)

---

- .net de IoC yapısı yokken autofac ,ninject ,structuremap , lightInject , dryInject gibi altyapısı sunuyormuş.
