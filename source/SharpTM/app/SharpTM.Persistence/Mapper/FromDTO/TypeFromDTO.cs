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
			return TopicFromDTO.FindOrCreate(topicMap, typeDTO.TopicReference);
		}

		public static ITopic Find(ITopicMap topicMap, TypeDTO typeDTO)
		{
			return TopicFromDTO.Find(topicMap, typeDTO.TopicReference);
		}
	}
}