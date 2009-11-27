// <copyright file="TopicMapMediator.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Helper
{
	public class TopicMapMediator : Mediator<TopicMap, TopicMapEntity>
	{
		public TopicMapMediator(ITopicMapRepository repository, TopicMapSystem topicMapSystem)
			: base(repository, entity => new TopicMap(entity, topicMapSystem))
		{
		}

		public TopicMap Create(TopicMapEntity entity, ILocator baseLocator)
		{
			var topicMap = Create(entity);
			topicMap.BaseLocator = baseLocator;

			return topicMap;
		}
	}
}