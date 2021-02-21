using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager produtManager = new ProductManager(new EfProductDal());


            //foreach(var product in produtManager.GetAll().Data) 
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //2numaralı kategoride ki ürünler gelsin :
            //foreach (var product in produtManager.GetByUnitPrice(20,100))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

        }
    }
}
