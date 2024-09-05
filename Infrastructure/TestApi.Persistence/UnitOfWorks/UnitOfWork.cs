using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Interfaces.Repositories;
using TestApi.Application.UnitOfWorks;
using TestApi.Persistence.Context;
using TestApi.Persistence.Repositories;

namespace TestApi.Persistence.UnitOfWorks
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async ValueTask DisposeAsync() => await _appDbContext.DisposeAsync();


		public int Save() => _appDbContext.SaveChanges();
		public async Task<int> SaveAsync() => await _appDbContext.SaveChangesAsync();
		IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_appDbContext);
		IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_appDbContext);
		
	}
}
