// <copyright file="RoleFromDTOTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.FromDTO
{
	public class When_mapping_a_role : With_filled_TopicMapDTO
	{
		static IAssociation association;
		static IRole role;
		static ITopicMap topicMap;

		Given a_topic_map = () => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/RoleFromDTOTests");
		Given the_converted_person_topic = () => TopicFromDTO.Create(topicMap, person);
		Given the_converted_role_player_topic = () => TopicFromDTO.Create(topicMap, marcelHoyer);
		Given an_association = () => association = AssociationFromDTO.Create(topicMap, projectLeaderOfSharpTM);

		Because of_mapping_the_role = () => role = RoleFromDTO.Create(association, marcelKnowsAboutLutz.Roles[0]);

		It should_map_item_identifiers = () => role.ItemIdentifiers.Count.ShouldEqual(marcelKnowsAboutLutz.Roles[0].ItemIdentities.Count);
		It should_map_the_role_type =
			() =>
				{
					var roleTypeSID = topicMap.CreateLocator(marcelKnowsAboutLutz.Roles[0].Type.TopicReference.HRef);
					role.Type.ShouldEqual(topicMap.GetTopicBySubjectIdentifier(roleTypeSID));
				};

		It should_map_the_player =
			() =>
				{
					var playerSID = topicMap.CreateLocator(marcelKnowsAboutLutz.Roles[0].TopicReference.HRef);
					role.Player.ShouldEqual(topicMap.GetTopicBySubjectIdentifier(playerSID));
				};
	}

	public class When_mapping_a_reified_role : With_filled_TopicMapDTO
	{
		static IAssociation association;
		static IRole role;
		static ITopicMap topicMap;
		static ITopic reifier;
		static TopicDTO reifierDTO;

		Given a_reifier = () =>
		                  	{
		                  		var reifierSID = TestHelper.CreateLocatorString(typeof(When_mapping_a_reified_role).FullName);
		                  		reifierDTO = topicMapDTO.CreateTopic(reifierSID);
		                  		marcelKnowsAboutLutz.Roles[0].Reifier = reifierSID;
		                  	};
		Given a_topic_map = () => topicMap = topicMapSystem.CreateTopicMap("http://sharptm.de/RoleFromDTOTests");
		Given the_converted_reifier = () => reifier = TopicFromDTO.Create(topicMap, reifierDTO);
		Given an_association = () => association = AssociationFromDTO.Create(topicMap, projectLeaderOfSharpTM);

		Because of_mapping_the_role = () => role = RoleFromDTO.Create(association, marcelKnowsAboutLutz.Roles[0]);

		It should_map_item_identifiers = () => role.ItemIdentifiers.Count.ShouldEqual(marcelKnowsAboutLutz.Roles[0].ItemIdentities.Count);
		It should_map_the_reifier = () => role.Reifier.ShouldEqual(reifier);
	}
}