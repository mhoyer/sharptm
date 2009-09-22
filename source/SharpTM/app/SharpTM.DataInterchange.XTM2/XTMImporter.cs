// <copyright file="XTMImporter.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2
{
	public class XTMImporter : IImporter<ITopicMap>, IExporter<ITopicMap>
	{
		readonly IBridge<TopicMapDTO, ITopicMap> _bridge;
		IConnector<TopicMapDTO> _connector;

		public XTMImporter(
			IConnector<TopicMapDTO> connector,
			IBridge<TopicMapDTO, ITopicMap> bridge)
		{
			if (connector == null) throw new ArgumentNullException("connector");
			if (bridge == null) throw new ArgumentNullException("bridge");

			_connector = connector;
			_bridge = bridge;
		}

		public ITopicMap Import(object id)
		{
			return Load(id, null);
		}

		public ITopicMap Load(string xtmFile)
		{
			return Load(xtmFile, xtmFile);
		}

		public ITopicMap Load(object id, string baseLocator)
		{
			TopicMapDTO topicMapDTO = _connector.Load(id);

			if (baseLocator != null)
			{
				topicMapDTO.ItemIdentities.Insert(0, new LocatorDTO() { HRef = baseLocator });
			}
			else if(topicMapDTO.ItemIdentities.Count == 0)
			{
				throw new DataInterchangeException(String.Format("Unable to load TopicMap from {0}. At least one item identifier required for a topic map.", id));
			}

			try
			{
				return _bridge.Map(topicMapDTO);
			}
			catch (Exception ex)
			{
				throw new DataInterchangeException(String.Format("Unable to load TopicMap from {0}.", id), ex);
			}
		}

		public void Export(ITopicMap entity)
		{
			_connector.Save(_bridge.Map(entity));
		}
	}
}