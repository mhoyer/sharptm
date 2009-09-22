// <copyright file="TopicRepository.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories.InMemory
{
	public class TopicRepository : Repository<TopicEntity>, ITopicRepository
	{
		
	}
}