# Entities Katmanı

Entities katmanı: bizim tüm domain'imize hizmet eden objeleri içerir.  (yardımcı katman gibi düşünebilirsiniz.)

Basit bir obje ile başlamıştık ;  Product ve Category ile.

 - Category de kategorinin ismi ve id'si
 
- Product'da 5 özellik vererek bu süreci tamamladk.

 - **Herhangi bir class çıplak kalmasın**   kurali ile Category veya Product nesnesinin bir VT nesnesi olduğunu anlatmak üzere bir imzalama gerçekleştirdik. 
 
- Bu imzalama  **`IEntity`** ile yapıldı.   
	
	:yum:Bu tamamen mimariyi yazan kişinin yoğurt yiyişidir... farklı şekillerde İnterface imzaları olabilir.(base tip gibi..)


### DataAccess içerisinde çalışıyorum. 
- IProductDal ' a bakarsanız ekleme silme güncelleme operasyonları oluşturmuştuk..  [bakınız]()
bir tanede ICategoryDal oluş istiyorum.

- Abstruct klasöründe Product gibi Category'ninde interface'i oluşturuldu..  [bakınız]() 

 - Farklı katmanların kullanacağı nesneleri public veririz.
IProductdal için yaptığımız çalışmayı , ICatogorydal için yaptım. Burada generic ifadeler var. bir generic alt yapı kurabiliriz.  gidip tüm nesnelere yazmak yerine bunlar için bir İnterface oluşturulabilir. GenericT gibi yapıyı kendimiz kurabilriiz.Bu yapınınn adı ; `generic repository desing pattern` 'dir.

 -      IProductDal ve ICategoryDal içöeirisindeki operasyonlar kesildi. ve IENTİTYrEPO 'YA koyuldu.
---
### Expression Kullanmak :
    Expression<Func<T,bool>> filter=null
 Yukarıdaki kod bir defa yazılır ve bir daha iht olmaz.. 

 - (p=>p.CategoryId==2);

 bu kodu yazabilmemizi sağlar .
 - ne işe yarar? 
	 -  filtreler
   yazabilmemeizi sağlar.
- nasıl yani ? 
	- eticaret uygulamasında koyulan filtreler gibi aslında.

### Entities içerisinde 

 1. Concrete içerisine gidip `Customer.cs` oluşturuldu,
 2. DataAccess 'e gidip Abstruct -> addClass diyorum ve `ICustomerDal` olarak.

Sizde bu şekilde sistemi basit şekilde standartize edebilirsiniz.


* - * - * 

#### **Concrete üzerinde :**
EntityFramework Klasörü oluşturuldu.(alternatif sistem)
İçerisine ; 
- `EfCategoryDal , EfProductDal` adında iki class oluşturuldu.  
- bu classs'ların içerisi entity framewokre göre kodlanacak

- Concrete klasörü içerisinde VT tablolarımı tutyorum.

## EF

Entity Framework , microsoftun bir ürünüdür. ORM dediğimiz bir ürüünüdr.
 linq destekli çalışıyor.
 - vt daki tabloyu sanki class'mış gibi onunla ilişkilendişrip, tüm operasyonarı yani sql'leri linq ile yaptığımız bir ortamdır.
- `ORM`;  vt nesneleri arasında bir bağ kurup veritabanı işlerimi yapma sürecidir..

#### Entity Framework siteme nasıl dahil edilir ? 
- şimdiye kadar c# ın .net içerisindeki implamentasyonlarını kullandık.
- ilerledikçe başkalarını yazdığı (paket) kodları kullanacağız.
- bunların ortak koyulduğu ortam ise Nuget'dir.

- Data ACCESS katmanı üzerinden sağ tıklayıp ; manage paket nuget deyip, Entity Framework eklendi. (Microsoft.EntityFrameworkCore.SqlServer)

* - * * - * 
 1.  VT ile benim nesnelerimi ilişkilendirme adımı : bunu yapabilmek için context yapı kurmak gerekiyor. 
*Context  demek DB tabloları ile proje classlarımızı ilişkilendirdiğimiz class'dır. 

//db ismi :Northwind

 - DataAccess Katmanı altındaki *EntityFramework* içerisine
   NorthwindContext adında bir class oluşturdum. bunu da :DbContext 
   baz-se sınıfından kalıtıyoruz. bizim context'imizin kendisidir.

> //OnConfiguring : senin projen hangi vt ile ilişkili'yi belirteceğim
> yer!!