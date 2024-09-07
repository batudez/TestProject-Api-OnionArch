using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Bases;
using TestApi.Application.Features.Auth.Rules;
using TestApi.Application.Interfaces.Automapper;
using TestApi.Application.UnitOfWorks;
using TestApi.Domain.Entities;

namespace TestApi.Application.Features.Auth.Command.RevokeAll
{
	public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommandRequest, Unit>
	{
		private readonly UserManager<User> _userManager;
		private readonly AuthRules _authRules;
		public RevokeAllCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, AuthRules authRules) : base(mapper, unitOfWork, httpContextAccessor)
		{
			_userManager = userManager;
			_authRules = authRules;
		}

		public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
		{
			List<User> users = await _userManager.Users.ToListAsync(cancellationToken);

            foreach (User user in users)
            {
				user.RefreshToken = null;
				await _userManager.UpdateAsync(user);
            }

			return Unit.Value;
        }
	}
}
