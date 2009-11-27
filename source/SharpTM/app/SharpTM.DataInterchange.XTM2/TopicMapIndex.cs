// <copyright file="TopicMapIndex.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2
{
	public class TopicMapIndex
	{
		static readonly Dictionary<ITopicMap, string> _topicMaps = new Dictionary<ITopicMap, string>();

		public static void Register(ITopicMap target, string baseLocator)
		{
			if (!_topicMaps.ContainsKey(target))
			{
				_topicMaps.Add(target, baseLocator);
			}
			else
			{
				_topicMaps[target] = baseLocator;
			}
		}

		public static string GetBaseLocatorByTopicMap(ITopicMap topicMap)
		{
			if (!_topicMaps.ContainsKey(topicMap)) return null;

			return _topicMaps[topicMap];
		}
	}
}