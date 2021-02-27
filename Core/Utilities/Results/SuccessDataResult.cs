using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        //işlem sonucunu default true  vereceğim.

        public SuccessDataResult(T data, string message): base (data,true, message)
        {

        }

        //2.yönt message olayına girmek istemiyorsa:
        public SuccessDataResult(T data) : base(data, true)
        {

        }

        public SuccessDataResult(string message) : base(default, true, message)
        {

        }
        public SuccessDataResult() : base(default, true) //message yok sadece true verdim.
        {

        }

    }
}
