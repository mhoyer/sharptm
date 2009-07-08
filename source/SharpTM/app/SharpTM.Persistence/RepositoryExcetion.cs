// <copyright file="RepositoryExcetion.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence
{
	public class RepositoryExcetion : Exception
	{
		public RepositoryExcetion(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}