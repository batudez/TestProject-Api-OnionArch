using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Interfaces.Repositories;
using TestApi.Application.UnitOfWorks;
using TestApi.Domain.Entities;
using TestApi.Persistence.Context;
using TestApi.Persistence.Repositories;
using TestApi.Persistence.UnitOfWorks;

namespace TestApi.Persistence
{
	public static class Registration
	{
		public static void AddPersistence(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(opt => 
			opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddIdentityCore<User>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequiredLength = 2;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireDigit = false;
				opt.SignIn.RequireConfirmedEmail = false;

			})
				.AddRoles<Role>()
				.AddEntityFrameworkStores<AppDbContext>();
		}
	}
}
