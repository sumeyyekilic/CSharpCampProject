using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult //generic ile ne olduğunu veriyorum
    {                                       //T ye kısıtlama yazmayacaağız, herşey olabilir(exception da..)
                                            //interface'ler bu şekilde implemente edilir.
        //IResult implementasyonu

        T Data { get; }  // data ürün, ürünler olabilri..
    }
}
