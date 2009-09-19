// <copyright file="NameEntity.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities
{
	public class NameEntity : ConstructEntity
	{
		public virtual TopicEntity Reifier { get; set; }
		public virtual TopicEntity Type { get; set; }
		public virtual string Value { get; set; }
		public virtual IList<TopicEntity> Scope { get; set; }
		public virtual IList<VariantEntity> Variants { get; set; }
	}
}