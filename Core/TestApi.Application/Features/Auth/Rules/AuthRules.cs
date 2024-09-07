using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Bases;
using TestApi.Application.Features.Auth.Exceptions;
using TestApi.Domain.Entities;

namespace TestApi.Application.Features.Auth.Rules
{
	public class AuthRules : BaseRules
	{
		public Task UserShouldNotBeExist(User? user)
		{
			if (user is not null) throw new UserAlreadyExistException();
			return Task.CompletedTask;
		}
		public Task EmailOrPasswordShouldNotBeInvalid(User? user,bool checkPassword)
		{
			if (user is null || !checkPassword) throw new EmailOrPasswordShouldNotBeInvalidException();
			return Task.CompletedTask;
		}
		public Task RefreshTokenShouldNotBeExpired(DateTime expiryDate)
		{
			if (expiryDate <= DateTime.UtcNow) throw new RefreshTokenExpiredException();
			return Task.CompletedTask;
		}
		public Task EmailAddressShouldBeValid(User? user)
		{
			if (user is null) throw new EmailAddressShouldBeValidException();
			return Task.CompletedTask;
		}
	}
}
