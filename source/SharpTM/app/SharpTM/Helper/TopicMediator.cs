// <copyright file="TopicMediator.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;

namespace Pixelplastic.TopicMaps.SharpTM.Helper
{
	public class TopicMediator : Mediator<Topic, TopicEntity>
	{
		public TopicMediator(ITopicRepository repository, TopicMap topicMap)
			: base(repository, entity => new Topic(entity, topicMap))
		{
		}
	}
}