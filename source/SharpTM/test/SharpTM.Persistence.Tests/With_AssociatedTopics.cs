// <copyright file="With_AssociatedTopics.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Core;
using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public abstract class With_AssociatedTopics : With_TopicMap
	{
		protected static ITopic topic1;
		protected static ITopic topic2;
		static ITopic roleType, associationType;
		static IAssociation association;

		Given two_associated_topics =
			() =>
				{
					topic1 = topicMap.CreateTopic();
					topic1.AddSubjectIdentifier(topicMap.CreateLocator(TOPIC_MAP_SID + "/topic1"));

					topic2 = topicMap.CreateTopic();
					topic2.AddSubjectIdentifier(topicMap.CreateLocator(TOPIC_MAP_SID + "/topic2"));

					associationType = topicMap.CreateTopic();
					roleType = topicMap.CreateTopic();
					
					association = topicMap.CreateAssociation(associationType);

					association.CreateRole(roleType, topic1);
					association.CreateRole(roleType, topic2);
				};
	}
}