// <copyright file="LocatorToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO
{
	public class LocatorToDTO : ClassMapper<ILocator, LocatorDTO>
	{
		private static LocatorToDTO mapper = new LocatorToDTO();

		public LocatorToDTO()
		{
			From(locator => locator.Reference)
				.To((xtm, locatorRef) =>
				    	{
				    		xtm.HRef = locatorRef;
				    	});
		}

		public static LocatorDTO Create(ILocator source)
		{
			LocatorDTO target = new LocatorDTO();
			mapper.Map(source, target);

			return target;
		}
	}
}