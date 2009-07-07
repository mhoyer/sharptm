using Pixelplastic.TopicMaps.SharpTM.Core;
using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public abstract class With_Occurrences : With_AssociatedTopics
	{
		static IOccurrence occurrenc1, occurrence2;

		Given two_occurrences =
			() =>
				{
					occurrenc1 = topic1.CreateOccurrence(
						topicMap.CreateTopic(),
						"This is a description.");

					occurrence2 = topic2.CreateOccurrence(
						topicMap.CreateTopic(),
						"23",
						Datatypes.Locators.Int);
				};
	}
}