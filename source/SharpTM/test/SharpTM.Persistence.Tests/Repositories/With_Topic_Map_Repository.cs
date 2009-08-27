using Pixelplastic.TopicMaps.SharpTM.Persistence.Bridges;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Connectors;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.Repositories
{
	public class With_Topic_Map_Repository : With_TopicMapSystem
	{
		static TopicMapBridge bridge;
		static XTMConnector cnx;
		protected static TopicMapRepository repository;

		Given the_topic_map_bridge = () => bridge = new TopicMapBridge(topicMapSystem);
		Given a_connector = () => cnx = new XTMConnector();
		Given an_topic_map_repository = () => repository = new TopicMapRepository(cnx, bridge);
	}
}