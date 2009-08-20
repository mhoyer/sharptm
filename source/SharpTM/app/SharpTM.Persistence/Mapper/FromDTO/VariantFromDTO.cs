// <copyright file="VariantFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class VariantFromDTO : ConstructFromDTO<VariantDTO, IVariant>
	{
		static VariantFromDTO mapper = new VariantFromDTO();

		private VariantFromDTO()
		{
		}

		public static IVariant Create(IName parent, VariantDTO source)
		{
			if(source.Scope == null || source.Scope.TopicReferences.Count == 0)
			{
				throw new MappingException(string.Format("Unable to map the variant of name '{0}'.", parent),
				 new NotSupportedException("Missing at least one scope."));
			}

			if (source.ResourceData == null && source.ResourceReference == null)
			{
				throw new MappingException(string.Format("Unable to map the variant of name '{0}'.", parent),
				 new ArgumentException("Neither resource data or a resource locator specified."));
			}

			IVariant target;

			if (source.ResourceData != null)
			{
				target = parent.CreateVariant(
					source.ResourceData.Text,
					ScopeFromDTO.Create(parent.TopicMap, source.Scope));

				DatatypeAwareFromDTO.Instance.Map(source, target);
			}
			else
			{
				target = parent.CreateVariant(
					LocatorFromDTO.Create(parent.TopicMap, source.ResourceReference),
					ScopeFromDTO.Create(parent.TopicMap, source.Scope));
			}

			ReifiableFromDTO.Instance.Map(source, target);

			return mapper.Map(source, target);
		}
	}
}