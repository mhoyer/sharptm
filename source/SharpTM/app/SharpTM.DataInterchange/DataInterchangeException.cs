// <copyright file="DataInterchangeException.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange
{
	public class DataInterchangeException : Exception
	{
		public DataInterchangeException(string message)
			: base(message)
		{ }

		public DataInterchangeException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}