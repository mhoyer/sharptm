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
		public virtual List<string> SubjectIdentifiers { get; set; }
		public virtual List<string> SubjectLocators { get; set; }
		public virtual List<NameEntity> Names { get; set; }
		public virtual List<OccurrenceEntity> Occurrences { get; set; }

		public TopicEntity()
		{
			SubjectIdentifiers = new List<string>();
			SubjectLocators = new List<string>();
			Names = new List<NameEntity>();
			Occurrences = new List<OccurrenceEntity>();
		}
	}
}