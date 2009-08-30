// <copyright file="LTMConnector.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Connectors
{
	[Obsolete("Not implemented yet.")]
	public class LTMConnector : FileConnector<TopicMapDTO>
	{
		public LTMConnector()
		{
			SerializeAction =
				(stream) => { throw new NotImplementedException(); };
		}
	}
}