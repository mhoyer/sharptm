using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public abstract class With_FilledTopicMapDTOandNames : With_FilledTopicMapDTO
	{
		Given Names_for_Marcel_Hoyer =
			() => marcelHoyer.Names.Add(TestHelper.CreateName("Marcel Hoyer", "Hoyer, Marcel", "Marcel"));
	}
}