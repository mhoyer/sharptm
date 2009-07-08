using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM.Tests
{
	public class With_Topic_Map_Repository : BDDTest
	{
		protected static TopicMapRepository tmr;

		Given an_XTM_topic_map_repository = () => tmr = new TopicMapRepository();
	}
}