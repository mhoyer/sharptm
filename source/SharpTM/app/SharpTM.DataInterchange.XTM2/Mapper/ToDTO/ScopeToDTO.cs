// <copyright file="ScopeToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.ToDTO
{
	public class ScopeToDTO : ClassMapper<IScoped, IScopedDTO>
	{
		public ScopeToDTO()
		{
			From(variant => variant.Scope)
				.To((dto, variantScopes) =>
				    	{
				    		if (variantScopes.Count > 0)
				    		{
				    			dto.Scope = new ScopeDTO();
				    		}

				    		foreach (ITopic scope in variantScopes)
				    		{
				    			if (scope.ItemIdentifiers.Count > 0)
				    			{
				    				dto.Scope.TopicReferences.Add(LocatorToDTO.Create(scope.ItemIdentifiers[0]));
				    			}
				    			else if (scope.SubjectIdentifiers.Count > 0)
				    			{
				    				dto.Scope.TopicReferences.Add(LocatorToDTO.Create(scope.SubjectIdentifiers[0]));
				    			}
				    			else
				    			{
				    				throw new MappingException("Unable to map a scope due to missing item and subject identifiers.");
				    			}
				    		}
				    	});
		}
	}
}