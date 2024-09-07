using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Bases;
using TestApi.Application.Features.Auth.Exceptions;
using TestApi.Application.Features.Auth.Rules;
using TestApi.Application.Interfaces.Automapper;
using TestApi.Application.Interfaces.Tokens;
using TestApi.Application.UnitOfWorks;
using TestApi.Domain.Entities;

namespace TestApi.Application.Features.Auth.Command.RefreshToken
{
	public class RefreshTokenCommandHandler : BaseHandler, 
		IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
	{
		private readonly UserManager<User> _userManager;
		private readonly ITokenService _tokenService;
		private readonly AuthRules _authRules;
		public RefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, 
			IHttpContextAccessor httpContextAccessor,UserManager<User> userManager,
			AuthRules authRules,
			ITokenService tokenService) : base(mapper, unitOfWork, httpContextAccessor)
		{
			_userManager = userManager;
			_tokenService = tokenService;
			_authRules = authRules;
		}

		public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
		{
			var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
			string email = principal.FindFirstValue(ClaimTypes.Email);

			User? user = await _userManager.FindByEmailAsync(email);
			IList<string> roles = await _userManager.GetRolesAsync(user);

			await _authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpireTime);

			JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user, roles);
			string newRefreshToken = _tokenService.GenerateRefreshToken();

			user.RefreshToken = newRefreshToken;
			await _userManager.UpdateAsync(user);

			return new()
			{
				AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
				RefreshToken = newRefreshToken,
			};
			
		}
	}
}
