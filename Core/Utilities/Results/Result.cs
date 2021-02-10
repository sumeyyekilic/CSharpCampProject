using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Sussess { get; }

        public string Message { get; }
    }
}
