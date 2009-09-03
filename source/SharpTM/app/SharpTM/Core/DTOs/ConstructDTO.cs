// <copyright file="ConstructDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	public class ConstructDTO
	{
		protected internal ConstructDTO()
		{
			Id = Guid.NewGuid().ToString();
			ItemIdentifiers = new ReadOnlyCollectionWithLimitedAccess<ILocator>();
		}

		public string Id { get; set; }

		public ITopicMap TopicMap { get; set; }

		public IConstruct Parent { get; set; }

		public ReadOnlyCollectionWithLimitedAccess<ILocator> ItemIdentifiers { get; set; }
	}
}