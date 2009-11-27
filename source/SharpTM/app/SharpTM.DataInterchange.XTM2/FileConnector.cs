// <copyright file="FileConnector.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.IO;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2
{
	public class FileConnector<TConstruct> : IConnector<TConstruct> where TConstruct : IConstructDTO
	{
		protected Func<TConstruct, Stream> SerializeAction { get; set; }

		protected FileConnector() { }

		public FileConnector(Func<TConstruct, Stream> serializeAction)
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

		public TopicMapDTO Load(Stream xtmStream, string baseLocator)
		{
			TopicMapDTO result = SerializeAction.Invoke(xtmStream) as TopicMapDTO;
			
			if (result == null)
			{
				throw new DataInterchangeException("Unable to convert from stream to topic map.");
			}

			result.BaseLocator = baseLocator;
			return result;
		}

		public void Save(TConstruct dto)
		{
			throw new NotImplementedException();
		}
	}
}