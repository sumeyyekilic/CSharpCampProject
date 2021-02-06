## CSharpCampProject

Kurumsal mimari ile gelişltireceğim proje  🎉
**Bir proje kurumsal mimariye nasıl yerleştirilir ?** 

 - Yaklaşık 8 aydır kendimi geliştirmekte olduğum 2 kurumsal mimari
   projesine ek olarak bu projeye kendimden güzel geliştirmeler
   katacağım ek projem olacaktır.  ( Bu projeye ise engin demiroğ önceliğinde ki kamp ile başladım ve Kamp sonunda ilerleteceğim )

**Geliştirmeler**

 - C# .net core
 -  Angular

Veritabanı programlama : otomasyon projelerimim bel kemiğidir.. sigortacılık uygulamalarından bankacılık uygulamalarına tüm sistemler bu verşiitabanları üzerinden ilerler. Bir pastayı kaplamak gibi de düşünebilirz.

**Günümüzde bir bankacılık uygulamasında;**

 - 1 web uygulaması
 - 2 internet bankacılığı
 - 3 mobil bankacılık
 - 4 şube uygulamaları
 - 5 İç uygulamalar
 - 6 iç operasyonlar
 - 7 İş birimi uygulamaları var … kısaca farklı farklı uygulama türleri  var..
Veritabanı programlama yaparken kodlarımızı farklı parçalara bölüyoruz

 - **Nedir bu parçalar ?**


 1. **Data access (veri erişim) Layer** : veriye ulaşmak için yazdığüımız katman.

(yazdığımı kodları katmanalra- layer bölüyoruz)  veri erişim sadece veriye erişim için gereklir olan kodları buraya yazıyoruz….

Neden veri erişim içi n ayrı bir katman iht var ?

C# ile Veri erişim için farklı teknikler vardır. Farklı teknikler kullanma durumu vardır. Birini tercih ediyoruz. Ama yazılım o kadar değişken bir süreci içine alıyor ki ilerleyen zamanla da bu tekniklerden vazgeçme ihtiyacı ortaya çıkabiliyor.. Veya yazılımın bir kısmnı farklı bir teknikle, başka bir kısmını farklı bir teknikle yazma ihtiyacı çıkabiliyor. Zamanla bir teknik çıktığında ve o teknik çok güzelse zaman içinde o tekniğe geçme iht olabilir…

Biz daccess oluşturuyoruz. Mesela x tekniğini kullanıyoruz ve x tekniği kodlarını buraya yazıyoruz. Zaman içinde yeni çıkan z tekniği çıkarsa ve bizim bu tekniğe geçme ihtiyacımız olursa sistme z’yi eklemek istediğimde veya sistemi tamamen z’ye çevirmek istediğimde  diğer katmanlarımız bundan etkilenmez..  Bunu düzen gibi düşünebiliriz. Bunu da soyutlama teknikleri ile yaparız.

Farklı katmanlar oluşturup farklı  katmanlara yazma olayı bize PnP OLUŞTURUYOR. Yani kodlarımızı tak ve çalıştır olarak  kullanabilmemizi sağlıyor J

 2. **Business Layer:** iş kodlarımızı buraya yazarız. İş kodu : en çok if i kulladığımız kuralları yazdığımız yerdir..

İş kurallları yenilenebilir. Her zaman iş birimleri şunu da yapalım diyerek iş akışına müdahale etmemizi gerektirecek kodlar eklememizi gerektirir. Dataaccess de ki kodları iş business içerisine yazmak ileriye dönük çok sıkıntılar yapmamıza yol açacaktır..

Günümüzde farklı ortamlarda da yazılım geliştimek durumundayız…

İos için bir android ürünü olabilir. .net ile ios u direk haberleştiremeyiz. Farklı sistemlerin birbirini anlayabilmesi için bir standart vardır. **NEDİR DERSENİZ?** Bu farklı sistemlerin birbirini anlayabilmesi için “SERVİCE” dediğimiz bir katman daha yazılır.  Bunu karşığı olarak APİ dediğiömiz alt yapıları kullanıyoruz. Yani business i ve ona bağlı dataacccessi APİ aracışığı i,le dış dünytaya açıyoruz.

Kısaca az önce bahsettiğim .net ile yazığım business’a ıos bağlanabiliyor hale gelior J APİ aracılığı ile.. Buda bizim karşımıza RestFul ile çıkıyor (Json aracılığı ile). Api nin önceki versiyonları ise SOAP’dır.

Data access : sadece SQL’ler kullanıyoruz

Business: e tic sitemize kaydolup ürün satak isteyenler için yazzacağımız kuırallar. Mesela yeni bir tedarikçi kayıt olduktan  sonra ilk bir ay max 10 ürün satabilir gibi.

UI' da kullanıcı ile iletişim kuruyoruz. Buradan kullanıcı Business Kurallarından geçer mi kurallarını yöterizi.