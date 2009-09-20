// <copyright file="TypeFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	public class TypeFromDTO : Mapper<TypeDTO, ITopic>
	{
		TypeFromDTO()
		{
		}

		public static ITopic Find(ITopicMap topicMap, TypeDTO typeDTO)
		{
			return TopicFromDTO.Find(topicMap, typeDTO.TopicReference);
		}

		public static ITopic FindOrCreate(ITopicMap topicMap, TypeDTO typeDTO)
		{
			return TopicFromDTO.FindOrCreate(topicMap, typeDTO.TopicReference);
		}
	}
}