using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
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
    }
}
