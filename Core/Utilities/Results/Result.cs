using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //ctor 'un base ler ile çalışma örneği    
        //2 param gönderen biri için succes'i diğer result metodundan al demek.
        public Result(bool success, string message):this(success) 
        {
            Message = message;
        }
        public Result(bool success)
        {
            
            Sussess = success;
        }
        public bool Sussess { get; } //getter readonly 'dir. cons.da set edilebilir.

        public string Message { get; }
    }
}
