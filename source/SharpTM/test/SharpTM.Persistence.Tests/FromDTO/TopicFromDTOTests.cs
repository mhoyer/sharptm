// <copyright file="TopicFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.FromDTO
{
	public class When_mapping_a_topic_with_many_identifiers : With_TopicDTO
	{
		static ITopic topic;

		Because of_mapping_the_topic_DTO_to_a_TMAPI_topic = () => topic = TopicFromDTO.Create(topicMap, identifiedTopicDTO);

		It should_contain_all_item_identifiers =
			() =>
				{
					foreach (LocatorDTO dto in identifiedTopicDTO.ItemIdentities)
					{
						topic.ItemIdentifiers
							.ShouldContain(LocatorFromDTO.Create(topicMap, dto));
					}
				};

		It should_contain_all_subject_identifiers =
			() =>
				{
					foreach (LocatorDTO dto in identifiedTopicDTO.SubjectIdentifiers)
					{
						topic.SubjectIdentifiers
							.ShouldContain(LocatorFromDTO.Create(topicMap, dto));
					}
				};

		It should_contain_all_subject_locators =
			() =>
				{
					foreach (LocatorDTO dto in identifiedTopicDTO.SubjectLocators)
					{
						topic.SubjectLocators
							.ShouldContain(LocatorFromDTO.Create(topicMap, dto));
					}
				};
	}

	public class When_mapping_a_topic_DTO_that_already_exists_in_TMAPI_topic_map_by_subject_identifier : With_TopicDTO
	{
		static string sid;
		static TopicDTO topicDTO;

		Given a_subject_identifier = () => sid = "http://sharptm.de/TopicFromDTOTests";
		Given an_existing_TMAPI_topic = () => topicMap.CreateTopicBySubjectIdentifier(topicMap.CreateLocator(sid));
		Given a_topic_DTO = () =>
		                    	{
		                    		topicDTO = new TopicDTO();
		                    		topicDTO.ItemIdentities.Add(new LocatorDTO() { HRef = sid });
		                    	};

		Because of_mapping_the_topic_map_DTO = () => TopicFromDTO.Create(topicMap, topicDTO);

		It should_map_to_one_and_only_one_topic = () => topicMap.Topics.Count.ShouldEqual(1);
		It should_restore_the_topic_by_subject_identifier = () => topicMap.GetTopicBySubjectIdentifier(topicMap.CreateLocator(sid)).ShouldNotBeNull();
		It should_add_the_item_identifier = () => topicMap.GetConstructByItemIdentifier(topicMap.CreateLocator(sid)).ShouldNotBeNull();
	}

	public class When_mapping_a_topic_DTO_that_already_exists_in_TMAPI_topic_map_by_subject_locator : With_TopicDTO
	{
		static string sloc;
		static TopicDTO topicDTO;

		Given a_subject_locator = () => sloc = "http://sharptm.de/TopicFromDTOTests";
		Given an_existing_TMAPI_topic = () => topicMap.CreateTopicBySubjectLocator(topicMap.CreateLocator(sloc));
		Given a_topic_DTO = () =>
		                    	{
		                    		topicDTO = new TopicDTO();
		                    		topicDTO.ItemIdentities.Add(new LocatorDTO() { HRef = sloc });
		                    	};

		Because of_mapping_the_topic_map_DTO = () => TopicFromDTO.Create(topicMap, topicDTO);

		// HACK is it correct that two topics can coexist, where one is defined by subject/item identifier 
		//      and another uses the same IRI as subject locator? 
		It should_map_to_one_and_only_one_topic = () => topicMap.Topics.Count.ShouldEqual(2); 
		It should_restore_the_topic_by_subject_locator = () => topicMap.GetTopicBySubjectLocator(topicMap.CreateLocator(sloc)).ShouldNotBeNull();
		It should_add_the_item_identifier = () => topicMap.GetConstructByItemIdentifier(topicMap.CreateLocator(sloc)).ShouldNotBeNull();
	}

	public class When_mapping_a_topic_DTO_that_already_exists_in_TMAPI_topic_map_by_item_identifier : With_TopicDTO
	{
		static string iid;
		static TopicDTO topicDTO;

		Given a_item_identifier = () => iid = "http://sharptm.de/TopicFromDTOTests";
		Given an_existing_TMAPI_topic = () => topicMap.CreateTopicByItemIdentifier(topicMap.CreateLocator(iid));
		Given a_topic_DTO = () =>
		                    	{
		                    		topicDTO = new TopicDTO();
		                    		topicDTO.ItemIdentities.Add(new LocatorDTO() { HRef = iid });
		                    	};

		Because of_mapping_the_topic_map_DTO = () => TopicFromDTO.Create(topicMap, topicDTO);

		It should_map_to_one_and_only_one_topic = () => topicMap.Topics.Count.ShouldEqual(1);
		It should_restore_the_topic_by_item_identifier = () => topicMap.GetConstructByItemIdentifier(topicMap.CreateLocator(iid)).ShouldNotBeNull();
		
	}

	/// <summary>
	/// <seealso cref="http://www.isotopicmaps.org/sam/sam-xtm/#d0e535"/>
	/// </summary>
	public class When_mapping_topics_with_id : With_TopicMapDTO
	{
		static ITopicMap topicMap;
		static TopicDTO topicDTO;
		static IConstruct construct;

		Given a_construct = () => topicDTO = new TopicDTO() { Id = "http://sharptm.de/ConstructFromDTOTests#" + typeof(When_mapping_topics_with_id).Name };
		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/ConstructFromDTOTests");

		Because of_mapping_the_construct = () => construct = TopicFromDTO.Create(topicMap, topicDTO);

		It should_generate_default_item_identifiers_from_id =
			() => construct.ItemIdentifiers.ShouldContain(topicMap.CreateLocator(topicDTO.Id));
	}

	public class When_mapping_an_empty_topic : With_TopicMapDTO
	{
		static ITopicMap topicMap;
		static ITopic topic;
		static TopicDTO topicDTO;
		
		Given a_topic = () => topicDTO = new TopicDTO();
		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/ConstructFromDTOTests");

		Because of_mapping_the_construct = () => topic = TopicFromDTO.Create(topicMap, topicDTO);

		It should_generate_one_item_identifiers = () => topic.ItemIdentifiers.Count.ShouldEqual(1);
	}

	public class When_mapping_a_topic_with_occurrences : With_filled_TopicMapDTO_and_occurrences
	{
		static ITopicMap topicMap;
		static ITopic topic;

		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/ConstructFromDTOTests");
		
		Because of_mapping_the_topic = () => topic = TopicFromDTO.Create(topicMap, marcelHoyer);
		
		It should_map_at_least_one_occurrence = () => topic.Occurrences.Count.ShouldNotEqual(0);
		It should_map_the_occurrences = () => topic.Occurrences.Count.ShouldEqual(marcelHoyer.Occurrences.Count);
	}

	public class When_mapping_a_topic_with_names : With_filled_TopicMapDTO_and_names
	{
		static ITopicMap topicMap;
		static ITopic topic;

		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/ConstructFromDTOTests");

		Because of_mapping_the_topic = () => topic = TopicFromDTO.Create(topicMap, marcelHoyer);

		It should_map_at_least_one_name = () => topic.Names.Count.ShouldNotEqual(0);
		It should_map_the_names = () => topic.Names.Count.ShouldEqual(marcelHoyer.Names.Count);
	}

	public class When_mapping_a_topic_with_instance_of : With_filled_TopicMapDTO_using_instance_of
	{
		static ITopicMap topicMap;
		static ITopic topic;
		static ITopic topicType;

		Given a_TMAPI_topic_map = () => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/ConstructFromDTOTests");
		
		Because of_mapping_the_topic_with_set_instanceOf_property = () => topic = TopicFromDTO.Create(topicMap, marcelHoyer);

		It should_add_the_topic_to_the_instance_role =
			() =>
			topic.RolesPlayed[0].Type.SubjectIdentifiers[0].Reference.ShouldEqual(
				"http://psi.topicmaps.org/iso13250/model/instance");

		It should_create_a_type_instance_association =
			() =>
			topic.RolesPlayed[0].Parent.Type.SubjectIdentifiers[0].Reference.ShouldEqual(
				"http://psi.topicmaps.org/iso13250/model/type-instance");

		It should_create_the_topic_type =
			() =>
				{
					topicType =
						topicMap.GetTopicBySubjectIdentifier(topicMap.CreateLocator("http://psi.topicmaps.org/iso13250/model/type"));
					topicType.ShouldNotBeNull();
				};

		It should_create_the_type_role =
			() => topic.RolesPlayed[0].Parent.GetRolesByTopicType(topicType)[0].Player
			.ShouldEqual(topicMap.CreateTopicBySubjectIdentifier(topicMap.CreateLocator(person.SubjectIdentifiers[0].HRef)));

	}
}