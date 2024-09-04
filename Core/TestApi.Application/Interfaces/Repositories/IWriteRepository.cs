﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Domain.Common;

namespace TestApi.Application.Interfaces.Repositories
{
	public interface IWriteRepository<T> where T : class, IEntityBase,new()
	{
		Task AddAsync(T Entity);
		Task AddRangeAsync(IList<T> entities);
		Task<T> UpdateAsync(T entity);
		Task HardDeleteAsync(T entity);
	}
}
