using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Interfaces.RedisCache;

namespace TestApi.Infrastructure.RedisCache
{
	public class RedisCacheService : IRedisService
	{
		private readonly ConnectionMultiplexer _redisConnection;
		private readonly IDatabase _database;
		private readonly RedisCacheSettings _redisCacheSettings;
        public RedisCacheService(IOptions<RedisCacheSettings> options)
        {
            _redisCacheSettings = options.Value;
			var opt = ConfigurationOptions.Parse(_redisCacheSettings.ConnectionString);
			_redisConnection = ConnectionMultiplexer.Connect(opt);
			_database = _redisConnection.GetDatabase();
        }
        public async Task<T> GetAsync<T>(string key)
		{
			var value = await _database.StringGetAsync(key);
			if (value.HasValue)
				return JsonConvert.DeserializeObject<T>(value);

			return default;
		}

		public async Task SetAsync<T>(string key, T value, DateTime? expirationTime = null)
		{
			TimeSpan timeUnitExpiration = expirationTime.Value - DateTime.UtcNow;
			await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), timeUnitExpiration);

		}
	}
}
