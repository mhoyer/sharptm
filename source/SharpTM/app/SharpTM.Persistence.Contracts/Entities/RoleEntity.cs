// <copyright file="RoleEntity.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities
{
	public class RoleEntity : ConstructEntity
	{
		public virtual TopicEntity Reifier { get; set; }
		public virtual TopicEntity Player { get; set; }
		public virtual TopicEntity Type { get; set; }
	}
}