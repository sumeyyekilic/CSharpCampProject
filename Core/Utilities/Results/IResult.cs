using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //temel voidler başlangıç  //ekstrea birşye  döndürmeyecek
    public interface IResult
    {
        //Temel voidler için başlangıç
        //bir işlem sonucu ve birde kullanıcıyı bilg adına msj olsun:
        bool Sussess { get; } //get'ler okumak için
        string Message { get; }
        bool Success { get; set; }
    }
}
