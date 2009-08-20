// <copyright file="OccurrenceFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class OccurrenceFromDTO : ConstructFromDTO<OccurrenceDTO, IOccurrence>
	{
		private static OccurrenceFromDTO mapper = new OccurrenceFromDTO();

		private OccurrenceFromDTO()
		{
			From(dto => dto.Scope)
				.To((occurrence, scopeDTO)
				    =>
				    	{
				    		if (scopeDTO != null)
				    		{
				    			ScopeFromDTO.Instance.Map(scopeDTO, occurrence);
				    		}
				    	});
		}

		public static IOccurrence Create(ITopic parent, OccurrenceDTO source)
		{
			IOccurrence target = parent.CreateOccurrence(
				TypeFromDTO.FindOrCreate(parent.TopicMap, source.Type),
				string.Empty);

			mapper.Map(source, target);
			DatatypeAwareFromDTO.Instance.Map(source, target);
			ReifiableFromDTO.Instance.Map(source, target);

			return target;
		}
	}
}