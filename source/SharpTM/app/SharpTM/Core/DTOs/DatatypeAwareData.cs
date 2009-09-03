// <copyright file="DatatypeAwareData.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	public abstract class DatatypeAwareData : ConstructData
	{
		public ITopic Reifier { get; set; }
		public string Value { get; set; }
		public string Datatype { get; set; }
		
		public string Scope { get; set; }
	}
}