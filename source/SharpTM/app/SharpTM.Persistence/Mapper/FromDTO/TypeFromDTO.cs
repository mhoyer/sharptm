// <copyright file="TypeFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class TypeFromDTO : Mapper<TypeDTO, ITopic>
	{
		private TypeFromDTO() {}

		public static ITopic FindOrCreate(ITopicMap topicMap, TypeDTO typeDTO)
		{
			var typeLocator = LocatorFromDTO.Create(topicMap, typeDTO.TopicReference);

			ITopic topicType = topicMap.GetTopicBySubjectIdentifier(typeLocator) ??
			                   topicMap.GetTopicBySubjectLocator(typeLocator) ??
			                   topicMap.GetConstructByItemIdentifier(typeLocator) as ITopic ??
			                   topicMap.CreateTopicByItemIdentifier(typeLocator);
			
			return topicType;
		}
	}
}