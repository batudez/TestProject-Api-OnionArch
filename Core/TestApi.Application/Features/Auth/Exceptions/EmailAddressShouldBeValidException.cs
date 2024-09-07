using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Bases;

namespace TestApi.Application.Features.Auth.Exceptions
{
	public class EmailAddressShouldBeValidException : BaseExceptions
	{
		public EmailAddressShouldBeValidException() : base("Böyle bir mail adresi bulunmamaktadır.") { }
	}
}
