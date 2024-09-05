using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Interfaces.Automapper;

namespace TestApi.Mapper
{
	public static class Registration
	{
		public static void AddCustomMapper(this IServiceCollection services)
		{
			services.AddSingleton<IMapper, AutoMapper.Mapper>();
		}
	}
}
