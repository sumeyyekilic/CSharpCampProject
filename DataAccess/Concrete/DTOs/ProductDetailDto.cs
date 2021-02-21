using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.DTOs
{
    public class ProductDetailDto : IDto
    {
        //birden fazla tablonun joini olabilir. Dto
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStoxk { get; set; }

    }
}
