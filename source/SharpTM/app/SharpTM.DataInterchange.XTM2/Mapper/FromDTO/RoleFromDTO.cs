// <copyright file="RoleFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	public class RoleFromDTO : ConstructFromDTO<RoleDTO, IRole>
	{
		static readonly RoleFromDTO mapper = new RoleFromDTO();

		public static IRole Create(IAssociation association, RoleDTO source)
		{
			IRole target = association.CreateRole(
				TypeFromDTO.FindOrCreate(association.Parent, source.Type),
				TopicFromDTO.FindOrCreate(association.Parent, source.TopicReference));

			mapper.Map(source, target);
			ReifiableFromDTO.Instance.Map(source, target);

			return target;
		}
	}
}