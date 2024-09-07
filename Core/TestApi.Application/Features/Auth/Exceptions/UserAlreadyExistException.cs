using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Bases;

namespace TestApi.Application.Features.Auth.Exceptions
{
	public class UserAlreadyExistException : BaseExceptions
	{
		public UserAlreadyExistException() : base("Böyle bir kullanıcı zaten var") { }
	}
}
