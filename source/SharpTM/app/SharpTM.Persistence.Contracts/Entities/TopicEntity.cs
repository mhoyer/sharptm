// <copyright file="TopicEntity.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities
{
	public class TopicEntity : ConstructEntity
	{
		public virtual IList<string> SubjectIdentifiers { get; set; }
		public virtual IList<string> SubjectLocators { get; set; }
		public virtual IList<NameEntity> Names { get; set; }
		public virtual IList<OccurrenceEntity> Occurrences { get; set; }
	}
}