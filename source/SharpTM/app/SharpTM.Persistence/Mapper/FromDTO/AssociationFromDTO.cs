// <copyright file="AssociationFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;
using TMAPI.Net.Index;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	/// <summary>
	/// Converts from <see cref="AssociationDTO"/> to <see cref="IAssociation"/>
	/// </summary>
	public class AssociationFromDTO : ConstructFromDTO<AssociationDTO, IAssociation>
	{
		private static AssociationFromDTO mapper = new AssociationFromDTO();

		private AssociationFromDTO()
		{
			From(dto => dto.Type)
				.To((association, typeDTO) =>
				    	{
				    		// TODO: association.
				    	});

			// TODO: From(dto => dto.Roles)
			// TODO: From(dto => dto.Reifier)
			// TODO: From(dto => dto.Scope)
		}

		public static IAssociation Create(ITopicMap topicMap, AssociationDTO source)
		{
			IAssociation association = topicMap.CreateAssociation(TypeFromDTO.FindOrCreate(topicMap, source.Type));

			return association;
		}
	}
}