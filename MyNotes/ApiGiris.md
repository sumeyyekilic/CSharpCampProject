# Api Notlarım

### Web API giriş

- bussines katmana ağırlık verip bir web api yazmaya ağırlık verilecek.

Bussiness  --product manager

- WebAPI  (Yazdığımız C# kodu Angular için, IOS için için, Android için, Vue kodlamalarının anlayacağı bir standart. Restfull standartı ile çalışır(JSON türünde).
-web api;  DAccess ve Business ile arayuz arasında köprü sağlar.

- Arayüz, yazdığımız Restfulll servise istek yapması gerekiyor. Yani request(istek). Bizim ona döndüğümüz yanıt ise response'dur.

- Web Api de : Biz istekte bulunan kişiye , yaptığı işlem sonucunda işlemin başarısız olduğu mesajı veya yaptığı işlemin başlarılı old. yapıları oluşturacağız.
	- ve bunu bir defa yazıcaz , tekrar yazmayacağız.

Enkapsülation : birden fazla data döndürmemizi sağlar.

 - core - utilities- result  (core; şirkette tü projelerde kullanabileceğin katman)
 - 
//getter readonly dir. cons da set edilerbilir.


 - c# 'da this demek class demek.
Constants  :proje sabitlerinin bulunduğu yapıdır.
- **//magic string** :  return new ErrorResult("ürünün ismi min 2 karaker olmalıdır!");
-  **magic string** her yerde tekrar ederse ve bir yerden sonra bunu değiştirmem gerekirse öyle standart olmayan mesajlar oluşur projede.
- bunun için geliştirdiğimiz yapı : Business katmanında ->Constants klasörü. (proje sabitlerini(nortwinnd'e özel  vs.. ) bunu içine koydum.) 
	- metinler, mesajlar ,enumlar buraya koyulabilir..
	- messages.cs vsburada yazdım.
	- public static class Messages  //sabit old için **static** verildi.

----
IDataResult  :  hem işlem sonucunu hem mesajı hem de döndüreceği şeyi içeren yapı görevi görecek.

 - bir projede çok fazla enum varsa soyutlamadan kaçılıyordur. suistimal edilen noktalarda denebilir.

 - core'a yazılan şey bir kere yazılır, tüm projelerde kullanılır.
 - 

 - SOLID
	 - I:Kullanmayacağın birşeyi yazma.