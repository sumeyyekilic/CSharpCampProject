using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult :Result
    {
        //base 'e birşey göndermek demek result demek
        public SuccessResult(string message) : base(true, message)
        {

        }
        //msj vermek istemiyor olabilirdi
        public SuccessResult():base(true)  //parametresiz olmasına rağmen base'İn resulta true gönderiyor.
        {

        }
    }
}
