// <copyright file="ConstructFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	public class ConstructFromDTO<TSourceType, TTargetType> : ClassMapper<TSourceType, TTargetType>
		where TSourceType : class, IConstructDTO, new()
		where TTargetType : class, IConstruct
	{
		protected ConstructFromDTO()
		{
			From(dto => dto.ItemIdentities)
				.To((construct, itemIdentifiers) =>
				    	{
				    		foreach (LocatorDTO itemIdentifierDTO in itemIdentifiers)
				    		{
				    			construct.AddItemIdentifier(
				    				LocatorFromDTO.Create(construct.TopicMap, itemIdentifierDTO.HRef));
				    		}
				    	});
		}
	}
}