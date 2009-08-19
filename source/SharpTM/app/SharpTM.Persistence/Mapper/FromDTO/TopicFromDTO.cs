// <copyright file="TopicFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	/// <summary>
	/// Converts a <see cref="TopicDTO"/> to an instance of <see cref="ITopic"/>.
	/// </summary>
	public class TopicFromDTO : ConstructFromDTO<TopicDTO, ITopic>
	{
		private static TopicFromDTO mapper = new TopicFromDTO();

		private TopicFromDTO()
		{
			From(dto => dto.Id)
				.To((topic, id) =>
				    	{
							if (!string.IsNullOrEmpty(id))
							{
								topic.AddItemIdentifier(LocatorFromDTO.Create(topic.Parent, id));
							}
				    	});

			From(dto => dto.SubjectIdentifiers)
				.To((topic, subjectIdentifiers) =>
				    	{
				    		foreach (LocatorDTO subjectIdentifier in subjectIdentifiers)
				    		{
				    			topic.AddSubjectIdentifier(
				    				LocatorFromDTO.Create(topic.Parent, subjectIdentifier));
				    		}
				    	});

			From(dto => dto.SubjectLocators)
				.To((topic, subjectLocators) =>
				    	{
				    		foreach (LocatorDTO subjectLocator in subjectLocators)
				    		{
				    			topic.AddSubjectLocator(
				    				LocatorFromDTO.Create(topic.Parent, subjectLocator));
				    		}
				    	});

			From(dto => dto.Occurrences)
				.To((topic, occurrences)
				=>
				    	{
				    		foreach (OccurrenceDTO occurrenceDTO in occurrences)
				    		{
				    			OccurrenceFromDTO.Create(topic, occurrenceDTO);
				    		}
				    	});
			// From(dto => dto.Names)
		}

		public static ITopic Create(ITopicMap topicMap, TopicDTO source)
		{
			ITopic topic;

			if (source.ItemIdentities.Count > 0)
			{
				topic = topicMap.CreateTopicByItemIdentifier(
					LocatorFromDTO.Create(topicMap, source.ItemIdentities[0]));
			}
			else if (source.SubjectIdentifiers.Count > 0)
			{
				topic = topicMap.CreateTopicBySubjectIdentifier(
					LocatorFromDTO.Create(topicMap, source.SubjectIdentifiers[0]));
			}
			else if (source.SubjectLocators.Count > 0)
			{
				topic = topicMap.CreateTopicBySubjectLocator(
					LocatorFromDTO.Create(topicMap, source.SubjectLocators[0]));
			}
			else
			{
				topic = topicMap.CreateTopic();
			}

			return mapper.Map(source, topic);
		}

		public static ITopic FindOrCreate(ITopicMap topicMap, string locator)
		{
			return FindOrCreate(topicMap, LocatorFromDTO.Create(topicMap, locator));
		}

		public static ITopic FindOrCreate(ITopicMap topicMap, LocatorDTO locatorDTO)
		{
			return FindOrCreate(topicMap, LocatorFromDTO.Create(topicMap, locatorDTO));
		}

		public static ITopic FindOrCreate(ITopicMap topicMap, ILocator locator)
		{
			ITopic topic = topicMap.GetTopicBySubjectIdentifier(locator) ??
							   topicMap.GetTopicBySubjectLocator(locator) ??
							   topicMap.GetConstructByItemIdentifier(locator) as ITopic ??
							   topicMap.CreateTopicByItemIdentifier(locator);

			return topic;
		}
	}
}