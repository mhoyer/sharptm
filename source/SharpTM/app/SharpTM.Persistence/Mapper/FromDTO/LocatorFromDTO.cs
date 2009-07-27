using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class LocatorFromDTO : ClassMapper<LocatorDTO, ILocator>
	{
		private LocatorFromDTO()
		{
		}

		public static ILocator Create(ITopicMap topicMap, LocatorDTO locator)
		{
			return topicMap.CreateLocator(locator.HRef);
		}

		public static ILocator Create(ITopicMapSystem topicMapSystem, LocatorDTO locator)
		{
			return topicMapSystem.CreateLocator(locator.HRef);
		}
	}
}