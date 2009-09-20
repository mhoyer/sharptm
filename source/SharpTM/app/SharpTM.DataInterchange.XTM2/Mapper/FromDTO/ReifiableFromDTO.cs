// <copyright file="ReifiableFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	public class ReifiableFromDTO : ClassMapper<IReifiableDTO, IReifiable>
	{
		static ReifiableFromDTO mapper;

		ReifiableFromDTO()
		{
			From(dto => dto.Reifier)
				.To((reifiable, reifierId)
				    =>
				    	{
				    		if (reifierId != null)
				    		{
				    			reifiable.Reifier = TopicFromDTO.FindOrCreate(
				    				reifiable.TopicMap,
				    				reifierId);
				    		}
				    	});
		}

		public static ReifiableFromDTO Instance
		{
			get
			{
				if (mapper == null)
				{
					mapper = new ReifiableFromDTO();
				}

				return mapper;
			}
		}
	}
}