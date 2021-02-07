using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager produtManager = new ProductManager(new InMemoryProductDal());


            foreach(var product in produtManager.GetAll()) 
            {
                Console.WriteLine(product.ProductName);
            }
            

        }
    }
}
