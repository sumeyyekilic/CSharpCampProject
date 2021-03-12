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
Core tarafında bi aspect yazmadan önce , cache için cross cutting curser klasörü oluştur. :
- Core ---> Cross Cutting Concerns -->Caching   -->**ICacheManager.cs**   : burda kullandığım interface benim bütün alternatiflerimin interface i olacak ! . Yani burada memory i kullanıcam ve eğer yarın buna Redis 'i Elasticsearch'u implament etmek istersem veya Elasticsearch'un, Logstash gelip bunu implemente edebilirim.. Burası teknoloji bağımsız bir interface olacak..
- - Cache olayında neyaparız ?
	- cache 'e bişey ekleme: 
	- cache den data gtirmek , bu tek bir data da olabilri. bir liste de dönebilir.(farklı veri tipi dönüşü söz konusu)