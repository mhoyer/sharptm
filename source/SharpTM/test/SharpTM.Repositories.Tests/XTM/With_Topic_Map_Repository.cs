using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Bridges;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories;
using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM.Tests
{
	public class With_Topic_Map_Repository : With_XTMConnector
	{
		static TopicMapBridge bridge;
		protected static ITopicMapSystem topicMapSystem;
		protected static TopicMapRepository repository;

		Given a_topic_map_system =
			() => topicMapSystem = TopicMapSystemFactory
			                       	.NewInstance<SharpTMSystemFactory>()
			                       	.NewTopicMapSystem();
		Given the_topic_map_bridge = () => bridge = new TopicMapBridge(topicMapSystem); 
		Given an_XTM_topic_map_repository = () => repository = new TopicMapRepository(cnx, bridge);
	}
}