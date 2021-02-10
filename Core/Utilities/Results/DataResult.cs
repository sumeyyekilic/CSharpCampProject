using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string message): base(success, message)  //diğer result dan farkı datat'sı olması
        {
            Data = data;
        }
        //mesj. göndermek istemeyebilirim.
        public DataResult(T data, bool success): base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
