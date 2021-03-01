using DataAccess.Abstract;
using DataAccess.Concrete.DTOs;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    //InMemoryProductDal : bellek üzerinde ürünle ilgili veri erişim kodlarını yazılacağı yer demek
    public class InMemoryProductDal : IProductDal  //IProduct dal eklendikten sonra ,"Implement Interface"i eklenir. Çünkü bellekte çalışırken yazdığım kodlar farklıdır, entity framework kulanırken yazacağın kodlar gerçek veritabanında farklıdır 
                                            //şuan bellekte çalışacak şekilde IProduct dalı kodlayacağım.
    {
        //içerisinde ürünleri barındıran değişken
        List<Product> _products;  //global değişken _ ile verilir, referans tiptir.sadece değişkeni oluşturur. tek başına anlam ifade etmez.
        public InMemoryProductDal() //ctor bellekte referans aldığında çalışacak olan kod blogudur. 
        {
            _products = new List<Product> {
                new Product{ProductId=1, CategoryId=1, ProductName="Bisiklet",UnitPrice=1500,UnitsInStock=15},
                new Product{ProductId=1, CategoryId=1, ProductName="Klavye",UnitPrice=150,UnitsInStock=3},
                new Product{ProductId=1, CategoryId=1, ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=1, CategoryId=1, ProductName="Masa",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=1, CategoryId=1, ProductName="Bardak",UnitPrice=15,UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //_products.Remove(product); //Bu kod çalışmaz. listeden asla silemezsin: çünkü arayuzden gönderdiğim productın bilgilerin aynı olması önemli değil
                                         //heap de 5 tane adres var. bunların değeri aynı ref numarasına sahip değildir.
                                         //id si aynı bile olsa bunun referansı 200 olursa vt gidip 200 ü silmeyi yapamaz. string veya bool silerdi ama referans tipi silemez
                                         //ürünleri silerken primary key'ini kullanırız. id herzaman farklıdır.
                                         //döngü ile tek tek dolaşıp önce productı dolaşıp sonra gönderdiğim  ürünün id si ile eşleşiyor mu diye bakmam lazımdı. Linq bilirseniz buna gerek kalmaz:)

            //Product productToDelete = null;
            //foreach(var p in _products)
            //{
            //    if (product.ProductId == p.ProductId)
            //    {
            //        _products = p;
            //    }
            //}

            //LINQ(Language Integrated Query) c# i güçlü kılar.
            //SingleOrDefault tek bir eleman bulmaya yarar.  p=> buna lambda işareti denir.
            //her p için gidip bak , p nin product id'si benim gönderdiğim p Id'sine eşitme? eşitse ; productToDelete'e eşitle
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);  //bu kod yukardaki foraceh'i yapar...

            //dolayısıyla yukardaki foreach'E gerek yok. 

            _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            //vt daki datayı business a vermem lazım. o yuzden return kulllırız
            return _products;
        }
        
        public void Update(Product product)
        {
            //güncellenecek ürün, gönderdiğim ürün Id'ye sahip olan listedeki ürünü bul demek.
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);  //bu kod yukardaki foraceh'i yapar...
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            productToUpdate.CategoryId = product.CategoryId;
            //Entity framework yukardakileri aslında bizim yerimize yapacaktır. bunlar işin mantığı için yazıldı..
        }

        public List<Product> GetAllByCatgory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();  //where :içindeki şarta uyan bütün elemanları bir liste haline getirir ve orda durur. ve döndürür
            //yukarıya && diyerek istediğim kadar yeni koşul ekleyebilrim. 
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }
        public List<ProductDetailDto> GetProductDetail()
        {
            throw new NotImplementedException();
        }
    }
}
