# Autofac yapılandırması

IoC yapıladırmasını (hangi interface in karşılığı nedir) api startupda yapmak iyi değil. bunu doğru yapmak için yerimiz startup içi olmamalı, daha geriye taşıyabiliyoruz. Bu noktada Autofac yapılandırması oluşturacağız.  
- (Business)  - manage nuget  - **Autofac** ve **Autofac.extras** install edildi..
- BUsiness da ->DependencyResolves
	- bağımlılıklaı burada çözümleyeceğiz.  
	-  interface ler referrans tutucuydu.
	- interface ler imza içeririr. New lenemez bilgileri gerçek hayatta kullanamayız. ama onun referans tutucu old anlar ve ona göre kullanırsak , soyutlama artık kolat bir hal alır.   
	- DependencyResolves içerisinde de  IProductDal karşılığı nedir, IProductService in karşılığı nedir ,  bunu Autofac kullanarak yapılandıracağız. 
	- yani bir teknoloji kullanacağız. (Autofac klasörü içinde de bunları çözüyor olacağız)..

####  bağımlılık konfigürasyonu 
bir defa yazacağız ve istediğimiz yerde kullanacağız.

- AutofacBusinessModule  adında cs oluşturdum :

    public class AutofacBusinessModule : **Module**

**Module** vererek autofac modülü oldupğğunu söylüyorum.

- override space diyince : ezilebilir metodları veriyor.   :heart: buradan Load'ı buluyorum. (uygulama hayata geçtiğinde dockerize ettiğimde uygulama ayağa kaltığında burası çalışacak :heart: )
builder.RegisterType  ---> services.AddSingleton'e karşılık gelir
- `builder.RegisterType<ProductManager>().As<IProductService>();`    // burada birisi senden  IProductService isterse  ProductManager 'ı newleyip ver demek.
- projeni farklı farklı müşteriye satıyorsun diyelim , müşterilerin farklı veritabanlarını kullanıyor olabilirler. 


UseServiceProviderFactory   : servis sağlayıcı fabrikası olarak kullan . neyi ? UseServiceProviderFactory()

- program.cs 'de :
- .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //.net altyapında fabrika olarak autofac i kullan diyorum.
- .ConfigureContainer<ContainerBuilder>(builder =>   //imza olarak builder
            {
                builder.RegisterModule(new AutofacBusinessModule());
            })

- eğer **autofac den vazgeçmek istersem** , yapacağım hareket **DependencyResolves** 'a kendi yapımı kurmak.
ve ikinci olarak da yukarda yazdığım yerdeki sadece program.cs e gidip :  **AutofacServiceProviderFactory** kodunu değiştirmek yeterli. hiç bir yer bozulmaz. 

----

:paw_prints: Katmanlı bir mimari oluşturduk. bunu yaparken öğrendiğimiz konuları nasıl uygulayacağımızı gördük aynı zamanda recap,  yaptığım şeyleri ilerleteceğim eni şeyler ekleyeceğim bir proje oldu. (araba kiralama projesi)
- aop olayına başlamıştık en son.
- business katmanının içinde bir metotta; iş katmanına ait kodlar vardı. yani bizim iş sürecimize ait kodlar. 
- **iş kodları** : işi geliştiren ortamın yönetim tarafından ortaya koyduğu kuralların tümüdür.
- iş kodları api ye yada önyüze yazılmaz.

### Validation (doğrulama)
- productin ekrandan girildiği ürün bilgisinin bizim kurallara yapısal olarak uygun olup olmamasıdır.. 
- meselaa , bir parolanın kurallara uygun olmadığı. bizim eklemeye konu olan nesnemizin yapısal olrak uygun olup olmadığıdır kısaca. 
	- **yapı mı yönetim mi ayrımı?** 
	- **Cross Custting Concerns** : buesiness katmanında business yapılır. fakar katmanlı mimarilerde bu katmanlaraı dikine esen "ilgi alanlarI" var. Bu kavram aslında  cross custting concerns  olarak karşımıza çıkan bir **süreçtir**. 
	- Peki  nedir  cross custting concerns  dediğim ?   
		-	 validation, log, cache , transaction, authorization , performans yönetimi gibi yaygın  cross custting concerns ler vadır. bunlarla ilgili tüm kodları business katmsnındaki o metoda yazmaya kalkarsak bir süre sonra çok karışır. iş kuralları tek satırlık değil çünkü. bazen bankacııık uygulamalarında iş kuralları çok fazla, yani scrool yetmiyor aşağı doğru, okadar uzun iş kuralları olabiliyr :D
- sektorde sıklıkla kullanılan yöntem şudur : (bir uyg yapıcaz , business üzerinde CCS isimli klasör oluştrdm)
- - daha sonra profesyonel bi loglama olacak , şimdilik aop örneği için yazıyorum: ILogger.cs
- loglama  yapılan operasyonların bir yerde kaydını tutmak. kim ne zaman ereye ne ekledi gibi.
-  loglama nerede çalıştırılır. iş metodunun başında bitiminde gibi.
- ILogger _logger 'ı classa ekleriz. ve dependency injection yapmış olurum.

sistem dependency'leri nasıl çözeceğini bilmiyor. :  (DependencyResolves daki Atofac a geliyorum )
-   `builder.RegisterType<FileLogger>().As<ILogger>().SingleInstance();` //eğer senden biri **ILogger** isterse, arka planda oluşturduğun **filelogger'ı** ver,
	- Yukardaki kod ne yapıyor : aslında siz uygulamayı yayına aldığınızda builder ile tanımladığınız her şeyin new'leri ni alıyor. (yukardaki FileLogger gibi). 
	-  yani bellekte ref oluşturuyo. o referasları da ctor içindeki logger arkadaşımıza veriyor :
-  

    public ProductManager(IProductDal productDal, ILogger logger) //ctora ben product olarak ıloggera iht duyuyorum dedim.
            {
                _Ilogger = logger;
                _productDal = productDal;
            }

- buesşness kdlarıyla nası yazıcam: 
- try : uygulamanın düzgün çalıştığını düşündüğnüz yer demek
- birde exc oluşur :  
- interceptors dmek aray girmek demek. metodun sonunda , metot ata verdiğinde çalışmak demek
- virtula metot : senin onu ezmeni bekleye  metot demek
- dolayısıyla biz bir Aspect  nerede çalışsın istiyorsa gidip omum ilgili metotlarını eziyoruz demek. ac demek MethodInterception temel alan ve hangisi çalışın istiyorsan onu içren operasyondur(OnBefore, OnAfter gibi )
invocation : business method.

- Sen bir **MethodInterception** 'sın.. *  madem ki  ben MethodInterception 'ım benim ezmemi istediğin metod var mı :) ? ben sadece before ez diyorsam onu ezer.
- ValidationTool e yapıyordu ? ben ona bir IValisdator veriyordum (IValisdator : bizim ilgili metodun parametrelerini validate etmek istediiğimiz fluent validation metodumuz. yani kurallarımın old metod. ) birde doğrulamam için varlık ver(ör product ).
	- `public static void Validate(IValidator validater, object entity)`    burada **IValidator validater**   **-->** doğrulama kurallarının old class , **object entity -->** doğrulanacak class


	- `public class ValidationAspect : MethodInterception`   **->** ValidationAspect bir MethodInterception. yani kısaca bizim aspect'imiz. metodun başında sonunda hata verdiğinde çalışacak  yapıdır. nerede çalışsın istiyorsan orada çalışr.
	- `protected override void OnBefore(IInvocation invocation)` : validasyon yani doğrulama metodun başında olduğu için burada onBefore'a yazdım.
	- attribute lara type şu şekilde atarız :  **[ValidationAspect(typeof(ProductValidator))]**
	- ValidationAspect deyim
	- defensive coding : savuynma odaklı kodlama diyoruz. bu kod  yazmasak da sis çalışır. ama attribute 'lar typeOf ile çalıştığı için kullanııcı kafasına göre herşeyi yazabilir. typeof(Product) bile geçebilir. Dolayısıyla burada instance göndermiyoruz sadece tipi gönderiyoruz. [ValidationAspect(typeof(ProductValidator))]
	- bi bankada altyapı ekibi sadece bizim core katmanını geliştirir. oradaki ark bunu kullanacak iş kodlamasını yapacak programcı yanlış tip atmasın diye uyarıyor. : mesela :
	-   	   public ValidationAspect(Type validatorType) //bana validator type ver diyor
		       {
		            //defensive code : savunma odaklı kodlama
		            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatör type IValidator değilse o zaman kız! dior
		            {
		                throw new System.Exception("bu bir doğrulama sınıf değil");
		            }
	yukarda , göndermeye çalıştığın validatorType  ,  IsAssignable yani atanabiliyor mu? 
	sonuç olarak yukarda gönderilen şeyin validator olduğpundan emin olduktan sonra `_validatorType = validatorType;`    validatorType'ımız eşitleniyor.

- OnBefore metodu ne yapıyor ?  biz ProductionManager.cs de Add metodunda bir tane  **[ValidationAspect(typeof(ProductValidator))]** ile ProductValidator  tipi göndermiştik.  Ama burda bi instance yok. Bellekte instance yok!  bu newlenmemiş oluyor. O yuzden ;
	- çalışma anında instanse (newlemek) oluşturmak isterseniz ;  **Activator.CreateInstance** kullanırsınız:
		-  `var validator = (IValidator)Activator.CreateInstance(_validatorType); //productValidator newlendi.`
	
		-      //prod val 'ın base metodunki argumanların 0.tipini yakala
                var entityType = _validatorType.BaseType.GetGenericArguments()[0];  
                //metodun argumanlarını gez. invocation(add metodu).
                var entities = invocation.Arguments.Where(t => t.GetType() == entityType);  //validation ın tipine eşit olan parametreleri git bul diyor. birden fazla olabilir
                //eğer ordaki bir tip benim entity type'ıma (product türü) eşitse onları validate et
                foreach (var entity in entities) //tüm params tek tek gez
                {
                    ValidationTool.Validate(validator, entity);
                } 

### CleanCode tekniği
- temiz kod yazma ve  mikroservis mimariği mantığı  üzerine :
-  yönetim : bir ürün eklemek istersen ekelemek istediğin ürünün kategorisinde max 10 ürün olabilir. Bunun için bir kural yazdsım :
- do not repaet your self!
- iş kurallarını  şu şekilde yazarsak , istediğiniz kadar katmanlı mimari kullanının spagetti kod olacak.:  
-  

    //bir kategoride en fazla 10 ürün olabilir : BU YÖNTEM YANLIŞ :) çünkü iş kuralı ve başka iş kuralları da olabilir.update yaparken de bu kural geçerli çünkü.. 
                //iş kuralı kategoride 15 ürün olabilir diye değiştiğinde gelip burrda değişitirmem kötü bir kodlama. yazılımcı kendini tekrar edecek. ve updatee metodunda bunu güncellemeyi unuttuysa hata yapar.
                var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count; //o kategorideki ürünleri bulursun
                if(result >= 10)
                {
                    return new ErrorResult(Messages.ProductCounOfCategoryError); 
                }

- daha algoritmik bir yapıya iş kuralına ihtiyacımız olduğunda bunların işlenmesi gerek.
yukardaki kodu quick action refactirining seçip 
-  mevcut kodları analiz edip 

- #aynı isimde ürün eklenemez  (bizim eklemeye çalıştığımz ürün ismi daha önceden veritabanında varsa onu ekleyemez. )

		 private IResult CheckIfProductNameExist(string productName) // kategoride ki ürün sayısının kurallara uygunluğunu doğrula 
        {
            //aynı isimde ürün eklenemez: 
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();  // any() uyan kayıt var mı ?
            if (result)//eğer böyle bir data varsa
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists); //ProductNameAlreadyExists : böyle bir ürün zaten var demek
            }
            return new SuccessResult();
        }

### iyileştirmeler
-  iş kurallarını bir standarta göre yazdık hepsi IResult dödürüyor. Birtane iş motoru yazsak, oraya iş kurallarımızı göndersek.  . Bunun için **polymorphism** (polimorfizm) kullanabiliriz.  amacım bu iş kuralllarını bir yere göndereyiim, o benim iiçin kaç tane iş kuralı gönderirisem onları çalıştırsa. 
- Bunu hangi katmana yazmalıyım ? tüm projelerde çalıştırılabilir yapı old iiçin Cora'a yazmalıyım.
	- `core-->Utilities  -->Business(add) --> BusinessRules.cs` oluştrdum... buraya standart bir hata döndürme metodu yazdım : 
	- 

	   //buraya iş kurallarını  gönder
	    public static IResult Run(params IResult[] logics) //params IResult[] logics  : params verdiğimizde run içerisine istediğimiz kadar parametre gönderebiliyoruz. gönderdiğiniz tüm parametreleri array haline getirip IResult arrayi olan logics'e gönderiyor.
	    {
	        //parametre ile gönderilen iş kurallarından başarısız olanı business'a haberdar ediyoruz.
	        foreach (var logic in logics)//her bir  logic için iş kuralını gez
	        {
	            if (!logic.Sussess) //logic'in succes durumu başarısız ise
	            {
	                return logic;
	            }
	        }
	        return null; //başarılı ise birşey döndürmesine gerek yok.
	    }
	-	Daha sonra Product managerda  bu iş motorunu çalıştırabilirim : aşağıdaki yapıya istediğim kadar iş kuralı ekleyebilirim :

		- **BusinessRules.Run(CheckIfProductNameExist(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId));** // iş kurallarını çalıştıracak. isterse bin tane iş kuralı olsun

 ----
 - overdesing : aşırı tasarım . sistemler %100 solid olamaz. problemi.
* 