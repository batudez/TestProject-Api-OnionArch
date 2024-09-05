using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Bases;

namespace TestApi.Application.Features.Products.Exceptions
{
	public class ProductTitleMustNotBeSameException : BaseExceptions
	{
        public ProductTitleMustNotBeSameException() : base("ürün basligi zaten var") { }
        
    }
}
