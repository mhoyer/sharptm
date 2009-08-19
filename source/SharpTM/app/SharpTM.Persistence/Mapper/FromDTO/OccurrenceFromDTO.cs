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
			From(dto => dto.ResourceData)
				.To((occurrence, resourceData) =>
				    	{
				    		if (resourceData != null)
				    		{
				    			if (!string.IsNullOrEmpty(resourceData.Datatype))
				    			{
									// TODO What if the data type is #anyType? see http://www.isotopicmaps.org/sam/sam-xtm/#sect-xml-canonicalization
									if (resourceData.Datatype == "http://www.w3.org/2001/XMLSchema#anyType")
									{
										throw new NotSupportedException("Occurrences of data type #anyType are not supported yet.");
									}

									occurrence.SetValue(
										resourceData.Text, 
										LocatorFromDTO.Create(occurrence.TopicMap, resourceData.Datatype));
				    			}
				    			else
				    			{
				    				occurrence.Value = resourceData.Text;
				    			}
				    		}
				    	});

			From(dto => dto.ResourceReference)
				.To((occurrence, resourceReference) =>
				    	{
				    		if (resourceReference != null)
				    		{
				    			occurrence.LocatorValue = 
									LocatorFromDTO.Create(occurrence.TopicMap, resourceReference);
				    		}
				    	});

			From(dto => dto.Reifier)
				.To((occurrence, reifierId)
				    =>
				    	{
				    		if (reifierId != null)
				    		{
				    			occurrence.Reifier = TopicFromDTO.FindOrCreate(
				    				occurrence.TopicMap,
				    				reifierId);
				    		}
				    	});

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

		public static IOccurrence Create(ITopic parent, OccurrenceDTO occurrenceDTO)
		{
			IOccurrence occurrence = parent.CreateOccurrence(
				TypeFromDTO.FindOrCreate(parent.TopicMap, occurrenceDTO.Type),
				string.Empty);

			return mapper.Map(occurrenceDTO, occurrence);
		}
	}
}