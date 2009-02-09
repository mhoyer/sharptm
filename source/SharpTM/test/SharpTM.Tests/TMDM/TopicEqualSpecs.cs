// <copyright file="TopicEqualitySpecs.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Tests.TMDM
{
	public class When_two_topics_are_the_same_instance_they_should_be_equal :
		TopicMapContextSpecification_with_two_topics
	{
		static bool areEqual;
		Because of_comparing_a_topic_with_itself = () => areEqual = ((Topic)topic1).Equals(topic1);
		It should_be_equal = () => areEqual.ShouldBeTrue();
	}

	public class When_two_topics_have_the_same_item_identifier_they_should_be_equal :
		TopicMapContextSpecification_with_two_topics
	{
		static bool areEqual;
		Given a_topic = () => topic1.AddItemIdentifier(topicMap.CreateLocator("http://sharptm.de"));

		Given another_topic_with_the_same_item_identifier_value =
			() => topic2.AddItemIdentifier(topicMap.CreateLocator("http://sharptm.de"));

		Because of_comparing_the_two_topics = () => areEqual = ((Topic)topic1).Equals(topic2);
		It should_be_equal = () => areEqual.ShouldBeTrue();
	}

	// Negative tests

	public class When_two_topics_are_not_the_same_instance_they_should_NOT_be_equal :
		TopicMapContextSpecification_with_two_topics
	{
		static bool areEqual;
		Because of_comparing_a_topic_with_itself = () => areEqual = ((Topic)topic1).Equals(topic2);
		It should_be_equal = () => areEqual.ShouldBeFalse();
	}

	public class When_two_topics_are_not_the_same_instance_they_should_NOT_be_equal_even_if_names_are_equal :
		TopicMapContextSpecification_with_two_topics
	{
		static bool areEqual;

		Given the_same_name =
			() =>
				{
					topic1.CreateName("SHARPTM");
					topic2.CreateName("SHARPTM");
				};
		Because of_comparing_a_topic_with_itself = () => areEqual = ((Topic)topic1).Equals(topic2);
		It should_be_equal = () => areEqual.ShouldBeFalse();
	}
}