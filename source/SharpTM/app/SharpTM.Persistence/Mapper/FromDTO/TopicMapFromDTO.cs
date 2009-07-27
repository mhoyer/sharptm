// <copyright file="TopicMapFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	/// <summary>
	/// Converts a <see cref="TopicMapDTO"/> to an instance of <see cref="ITopicMap"/>.
	/// </summary>
	public class TopicMapFromDTO : ConstructFromDTO<TopicMapDTO, ITopicMap>
	{
		private static readonly TopicMapFromDTO mapper = new TopicMapFromDTO();

		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMapFromDTO"/> class.
		/// </summary>
		private TopicMapFromDTO()
		{
			From(dto => dto.Topics)
				.To((tm, topics) =>
				{
					foreach (TopicDTO dto in topics)
					{
						TopicFromDTO.Create(tm, dto);
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
			return mapper.Map(source, topicMapSystem.CreateTopicMap(iri));
		}
	}
}