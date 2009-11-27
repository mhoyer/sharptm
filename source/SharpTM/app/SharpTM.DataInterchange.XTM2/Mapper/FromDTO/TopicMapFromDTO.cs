// <copyright file="TopicMapFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
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
			ITopicMap target = topicMapSystem.CreateTopicMap(source.BaseLocator);
			TopicMapIndex.Register(target, source.BaseLocator);
			mapper.Map(source, target);
			ReifiableFromDTO.Instance.Map(source, target);

			return target;
		}
	}
}