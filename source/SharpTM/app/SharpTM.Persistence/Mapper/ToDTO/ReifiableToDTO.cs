// <copyright file="ReifiableToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO
{
	public class ReifiableToDTO : ClassMapper<IReifiable, IReifiableDTO> 
	{
		public ReifiableToDTO()
		{
			From(reifier => reifier)
				.To((dto, reifiable) =>
				    	{
				    		if (reifiable != null &&
								reifiable.Reifier != null &&
				    		    reifiable.Reifier.ItemIdentifiers.Count > 0)
				    		{
				    			dto.Reifier = reifiable.Reifier.ItemIdentifiers[0].Reference;
				    		}
				    	}
				);
		}
	}
}