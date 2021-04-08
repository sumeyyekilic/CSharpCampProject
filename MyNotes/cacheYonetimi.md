# Cache yönetimi
Cache : 
- nedir ? 
- prouct managerda mesela ürünlei listeliyoruz.  buraya gelip [CacheAspect] yazdığımda bunu bellli bir süre cache den gelmesini istiyorum. herhanngi bir kullanıcı bu kodu çağırıysa, ve **o data değişmediyse tekrar tekrar vt a gitmesine gerek yok**.    Veya kategoriye göre getitirken , bir kategoriyi başkası çağırdıysa onu cache lemek ve ondan sonra gelen isteklerde onun cache den getirilmesini istiyorum.
Cache yöntemi :
mcrosftun builtin kendi içinde olan cache mekanizmasını :
- **inmemory cache :** cache lenmek istenen şeyler bellekte  da tutuluyor. bu bellek bizim serverdaki  . eğer cache de o data varsa vt a gitmeksizin o datatyı getiriyoruz. 1. cache içn istediğimiz kadar süre verebilriz. 2. bir ürünün eklenmesi veya bir ürürnün güncellenmesi silinmes, durumund cache inuçurulmasını istiyorum.
	-	bu mimariyi kuacağız. cache lemek istedfiğimiz datayık ey , value ile tutuyoruz. 
	-	key : cache 'a verdiğimiz isim 
	- örneğin , parametreiz metot ve buna  bir key vermek istersen classismi.metotAdı olabilir . yada Business.Concrete.ProductManager.GetAll diyebilriz.
	- birde parametreli olanlar var Business.Concrete.ProductManager.GetById(1) gib. farklı parametrelere göre de cacahe yapabilecek mekanizma oluşturcaz.

--
--
Core tarafında bi aspect yazmadan önce , cache için cross cutting curser klasörü oluştur. :
- Core ---> Cross Cutting Concerns -->Caching   -->**ICacheManager.cs**   : burda kullandığım interface benim bütün alternatiflerimin interface i olacak ! . Yani burada memory i kullanıcam ve eğer yarın buna Redis 'i Elasticsearch'u implament etmek istersem veya Elasticsearch'un, Logstash gelip bunu implemente edebilirim.. Burası teknoloji bağımsız bir interface olacak..
	 - Cache olayında neyaparız ?
		- cache 'e bişey ekleme: 
		- cache den data gtirmek , bu tek bir data da olabilri. bir liste de dönebilir.(farklı veri tipi dönüşü söz konusu)
- ICacheManager.cs 'ın implementasyonu:
- built-in olarak gelen Microsoft'un cache mekanizmasını kullanıcaz. 
- alternatif olarak Redise geçmek istersem,  Core ---> Cross Cutting Concerns -->Caching   --> **Redis** 'e gelip bunun içini doldurmak. aspect injectionı buna göre yapıp bitirmek. yani redisi yazıp injection'ı değiştirdiğimiz anda herşey değişir

- Core ---> Cross Cutting Concerns -->Caching   --> Microsoft ---> **MemoryCacheManager** :ICacheManager implementasyonu  olduğunu söyledim ve implemente ettim..
	- burada microsoftun kendi kütüphanesini kullanıcaz. (IMemoryCache )
	- IMemoryCache  interface olduğu için onu çözmemiz gerekir. çünkü Aspect bağımlılık zincirinin içinde değil.
	- //birisi senden ICacheManager  isterse ona MemoryCacheManager(bizim) ver
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            //yarın ben redis cache istersm burada MemoryCacheManager yerine  RedisCacheManager yazmam yeterli.
-amacım sadece mic un memory cachini eklemek değil. eğer onu eklersem  yarın başka bir cache yönetiminde patlarım. çünkü hard code  her yere `_memoryCache.Set(key, value, TimeSpan.FromMinutes(10));` bunu yazmış olurum. bunu yaparsam başka cache siteminde patlarım.
-ben .net core dan gelen kodu kendime uyarlıyorum :  (MemoryCacheManager.cs de)

- Yukarıda MemoryCacheManager içinde **adapter pattern** ı uygulamış oldukk   :green_heart:   **adaptasyon deseni,** bir şeyi kendi sistemime uyarlıyrorum. (var olan sistemi). diyorum ki ben sana göre çalışmayacağım, sen benim sistemime göre çalışacaksın..

-**serviceCollection.AddMemoryCache();**  //arka planda hazır bir  ICacheManager instanse ı oluşturuyor. Hazır cache yapıları için oluşturulmuş.
 - //RemoveByPattern metodu :Çalışma anında bellekten silmeyi sağlar.
- Reflection: elimizde bir sınıfın instance'i var bellekte ve ona çalışma anında müdahale etmek istiyoruz. Bunu Reflection ile yapıyorduk. Çalışma anında elimizde bulunan nesnelere ve olmayanlarıda yeniden oluşturmak gibi çalışmalar yapabilileceğimiz bir yapıdır. Kısaca kodu çalışma anında oluşturma, çalışma anında müdahale etme gibi şeyleri ""reflaction"" ile yaparız.


AMACIM :  "bir aspect yazayım ve benim için i**lgili metodu daha çalışmaya başlamadan kontrol etsin** ve **cache de varsa cacheden getirsin** **eğer yoksa veriitabanından getirip cache'e eklesin** ve bu şekilde çalışsın" :) istiyorum..


**ReflectedType**  namespace  mesela  **namespace   Business.Concreate**
**ReflectedType.FullName:**    namespace + class ismi veriri. -->  namespace   **Business.Concreate  + IProductService**

her metodu üstüne    [CacheAspect] konulmaz !!

-     
    :star: diyelim ben bi eticaret sitesi yaptım. ürünler filtreleniyor ve çoğunlukla bunlar cache den gelir. çok sık kullanılmayan bi metot vardır birkaç günde bir çağrılıyor ve büyük data döndürüyorsa bu cache'e konulmaz..
    **CacheRemoveAspect**   :    datamız ne zaman bozulur?  yeni data eklenirse güncelelnir veya silinirse. Eğer bi manager'da cache yönetimi yapıyorssan o manager da veriyi manupüle eden metotlarına  :CacheRemoveAspect    uygularsın !!

- cache'i yaparken öemli 2 şey var 
**1**.Manipulasyon yapan meotlarımı cach Aspect ile yönetmek
**2**. business arayuze yazmak hatadır. Business'i tek bir çatıda kullanmak. 
Veritabanına gidip el ile data eklemek problem :D


-   
:fire: Transaction yönetimi : uygulamalarda tutarlılığı korumak için yaptığımız yöntem. ör: benim hesabımda 100tl var. keremi heesabına 10 tl aktarıcam. benim hesabım 10 tl düşecek şekilde update edilmesi, kereminde hesabını 10 tl artacak şekilde update edilmesi gerekir.  yani 2 işlem var. 2 veritabanı işi. benim hesabımdan giderken güncelledi ve keremin hesabına yazarken hata verdi.. burda işlemi geri alması gerekir. Bunu nasıl yaparım ?
( bir şablon oluşturuyorum. TransactionScopeAspect olarak.) 

Aspect oriented programming bir yerden sonra kolaylaşıyor.

**sistem neden yavaş sorunu :)**
- bunun  için "sistemimize yoğun sorgulama operasyolarında, sistemde bir performans zafiyeti varsa sistem bizi uyarsın.." yapabilriiz.

metodun ne kadar süreceğine dair timer koydum : private  Stopwatch  _stopwatch;
- core madule'a Stopwatch eklenir.

- ilgili metoda;
-    [PerformanceAspect(5)]  **//bu metodun çalışması 5sn geçerse beni uyar diyorum.  Buradan performans zafiyetine sebep olan metodu bulmuş olucam.**
- eğer bunu core da  Intercepter ların oldğpu yere koyarsam sistemde her şeyi takip eder :)



-----

-- ** **Reflaction**--  ** **Intercept araştırması**   Dispose ? -- UnitOfWork ?