// <copyright file="AssociationFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

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
			From(dto => dto.Roles)
				.To((association, roleDTOs)
				    =>
				    	{
				    		foreach (RoleDTO roleDTO in roleDTOs)
				    		{
				    			RoleFromDTO.Create(association, roleDTO);
				    		}
				    	});
		}

		public static IAssociation Create(ITopicMap parent, AssociationDTO source)
		{
			IAssociation target = parent.CreateAssociation(TypeFromDTO.FindOrCreate(parent, source.Type));
			mapper.Map(source, target);
			ReifiableFromDTO.Instance.Map(source, target);
			ScopeFromDTO.Instance.Map(source, target);

			return target;
		}
	}
}