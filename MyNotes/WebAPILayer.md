
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
