using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public abstract class With_FilledTopicMapDTOandNames : With_FilledTopicMapDTO
	{
		protected static NameDTO marcelHoyerName;

		Given The_name_for_Marcel_Hoyer =
			() =>
				{
					marcelHoyerName = TestHelper.CreateName("Marcel Hoyer");
					marcelHoyer.Names.Add(marcelHoyerName);
				};

	}
}