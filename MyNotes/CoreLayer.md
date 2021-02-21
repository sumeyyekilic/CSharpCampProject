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