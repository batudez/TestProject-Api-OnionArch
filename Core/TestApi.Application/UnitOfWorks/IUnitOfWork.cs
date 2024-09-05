using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Interfaces.Repositories;
using TestApi.Domain.Common;

namespace TestApi.Application.UnitOfWorks
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IReadRepository<T> GetReadRepository<T>() where T : class,IEntityBase,new();
		IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new();
		Task<int> SaveAsync();
		int Save();
	}
}
