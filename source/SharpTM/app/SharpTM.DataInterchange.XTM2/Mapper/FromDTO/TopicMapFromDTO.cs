// <copyright file="TopicMapFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	/// <summary>
	/// Converts a <see cref="TopicMapDTO"/> to an instance of <see cref="ITopicMap"/>.
	/// </summary>
	public class TopicMapFromDTO : ConstructFromDTO<TopicMapDTO, ITopicMap>
	{
		static readonly TopicMapFromDTO mapper = new TopicMapFromDTO();

		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMapFromDTO"/> class.
		/// </summary>
		TopicMapFromDTO()
		{
			From(dto => dto.Topics)
				.To((tm, topics) =>
				    	{
				    		foreach (TopicDTO topicDTO in topics)
				    		{
				    			TopicFromDTO.Create(tm, topicDTO);
				    		}
				    	});

			From(dto => dto.Associations)
				.To((tm, associations) =>
				    	{
				    		foreach (AssociationDTO associationDTO in associations)
				    		{
				    			AssociationFromDTO.Create(tm, associationDTO);
				    		}
				    	});
		}

		public static ITopicMap Create(ITopicMapSystem topicMapSystem, TopicMapDTO source)
		{
			if (source.ItemIdentities.Count == 0)
			{
				throw new MappingException("Unable to create Topic Map without identifiers.");
			}

			return Create(topicMapSystem, source.ItemIdentities[0].HRef, source);
		}

		public static ITopicMap Create(ITopicMapSystem topicMapSystem, string iri, TopicMapDTO source)
		{
			ITopicMap target = topicMapSystem.CreateTopicMap(iri);
			mapper.Map(source, target);
			ReifiableFromDTO.Instance.Map(source, target);

			return target;
		}
	}
}