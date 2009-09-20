// <copyright file="OccurrenceFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	public class OccurrenceFromDTO : ConstructFromDTO<OccurrenceDTO, IOccurrence>
	{
		static readonly OccurrenceFromDTO mapper = new OccurrenceFromDTO();

		OccurrenceFromDTO()
		{
		}

		public static IOccurrence Create(ITopic parent, OccurrenceDTO source)
		{
			IOccurrence target = parent.CreateOccurrence(
				TypeFromDTO.FindOrCreate(parent.TopicMap, source.Type),
				string.Empty);

			mapper.Map(source, target);
			DatatypeAwareFromDTO.Instance.Map(source, target);
			ReifiableFromDTO.Instance.Map(source, target);
			ScopeFromDTO.Instance.Map(source, target);

			return target;
		}
	}
}