// <copyright file="ConstructEntity.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities
{
	public class ConstructEntity
	{
		public virtual string Id { get; set; }
		public virtual IList<string> ItemIdentifiers { get; set; }
	}
}