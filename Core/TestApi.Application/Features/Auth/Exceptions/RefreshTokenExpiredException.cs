using TestApi.Application.Bases;

namespace TestApi.Application.Features.Auth.Exceptions
{
	public class RefreshTokenExpiredException : BaseExceptions
	{
		public RefreshTokenExpiredException() : base("Oturum süresi sona ermiştir tekrar giris yapın") { }
	}
}
