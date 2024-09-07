using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Interfaces.RedisCache;

namespace TestApi.Application.Behaviour
{
	public class RedisCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IRedisService _redisService;

		public RedisCacheBehaviour(IRedisService redisService)
		{
			_redisService = redisService;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if(request is ICacheableQuery query)
			{
				var cacheKey = query.CacheKey;
				var cacheTime = query.CacheTime;

				var cachedData = await _redisService.GetAsync<TResponse>(cacheKey);
				if(cachedData is not null)
					return cachedData;

				var response = await next();
				if(response is not null) await _redisService.SetAsync(cacheKey, response,DateTime.UtcNow.AddMinutes(cacheTime));
				
				return response;
			}

			return await next();
		}

	}
}
