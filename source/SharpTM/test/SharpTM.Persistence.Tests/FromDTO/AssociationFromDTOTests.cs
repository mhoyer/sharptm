// <copyright file="AssociationFromDTOTests.cs" company="Pixelplastic">
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
	public class When_mapping_an_association : With_Filled_TopicMapDTO
	{
		static ITopicMap topicMap;
		static IAssociation association;

		Given an_empty_TMAPI_topic_map =
			() => topicMap = topicMapSystem
			                 	.CreateTopicMap("http://sharptm.de/" + typeof(When_mapping_an_association).FullName);
		Given the_mapped_type = () => TopicFromDTO.Create(topicMap, knowsAbout);

		Because of_mapping_the_association = () => association = AssociationFromDTO.Create(topicMap, marcelKnowsAboutLutz);

		It should_map_the_item_identifiers = () => association.ItemIdentifiers.Count.ShouldEqual(marcelKnowsAboutLutz.ItemIdentities.Count);
		It should_map_the_association_type = () => association.Type.ShouldEqual(topicMap.GetTopicBySubjectIdentifier(topicMapSystem.CreateLocator(marcelKnowsAboutLutz.Type.TopicReference.HRef)));
		It should_map_the_roles = () => association.Roles.Count.ShouldEqual(marcelKnowsAboutLutz.Roles.Count);
		It should_create_the_role_types = () => association.RoleTypes.Count.ShouldEqual(1);
	}

	public class When_mapping_a_reified_association : With_Filled_TopicMapDTO
	{
		static IAssociation association;
		static ITopicMap topicMap;
		static ITopic reifier;
		static TopicDTO reifierDTO;

		Given a_reifier =
			() =>
				{
					var reifierSID = "http://sharptm.de/" + typeof(When_mapping_a_reified_association).FullName;
					reifierDTO = topicMapDTO.CreateTopic(reifierSID);
					marcelKnowsAboutLutz.Reifier = reifierSID;
				};

		Given an_empty_TMAPI_topic_map =
			() => topicMap = topicMapSystem
			                 	.CreateTopicMap("http://sharptm.de/" + typeof(When_mapping_a_reified_association).FullName);
		Given the_converted_reifier = () => reifier = TopicFromDTO.Create(topicMap, reifierDTO);

		Because of_mapping_the_association = () => association = AssociationFromDTO.Create(topicMap, marcelKnowsAboutLutz);

		It should_map_the_reifier = () => association.Reifier.ShouldEqual(reifier);
	}

	public class When_mapping_a_scoped_association : With_Filled_TopicMapDTO
	{
		static IAssociation association;
		static ITopicMap topicMap;
		static ITopic scope;
		static TopicDTO scopeDTO;

		Given a_scope =
			() =>
				{
					var scopeSID = "http://sharptm.de/" + typeof(When_mapping_a_scoped_association).FullName;
					scopeDTO = topicMapDTO.CreateTopic(scopeSID);
					marcelKnowsAboutLutz.Scope = new ScopeDTO();
					marcelKnowsAboutLutz.Scope.TopicReferences.Add(scopeDTO.SubjectIdentifiers[0]);
				};
		Given an_empty_TMAPI_topic_map =
			() => topicMap = topicMapSystem
			                 	.CreateTopicMap("http://sharptm.de/" + typeof(When_mapping_a_scoped_association).FullName);
		Given the_converted_scope = () => scope = TopicFromDTO.Create(topicMap, scopeDTO);

		Because of_mapping_the_association = () => association = AssociationFromDTO.Create(topicMap, marcelKnowsAboutLutz);

		It should_map_the_scope = () => association.Scope.ShouldContain(scope);
		It should_map_only_one_scope = () => association.Scope.Count.ShouldEqual(marcelKnowsAboutLutz.Scope.TopicReferences.Count);
	}
}