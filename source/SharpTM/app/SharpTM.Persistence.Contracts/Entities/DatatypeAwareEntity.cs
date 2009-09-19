// <copyright file="DatatypeAwareEntity.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities
{
	public abstract class DatatypeAwareEntity : ConstructEntity
	{
		public virtual string Datatype { get; set; }
		public virtual TopicEntity Reifier { get; set; }
		public virtual IList<TopicEntity> Scope { get; set; }
		public virtual string Value { get; set; }
	}
}