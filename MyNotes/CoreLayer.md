# Core Katmanı

Core Katmanı:  (.net core ile alakası yok)
core katmanını iki katmanda kullansın  

* aspect oriented programming , json web token güvenlik tekniği olabilir,  owasp'a yönelik güvenlik teknikleri , çeşitli utility gibi araçları , loglama cache transaction yönetimi perforans yönetimi kodlarını bunu içerisine koyacağız. 

* asıl odaklanacağım katman burası olacak.

* büyük kurumsal projelerde(altyapı ekibi bu katmanla ilgilenir.)  sistemler için altyapı yazılır ve kuumsal hafızanın kurumsalda kalması ve herkesin kafasına göre kod yazmamasını önüne geçilir.

Hangi katmanla ilgileneceksem o katmanla ilgili klasör koydum: 
1 Dataaccess : altyapımda veri erişimlerle ilgilenmek için. Dataaccess'e hizmete edecek kodlar burada.

* evrensel olarak başka projede de kullanmak için kodlarımı buraya koyacağım.

* bu yuzden DataAccess katmanında ki **IEntityRepository.cs** 'i alıp **Core-DataAccess** içerine taşıdım. 
	
	* isim uzayındaki bozulmalar olacaktır.
	* public interface IEntityRepository<T> where T:class, IEntity, new()  burada ki IEntity -> Entity katmanında ve bu katmana bağımlılık oluşur. bu yuzden `core katmanında` **Entities** adında bir klasör oluşturdum.
	* Ve entity.cs 'i artık buraya taşıdım böylelikle katman bağımlılığım kalktı :)

  
:star: Core standarttır. tüm .NET projelerimde kullanabilirim.

:star: Core katmanı diğer katmanları referans almaz ! 

* eğer başka katmanları referans alırsa , sen o referans aldığı katmana bağımlı olursun. 
* ama ben Core katmanını yarın bir gün, başka projede de kullanabileyim istiyorsam bağımlı olamamalı.. 
* IEntity artık core entities den geliyor..

* Dataaccess katmanı core'a bağımlıdır. (IProductDal, IEntityRepository'i kalıtım alıyor. Dataacces'de core katmanını referans olarak verdim.)



//CodeRefactoring

* Entities katmanına da Core katmanını ref verdim. (isim uzayına ekledim)


* Core katmanında entity framework için evrensel kod yazabilmek adına : **Core -> DataAccess klasörü** iççerisine ->**EntityFramework kalsörü** açtım.
  *	**EfEntityRepositoryBase** classı oluşturdum. Entity, Icontext tipi alıp ona göre çalışacağım. 
  * bu yapı ile artık projelerimde bir tablo oluşturduğumda onun için ekleme güncelleme, listeleme, silme , farklı parametrelere, filtrelemelere göre listeleme kodlarını tekrar yazmicam. Bir kere yazıp her yerde onu kullanıcam.
  * entity framework paketi core katmanına da install edildi.
 