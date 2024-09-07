using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Bases;
using TestApi.Application.Features.Auth.Rules;
using TestApi.Application.Interfaces.Automapper;
using TestApi.Application.UnitOfWorks;
using TestApi.Domain.Entities;

namespace TestApi.Application.Features.Auth.Command.Revoke
{
	public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest,Unit>
	{
		private readonly UserManager<User> _userManager;
		private readonly AuthRules _authRules;
		public RevokeCommandHandler(UserManager<User> userManager,
			IMapper mapper, IUnitOfWork unitOfWork, 
			IHttpContextAccessor httpContextAccessor,
			AuthRules authRules) : base(mapper, unitOfWork, httpContextAccessor)
		{
			_userManager = userManager;
			_authRules = authRules;
		}

		public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
		{
			User user = await _userManager.FindByEmailAsync(request.Email);
			await _authRules.EmailAddressShouldBeValid(user);

			user.RefreshToken = null;
			await _userManager.UpdateAsync(user);

			return Unit.Value;
		}
	}
}
