using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        //işlem sonucunu default true  vereceğim.

        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }

        //2.yönt message olayına girmek istemiyorsa:
        public ErrorDataResult(T data) : base(data, false)
        {

        }

        public ErrorDataResult(string message) : base(default, false, message)
        {

        }
        public ErrorDataResult() : base(default, false)
        {

        }

    }
}
