// <copyright file="MergeTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Tests
{
	public class When_merging_two_scopes : TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static IName name1, name2;

		Given a_name_with_a_scope = () => name1 = topic1.CreateName("NAME", scope1);
		Given another_name_with_a_different_scope = () => name2 = topic2.CreateName("NAME23", scope2);

		Because of_merging_the_scopes = () => scope1.MergeIn(scope2);
		It should_add_the_scope_of_the_first_to_the_second_name = () => name2.Scope.ShouldContain(scope1);

		It should_not_change_the_scope_of_first_name = () => name1.Scope.ShouldContain(scope1);
		It should_remove_the_original_scope_of_the_second_name = () => name2.Scope.ShouldNotContain(scope2);
	}

	public class When_merging_two_typed_names_it_should_throw_an_exception :
		TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static Exception exception;
		static IName name1, name2;

		Given a_typed_name = () => name1 = topic1.CreateName(type1, "NAME");
		Given another_name_with_a_different_type = () => name2 = topic2.CreateName(type2, "NAME");

		Because of_merging_the_names = () => exception = Catch.Exception(() => Merging.Merge.Name(name1).Into(name2));

		It throws_an_exception = () => exception.ShouldBeType<TMAPIException>();
	}

	public class When_merging_two_names_with_same_variants_it_should_merge_them_too :
		TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static IName name1, name2;
		static IVariant variant1, variant2;

		Given a_scoped_name_with_a_variant =
			() =>
				{
					name1 = topic1.CreateName("München", scope1);
					variant1 = name1.CreateVariant("Munich", scope2);
				};

		Given another_scoped_name_with_same_values_and_variant =
			() =>
				{
					name2 = topic2.CreateName("München", scope1);
					variant2 = name2.CreateVariant("Munich", scope2);
				};

		Because of_merging_the_topics = () => topic1.MergeIn(topic2);

		It should_merge_the_names =
			() =>
				{
					topic1.Names.Count.ShouldEqual(1);
					topic1.Names[0].ShouldEqual(name1);
				};

		It should_merge_the_variants =
			() =>
				{
					topic1.Names[0].Variants.Count.ShouldEqual(1);
					topic1.Names[0].Variants[0].ShouldEqual(variant1);
				};
	}

	public class When_adding_a_SID_to_a_topic_while_another_topic_with_same_SID_still_exists : With_TopicMap
	{
		static ILocator sid;
		static ITopic existingTopic;
		static ITopic topicToBeMerged;

		Given a_subject_identifier = () => sid = topicMapSystem.CreateLocator("http://sharptm.de");
		Given an_existing_topic = () => existingTopic = topicMap.CreateTopicBySubjectIdentifier(sid);
		Given another_topic = () => topicToBeMerged = topicMap.CreateTopicBySubjectIdentifier(sid.Resolve("foo"));

		Because of_adding_the_same_SID_to_last_topic = () => topicToBeMerged.AddSubjectIdentifier(sid);

		It should_save_the_parent_of_the_existing_topic = () => existingTopic.Parent.ShouldNotBeNull();
		It should_save_the_parent_of_the_other_topic = () => topicToBeMerged.Parent.ShouldNotBeNull();
		It should_reference_same_occurrence_list_for_both = () => existingTopic.Occurrences.ShouldEqual(topicToBeMerged.Occurrences);
		It should_reference_same_roles_played_list_for_both = () => existingTopic.RolesPlayed.ShouldEqual(topicToBeMerged.RolesPlayed);
	}
}