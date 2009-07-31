// <copyright file="RoleFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class RoleFromDTO : ConstructFromDTO<RoleDTO, IRole>
	{
		private static readonly RoleFromDTO mapper = new RoleFromDTO();
		
		private RoleFromDTO()
		{
			From(dto => dto.Reifier)
				.To((role, reifierId)
				    =>
				    	{
							role.Reifier = TopicFromDTO.FindOrCreate(
								role.TopicMap, 
								reifierId);
				    	});
				
		}

		public static IRole Create(IAssociation association, RoleDTO roleDTO)
		{
			var role = association.CreateRole(
				TypeFromDTO.FindOrCreate(association.Parent, roleDTO.Type),
				TopicFromDTO.FindOrCreate(association.Parent, roleDTO.TopicReference));

			return mapper.Map(roleDTO, role);
		}
	}
}