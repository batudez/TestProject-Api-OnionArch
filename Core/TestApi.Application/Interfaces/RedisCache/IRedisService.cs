using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Application.Interfaces.RedisCache
{
	public interface IRedisService
	{
		Task<T> GetAsync<T>(string key);
		Task SetAsync<T>(string key, T value, DateTime? expirationTime = null);
	}
}
