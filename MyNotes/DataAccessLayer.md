# DataAccess


**DataAccess LAYER'ı kodlamak**: sql cümleciklerini yazdğm yerdi.

  
:exclamation: Bu illa sql olmak zorunda değil. 

> bir metin dosyasından da data çekiyor olabilirsiniz, mongodb gibi
> NoSQL ortamdan da data çekiyor olabilirsiniz.

**Yazdığım Entities Katmanıına bakarsanız ;**

 - Abstruct klasöründe bir interface oluşturdum  ,
 - Concreta klasöründe VT tablolarımı oluşturdum.

**DataAccess de olay şu;** 

 - **Abstruct** klasöründe öncelikle inerface i oluşturdum. (Yani Product'ın interface'ini oluşturdum.) ne demek? iş yapan klasları
   oluşturacağımız zaman onun bir interface 'i yoksa önce onun bir
   interface'i oluşturulur.(bunu standart hale getirmeliyim)

 - IProductDal : **I**:interace olduğunu, **Product**  : hangi tabloya karşılık
   geldiğini, **Dal**: hangi katmana karşılık geldiğini anlatır(Data Access Layer).

	** Interface 'in kendisi public değil, operasyonları publictir.

 - **Concrete** Klasörü ; da e alternatiff olan x,y,z (adonet, hyper gibi) yöntemleri kullanabilriz.
 - Teknoloji anlamaında alternatif birşey kullanıyorsnız, orada klasörlemeye gidin.  
**Concrete içerisi**e ---> Add new Folder  :   InMemory  ,ve EntityFramework klasörleri oluşturdum..

 - InMemory :bellek demek, InMemeoryProductDal oluşturuldu.(bellek
   üzerinde ürünle ilgili veri erişim kodlarını yazılacağı yer demek: )

*********
 //ctor bellekte referans aldığında çalışacak olan kod blogudur. 
 //global değişken _ ile başlar verilir, referans tiptir.sadece değişkeni oluşturur. tek başına anlam ifade etmez.

        _products = new List<Product> {
            new Product{//control space yaparsan sana alanları getirir}
        };


  
:star2: veri erişim katmanı bu şekilde :)

