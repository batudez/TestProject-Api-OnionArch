using TestApi.Application.Bases;

namespace TestApi.Application.Features.Auth.Exceptions
{
	public class EmailOrPasswordShouldNotBeInvalidException : BaseExceptions
	{
		public EmailOrPasswordShouldNotBeInvalidException() : base("Email or password incorrect") { }
	}
}
