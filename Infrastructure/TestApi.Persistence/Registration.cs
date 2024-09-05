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
		}
	}
}
