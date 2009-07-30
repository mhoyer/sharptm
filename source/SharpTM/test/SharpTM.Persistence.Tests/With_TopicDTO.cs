using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public class With_TopicDTO : With_TopicMapDTO
	{
		protected static TopicDTO emptyTopicDTO;
		protected static TopicDTO identifiedTopicDTO;
		protected static ITopicMap topicMap;

		Given a_topicMap = () => topicMap = TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		Given an_empty_topic =
			() =>
				{
					emptyTopicDTO = new TopicDTO();
					emptyTopicDTO.Id = "http://sharptm.de/With_TopicDTO#an_empty_topic";

					topicMapDTO.Topics.Add(emptyTopicDTO);
				};

		Given an_identified_topic =
			() =>
				{
					identifiedTopicDTO = new TopicDTO();
					identifiedTopicDTO.Id = "http://sharptm.de/With_TopicDTO#an_identified_topic";
					identifiedTopicDTO.SubjectIdentifiers.Add(new LocatorDTO() { HRef = "http://sharptm.de/si" });
					identifiedTopicDTO.SubjectIdentifiers.Add(new LocatorDTO() { HRef = "http://marcelhoyer.de/si" });
					identifiedTopicDTO.SubjectLocators.Add(new LocatorDTO() { HRef = "http://sharptm.de/sl" });
					identifiedTopicDTO.SubjectLocators.Add(new LocatorDTO() { HRef = "http://marcelhoyer.de/sl" });
					identifiedTopicDTO.ItemIdentities.Add(new LocatorDTO() { HRef = "http://sharptm.de/ii" });
					identifiedTopicDTO.ItemIdentities.Add(new LocatorDTO() { HRef = "http://marcelhoyer.de/ii" });

					topicMapDTO.Topics.Add(identifiedTopicDTO);
				};

	}
}