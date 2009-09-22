// <copyright file="TopicMapRepository.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories.InMemory
{
	public class TopicMapRepository : Repository<TopicMapEntity>, ITopicMapRepository 
	{
		ITopicRepository _topicRepository;
		IAssociationRepository _associationRepository;

		public ITopicRepository TopicRepository
		{
			get
			{
				if (_topicRepository == null)
				{
					_topicRepository = new TopicRepository();
				}

				return _topicRepository;
			}
		}

		public IAssociationRepository AssociationRepository
		{
			get
			{
				if (_associationRepository == null)
				{
					_associationRepository = new AssociationRepository();
				}

				return _associationRepository;
			}
		}
	}
}