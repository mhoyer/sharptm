// <copyright file="RepositoryFactory.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories
{
	public class TopicMapRepository : IRepository<ITopicMap>
	{
		readonly IBridge<TopicMapDTO, ITopicMap> _bridge;
		IConnector<TopicMapDTO> _connector;

		public TopicMapRepository(
			IConnector<TopicMapDTO> connector,
			IBridge<TopicMapDTO, ITopicMap> bridge)
		{
			if (connector == null) throw new ArgumentNullException("connector");
			if (bridge == null) throw new ArgumentNullException("bridge");

			_connector = connector;
			_bridge = bridge;
		}

		public ITopicMap Load(object id)
		{
			return _bridge.Map(_connector.Load(id));
		}

		public void Save(ITopicMap entity)
		{
			_connector.Save(_bridge.Map(entity));
		}
	}
}