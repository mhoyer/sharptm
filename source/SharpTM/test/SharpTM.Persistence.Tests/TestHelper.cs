// <copyright file="TestHelper.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public static class TestHelper
	{
		public static OccurrenceDTO CreateOccurrence(TypeDTO occurrenceType)
		{
			OccurrenceDTO occurrenceDTO = new OccurrenceDTO();
			occurrenceDTO.Type = occurrenceType;

			return occurrenceDTO;
		}

		public static OccurrenceDTO CreateOccurrence(TypeDTO occurrenceType, LocatorDTO resourceReference)
		{
			OccurrenceDTO occurrenceDTO = CreateOccurrence(occurrenceType);
			occurrenceDTO.ResourceReference = resourceReference;

			return occurrenceDTO;
		}

		public static OccurrenceDTO CreateOccurrence(TypeDTO occurrenceType, ResourceDataDTO resourceData)
		{
			OccurrenceDTO occurrenceDTO = CreateOccurrence(occurrenceType);
			occurrenceDTO.ResourceData = resourceData;

			return occurrenceDTO;
		}

		public static LocatorDTO CreateLocator(string relativeUri)
		{
			return new LocatorDTO() { HRef = CreateLocatorString(relativeUri) };
		}

		public static string CreateLocatorString(string relativeUri)
		{
			return String.Format("http://sharptm.de/{0}", relativeUri);
		}

		public static TopicDTO CreateTopic(this TopicMapDTO topicMapDTO, params string[] subjectIdentifiers)
		{
			var topic = new TopicDTO();
			topic.InstanceOf = new InstanceOfDTO();

			foreach (string identifier in subjectIdentifiers)
			{
				topic.SubjectIdentifiers.Add(new LocatorDTO() { HRef = identifier });
			}

			topicMapDTO.Topics.Add(topic);

			return topic;
		}

		public static AssociationDTO CreateAssociation(TypeDTO associationType,
														TypeDTO roleType1, TopicDTO role1,
														TypeDTO roleType2, TopicDTO role2)
		{
			var association = new AssociationDTO();

			var associationLocator = new LocatorDTO();
			associationLocator.HRef = associationType.TopicReference.HRef + association.GetHashCode();

			association.ItemIdentities.Add(associationLocator);
			association.Type = associationType;
			association.Roles.Add(new RoleDTO() { Type = roleType1, TopicReference = role1.SubjectIdentifiers[0] });
			association.Roles.Add(new RoleDTO() { Type = roleType2, TopicReference = role2.SubjectIdentifiers[0] });

			return association;
		}

		public static NameDTO CreateName(string name, params string[] variants)
		{
			var nameDTO = new NameDTO();
			nameDTO.Value = name;

			foreach (string variant in variants)
			{
				var variantDTO = new VariantDTO();
				variantDTO.ResourceData = new ResourceDataDTO() { Text = variant };

				nameDTO.Variants.Add(variantDTO);
			}

			return nameDTO;
		}
	}
}