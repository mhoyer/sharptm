// <copyright file="MappingException.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Runtime.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Helper
{
	public class MappingException : Exception
	{
		public MappingException()
		{
		}

		public MappingException(string message)
			: base(message)
		{
		}

		public MappingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected MappingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}