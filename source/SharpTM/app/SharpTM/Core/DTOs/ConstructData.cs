// <copyright file="ConstructData.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	/// <summary>
	/// </summary>
	public abstract class ConstructData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConstructData"/> class.
		/// </summary>
		protected internal ConstructData()
		{
			Id = Guid.NewGuid().ToString();
			ItemIdentifiers = new ReadOnlyCollectionWithLimitedAccess<ILocator>();
		}

		/// <summary>
		/// Gets or sets Id.
		/// </summary>
		/// <value>
		/// The id.
		/// </value>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets ItemIdentifiers.
		/// </summary>
		/// <value>
		/// The item identifiers.
		/// </value>
		public ReadOnlyCollectionWithLimitedAccess<ILocator> ItemIdentifiers { get; set; }
	}
}