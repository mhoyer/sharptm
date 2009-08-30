// <copyright file="NameFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
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

		private NameFromDTO()
		{
			From(dto => dto.Variants)
				.To((name, variants)
				    =>
				    	{
				    		foreach (VariantDTO variantDTO in variants)
				    		{
				    			VariantFromDTO.Create(name, variantDTO);
				    		}
				    	});
		}

		public static IName Create(ITopic parent, NameDTO source)
		{
			try
			{
				ITopic nameType;

				if (source.Type == null)
				{
					nameType = TopicFromDTO.Find(parent.TopicMap, PSI_TOPIC_NAME) ??
							parent.TopicMap.CreateTopicBySubjectIdentifier(LocatorFromDTO.Create(parent.TopicMap, PSI_TOPIC_NAME));
				}
				else
				{
					nameType = TypeFromDTO.FindOrCreate(parent.TopicMap, source.Type);
				}

				IName target = parent.CreateName(nameType, source.Value);
				mapper.Map(source, target);
				ReifiableFromDTO.Instance.Map(source, target);
				ScopeFromDTO.Instance.Map(source, target);

				return target;
			}
			catch (Exception ex)
			{
				var message = String.Format("Unable to create name item for '{0}' of topic '{1}'.", source.Value, parent);

				throw new MappingException(message, ex);
			}
		}
	}
}