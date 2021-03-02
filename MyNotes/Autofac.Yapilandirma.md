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