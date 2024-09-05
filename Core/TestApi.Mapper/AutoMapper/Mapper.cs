using AutoMapper;
using AutoMapper.Internal;


namespace TestApi.Mapper.AutoMapper
{
	public class Mapper : Application.Interfaces.Automapper.IMapper
	{
		public static List<TypePair> typePairs = new();

		private IMapper mapperContainer;
		public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
		{
			Config<TDestination, TSource>(5, ignore);

			return mapperContainer.Map<TSource, TDestination>(source);
		}

		public IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources, string? ignore = null)
		{
			Config<TDestination, TSource>(5, ignore);

			return mapperContainer.Map<IList<TSource>, IList<TDestination>>(sources);
		}

		public TDestination Map<TDestination>(object source, string? ignore = null)
		{
			Config<TDestination , object>(5, ignore);

			return mapperContainer.Map<TDestination>(source);
		}

		public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
		{
			Config<TDestination, IList<object>>(5, ignore);

			return mapperContainer.Map<IList<TDestination>>(source);
		}

		protected void Config<TDestination, TSource>(int depth = 5 , string? ignore = null)
		{
			var typePair = new TypePair(typeof(TSource) , typeof(TDestination));
			if (typePairs.Any(a => a.DestinationType == typePair.DestinationType &&
			a.SourceType == typePair.SourceType) && ignore is null) return;

			typePairs.Add(typePair);
			var config = new MapperConfiguration(cfg =>
			{
				foreach (var item in typePairs)
				{
					if(ignore is not null)
						cfg.CreateMap(item.SourceType , item.DestinationType).MaxDepth(depth)
						.ForMember(ignore, x => x.Ignore()).ReverseMap();
					else
						cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ReverseMap();
				}
			});

			mapperContainer = config.CreateMapper();
		}
	}
}
