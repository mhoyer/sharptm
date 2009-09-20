// <copyright file="ConstructToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.ToDTO
{
	/// <summary>
	/// Converts the base properties of an <see cref="IConstruct"/> object 
	/// to an <see cref="IConstructDTO"/> object.
	/// </summary>
	/// <typeparam name="TSourceType">The type of the source type.</typeparam>
	/// <typeparam name="TTargetType">The type of the target type.</typeparam>
	public abstract class ConstructToDTO<TSourceType, TTargetType> : ClassMapper<TSourceType, TTargetType>
		where TSourceType : class, IConstruct
		where TTargetType : class, IConstructDTO, new()
	{
		protected ConstructToDTO()
		{
			From(construct => construct.ItemIdentifiers)
				.To((dto, itemIdentifiers) =>
				    	{
				    		foreach (ILocator itemIdentifier in itemIdentifiers)
				    		{
				    			dto.ItemIdentities.Add(LocatorToDTO.Create(itemIdentifier));
				    		}
				    	}
				);
		}
	}
}