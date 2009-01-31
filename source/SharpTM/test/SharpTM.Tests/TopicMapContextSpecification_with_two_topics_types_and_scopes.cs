// <copyright file="TopicMapContextSpecification_with_two_topics_types_and_scopes.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Tests
{
	public abstract class TopicMapContextSpecification_with_two_topics_types_and_scopes
		: TopicMapContextSpecification
	{
		protected static ITopic scope1, scope2;
		protected static ITopic topic1, topic2;
		protected static ITopic type1, type2;

		Given a_first_unspecified_scope = () => scope1 = topicMap.CreateTopic();
		Given a_first_unspecified_topic = () => topic1 = topicMap.CreateTopic();
		Given a_first_unspecified_type = () => type1 = topicMap.CreateTopic();

		Given a_second_unspecified_scope = () => scope2 = topicMap.CreateTopic();
		Given a_second_unspecified_topic = () => topic2 = topicMap.CreateTopic();
		Given a_second_unspecified_type = () => type2 = topicMap.CreateTopic();
	}
}