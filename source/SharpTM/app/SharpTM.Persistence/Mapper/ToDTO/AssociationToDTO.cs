// <copyright file="AssociationToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO
{
	public class AssociationToDTO : ClassMapper<IAssociation, AssociationDTO>
	{
		private static readonly AssociationToDTO mapper = new AssociationToDTO();

		public AssociationToDTO()
		{
			From(assoc => assoc.ItemIdentifiers)
				.To((xtm, id) =>
				    	{
				    		foreach (ILocator locator in id)
				    		{
				    			xtm.ItemIdentities.Add(LocatorToDTO.Create(locator));
				    		}
				    	});

			//From(association => association.Roles)
		}

		public static AssociationDTO Create(IAssociation source)
		{
			AssociationDTO target = new AssociationDTO();
			mapper.Map(source, target);

			return target;
		}
	}
}