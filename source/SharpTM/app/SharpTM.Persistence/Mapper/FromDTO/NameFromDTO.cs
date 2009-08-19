// <copyright file="NameFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class NameFromDTO : ConstructFromDTO<NameDTO, IName>
	{
		static NameFromDTO mapper = new NameFromDTO();

		/// <summary>
		/// <see href="http://www.isotopicmaps.org/sam/sam-xtm/#d0e776">4.10 The name element</see>
		/// </summary>
		const string PSI_TOPIC_NAME = "http://psi.topicmaps.org/iso13250/model/topic-name";
		static TypeDTO psiTopicName = new TypeDTO(PSI_TOPIC_NAME);

		private NameFromDTO()
		{
			// From(dto => dto.ResourceData)
		}

		public static IName Create(ITopic parent, NameDTO source)
		{
			if (source.Type == null)
			{
				source.Type = psiTopicName;
			}

			IName target = parent.CreateName(
			    TypeFromDTO.FindOrCreate(parent.TopicMap, source.Type), source.Value);

			return mapper.Map(source, target);
		}
	}
}