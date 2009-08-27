// <copyright file="Connector.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.IO;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Connectors
{
	public abstract class Connector<TConstruct> : IConnector<TConstruct> where TConstruct : IConstructDTO
	{
		protected Func<TConstruct, Stream> SerializeAction { get; set; }

		protected Connector() { }

		public Connector(Func<TConstruct, Stream> serializeAction)
		{
			SerializeAction = serializeAction;
		}

		[Obsolete]
		public TConstruct Load(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}

			return Load(id.ToString());
		}

		public TConstruct Load(string xtmFileName)
		{
			return Load(File.OpenRead(xtmFileName));
		}

		public TConstruct Load(Stream xtmStream)
		{
			return SerializeAction.Invoke(xtmStream);
		}

		public void Save(TConstruct dto)
		{
			throw new NotImplementedException();
		}
	}
}