// <copyright file="TopicDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	/// <summary>
	/// Simple data storage for <see cref="ITopic"/> items.
	/// </summary>
	public class TopicDTO : ConstructDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TopicDTO"/> class.
		/// </summary>
		public TopicDTO()
		{
			Names = new ReadOnlyCollectionWithLimitedAccess<IName>();
			Occurrences = new ReadOnlyCollectionWithLimitedAccess<IOccurrence>();
			RolesPlayed = new ReadOnlyCollectionWithLimitedAccess<IRole>();
			SubjectIdentifiers = new ReadOnlyCollectionWithLimitedAccess<ILocator>();
			SubjectLocators = new ReadOnlyCollectionWithLimitedAccess<ILocator>();
			Types = new ReadOnlyCollectionWithLimitedAccess<ITopic>();
		}

		/// <summary>
		/// Gets or sets Names.
		/// </summary>
		/// <value>
		/// The names.
		/// </value>
		public ReadOnlyCollectionWithLimitedAccess<IName> Names { get; set; }

		/// <summary>
		/// Gets or sets Occurrences.
		/// </summary>
		/// <value>
		/// The occurrences.
		/// </value>
		public ReadOnlyCollectionWithLimitedAccess<IOccurrence> Occurrences { get; set; }

		/// <summary>
		/// Gets or sets Reified.
		/// </summary>
		/// <value>
		/// The reified.
		/// </value>
		public IReifiable Reified { get; set; }

		/// <summary>
		/// Gets or sets RolesPlayed.
		/// </summary>
		/// <value>
		/// The roles played.
		/// </value>
		public ReadOnlyCollectionWithLimitedAccess<IRole> RolesPlayed { get; set; }

		/// <summary>
		/// Gets or sets SubjectIdentifiers.
		/// </summary>
		/// <value>
		/// The subject identifiers.
		/// </value>
		public ReadOnlyCollectionWithLimitedAccess<ILocator> SubjectIdentifiers { get; set; }

		/// <summary>
		/// Gets or sets SubjectLocators.
		/// </summary>
		/// <value>
		/// The subject locators.
		/// </value>
		public ReadOnlyCollectionWithLimitedAccess<ILocator> SubjectLocators { get; set; }

		/// <summary>
		/// Gets or sets Types.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		public ReadOnlyCollectionWithLimitedAccess<ITopic> Types { get; set; }
	}
}