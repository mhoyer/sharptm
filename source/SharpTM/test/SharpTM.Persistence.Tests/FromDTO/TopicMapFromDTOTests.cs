// <copyright file="TopicMapFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Reflection;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Connectors;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.FromDTO
{
	public class When_mapping_from_topic_map_DTO_without_identifiers : With_TopicMapDTO
	{
		static Exception exception; 

		Given a_topic_map_DTO_without_ids = () => topicMapDTO.ItemIdentities.Clear();
		Because of_mapping = () => exception = Catch.Exception(() => TopicMapFromDTO.Create(topicMapSystem, topicMapDTO));
		It should_throw_an_exception = () => exception.ShouldBeType<MappingException>();
	}

	public class When_mapping_from_topic_map_DTO : With_TopicMapDTO
	{
		Because of_mapping_the_topic_map = 
			() => TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		It should_create_and_add_a_new_TMAPI_topic_map_to_the_system = 
			() => topicMapSystem.GetTopicMap(TOPIC_MAP_SID).ShouldNotBeNull();
	}

	public class When_mapping_from_topic_map_DTO_with_topics : With_filled_TopicMapDTO
	{
		static ITopicMap topicMap;

		Because of_mapping_the_topic_map =
			() => topicMap = TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		It should_create_and_add_new_TMAPI_topics =
			() => topicMap.Topics.Count.ShouldEqual(topicMapDTO.Topics.Count);

		It should_map_the_SharpTM_topic =
			() => topicMap.GetTopicBySubjectIdentifier(topicMap.CreateLocator(sharpTM.SubjectIdentifiers[0].HRef));
	}

	public class When_mapping_from_topic_map_DTO_with_associations : With_filled_TopicMapDTO
	{
		static ITopicMap topicMap;

		Because of_mapping_the_topic_map =
			() => topicMap = TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		It should_create_and_add_new_TMAPI_associations =
			() => topicMap.Associations.Count.ShouldEqual(topicMapDTO.Associations.Count);
	}

	public class When_mapping_from_topic_map_DTO_with_merge_maps : With_filled_TopicMapDTO
	{
		Because of_mapping_the_topic_map =
			() => TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		// TODO: It should_handle_the_merge_maps = () => 
		[Fact(Skip = "Not implemented.")]
		public new void Run()
		{
		}
	}

	public class When_mapping_from_topic_map_DTO_with_reifier : With_filled_TopicMapDTO
	{
		static string reifierSID;
		static TopicDTO reifierDTO;
		static ITopicMap topicMap;

		Given a_reifier =
			() =>
				{
					reifierSID = TestHelper.CreateLocatorString(typeof(When_mapping_from_topic_map_DTO_with_reifier).FullName);
					reifierDTO = topicMapDTO.CreateTopic(reifierSID);
					topicMapDTO.Reifier = reifierSID;
				};

		Because of_mapping_the_topic_map =
			() => topicMap = TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		It should_map_the_reifier = () => topicMap.Reifier.ShouldNotBeNull();
		It should_map_the_reifiers_subject_identifier = () => topicMap.Reifier.SubjectIdentifiers[0].Reference.ShouldEqual(reifierSID);
	}

	public class When_mapping_from_music_xtm : With_TopicMapSystem
	{
		static string xtm;
		static ITopicMap topicMap;
		static TopicMapDTO topicMapDTO;
		static XTMConnector xtmConnector;

		Given an_XMT_file = () => xtm = typeof(With_TopicMap).Namespace + ".music.xtm";
		Given an_xtm_connector = () => xtmConnector = new XTMConnector();

		Given the_loaded_topic_map_DTO =
			() =>
				{
					topicMapDTO =
						xtmConnector.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(xtm));
					topicMapDTO.ItemIdentities.Add(TestHelper.CreateLocator("music.xtm"));
				};

		Because of_mapping_the_music_topic_map_DTO = () => topicMap = TopicMapFromDTO.Create(topicMapSystem, topicMapDTO);

		It should_create_the_TMAPI_topic_map = () => topicMap.ShouldNotBeNull();
	}
}