using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public abstract class With_AssociatedAndNamedTopics : With_AssociatedTopics
	{
		protected static IName name1, name2, name3;
		protected static IVariant variant1, variant2;
		protected static ITopic reifier;
		

		Given three_names_and_two_variants =
			() =>
				{
					name1 = topic1.CreateName("Marcel Hoyer");
					name2 = topic1.CreateName("Pixelplastic");
					name3 = topic2.CreateName("Alexander Groﬂ");

					variant1 = name1.CreateVariant("Hoyer, Marcel", topicMap.CreateTopic());
					variant2 = name1.CreateVariant("M. Hoyer", topicMap.CreateTopic());

					reifier = topicMap.CreateTopicBySubjectIdentifier(topicMap.CreateLocator(TOPIC_MAP_SID + "/reifier"));
					variant1.Reifier = reifier;
					name2.Reifier = topicMap.CreateTopic();
				};
	}
}