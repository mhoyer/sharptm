// <copyright file="RoleData.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	public class RoleData : ConstructData
	{
		public ITopic Player { get; set; }
		public ITopic Reifier { get; set; }
		public ITopic Type { get; set; }
	}
}