// <copyright file="RepositoryException.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence
{
	public class RepositoryException : Exception
	{
		public RepositoryException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}