using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM.Tests
{
	public class With_Topic_Map_System_Repository : BDDTest
	{
		static TopicMapSystemRepository tmsr;

		Given an_XTM_repository = () => tmsr = new TopicMapSystemRepository();
	}
}