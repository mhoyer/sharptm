// <copyright file="Merge.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	public class Merge
	{
		public static AssociationToBeMerged Association(IAssociation association)
		{
			return new AssociationToBeMerged(association);
		}

		/// <summary>
		/// Specifies the <see cref="ITopic"/> instance that should be merged.
		/// </summary>
		/// <param name="construct">The <see cref="IConstruct"/> instance to be merged.</param>
		/// <returns>A wrapper for the merge source definition.</returns>
		public static ConstructToBeMerged Construct(IConstruct construct)
		{
			return new ConstructToBeMerged(construct);
		}

		/// <summary>
		/// Specifies the <see cref="IName"/> instance that should be merged.
		/// </summary>
		/// <param name="name">The <see cref="IName"/> instance to be merged.</param>
		/// <returns>A wrapper for the merge source definition.</returns>
		public static NameToBeMerged Name(IName name)
		{
			return new NameToBeMerged(name);
		}

		public static OccurrenceToBeMerged Occurrence(IOccurrence occurrence)
		{
			return new OccurrenceToBeMerged(occurrence);
		}

		public static ReifiableToBeMerged Reifiable(IReifiable reifiable)
		{
			return new ReifiableToBeMerged(reifiable);
		}

		public static RoleToBeMerged Role(IRole role)
		{
			return new RoleToBeMerged(role);
		}

		public static ScopedToBeMerged Scoped(IScoped scoped)
		{
			return new ScopedToBeMerged(scoped);
		}

		/// <summary>
		/// Specifies the <see cref="ITopic"/> instance that should be merged.
		/// </summary>
		/// <param name="topic">The <see cref="ITopic"/> instance to be merged.</param>
		/// <returns>A wrapper for the merge source definition.</returns>
		public static TopicToBeMerged Topic(ITopic topic)
		{
			return new TopicToBeMerged(topic);
		}

		public static TopicMapToBeMerged TopicMap(ITopicMap topicMap)
		{
			return new TopicMapToBeMerged(topicMap);
		}

		public static TypedToBeMerged Typed(ITyped typed)
		{
			return new TypedToBeMerged(typed);
		}

		public static VariantToBeMerged Variant(IVariant variant)
		{
			return new VariantToBeMerged(variant);
		}
	}
}