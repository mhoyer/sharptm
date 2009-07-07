// <copyright file="TopicToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO
{

	public class TopicToDTO : ConstructToDTO<ITopic, TopicDTO>
	{
		private static TopicToDTO mapper;

		public TopicToDTO()
		{

		}

		public static TopicDTO Create(ITopic source)
		{
			TopicDTO target = new TopicDTO();
			mapper.Map(source, target);

			return target;
		}
	}
}