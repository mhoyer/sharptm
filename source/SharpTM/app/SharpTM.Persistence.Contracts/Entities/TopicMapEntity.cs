// <copyright file="TopicMapEntity.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities
{
	public class TopicMapEntity : ConstructEntity
	{
		public virtual IList<AssociationEntity> Associations { get; set; }
		public virtual IList<TopicEntity> Topics { get; set; }
		public virtual TopicEntity Reifier { get; set; }
	}
}