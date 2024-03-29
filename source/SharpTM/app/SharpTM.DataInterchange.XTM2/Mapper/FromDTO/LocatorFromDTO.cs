// <copyright file="LocatorFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	public class LocatorFromDTO : ClassMapper<LocatorDTO, ILocator>
	{
		LocatorFromDTO()
		{
		}

		public static ILocator Create(ITopicMap topicMap, LocatorDTO locator)
		{
			return Create(topicMap, locator.HRef);
		}

		public static ILocator Create(ITopicMap topicMap, string locator)
		{
			if (!Uri.IsWellFormedUriString(locator, UriKind.Absolute))
			{
				var baseLocator = TopicMapIndex.GetBaseLocatorByTopicMap(topicMap);

				if (locator[0] == '#')
				{
					locator = locator.Remove(0, 1);
				}

				locator = string.Format("{0}#{1}", baseLocator, locator);
			}

			return topicMap.CreateLocator(locator);
		}

		public static ILocator Create(ITopicMapSystem topicMapSystem, LocatorDTO locator)
		{
			return Create(topicMapSystem, locator.HRef);
		}

		public static ILocator Create(ITopicMapSystem topicMapSystem, string locator)
		{
			return topicMapSystem.CreateLocator(locator);
		}
	}
}