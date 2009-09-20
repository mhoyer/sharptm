// <copyright file="LocatorToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.ToDTO
{
	public class LocatorToDTO : ClassMapper<ILocator, LocatorDTO>
	{
		static readonly LocatorToDTO mapper = new LocatorToDTO();

		public LocatorToDTO()
		{
			From(locator => locator.Reference)
				.To((dto, locatorRef) => { dto.HRef = locatorRef; });
		}

		public static LocatorDTO Create(ILocator source)
		{
			LocatorDTO target = new LocatorDTO();
			mapper.Map(source, target);

			return target;
		}
	}
}