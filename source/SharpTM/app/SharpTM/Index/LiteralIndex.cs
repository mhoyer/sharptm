// <copyright file="LiteralIndex.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Pixelplastic.TopicMaps.SharpTM.Core;
using TMAPI.Net.Core;
using TMAPI.Net.Index;

namespace Pixelplastic.TopicMaps.SharpTM.Index
{
	/// <summary>
	/// Implements the <see cref="ILiteralIndex"/> interface.
	/// </summary>
	public class LiteralIndex : Index, ILiteralIndex
	{
		/// <summary>
		/// Represents the current names in the index.
		/// </summary>
		readonly List<IName> names;

		/// <summary>
		/// Represents the current occurrences in the index.
		/// </summary>
		readonly List<IOccurrence> occurrences;

		/// <summary>
		/// Represents the current variants in the index.
		/// </summary>
		readonly List<IVariant> variants;

		/// <summary>
		/// Initializes a new instance of the <see cref="LiteralIndex"/> class.
		/// </summary>
		/// <param name="topicMapSystem">The topic map system this index is based on.</param>
		/// <param name="enableAutoUpdate">if set to <c>true</c> auto update will be enabled.</param>
		public LiteralIndex(ITopicMapSystem topicMapSystem, bool enableAutoUpdate)
			: base(topicMapSystem, enableAutoUpdate)
		{
			if (topicMapSystem == null)
			{
				throw new ArgumentNullException("topicMapSystem");
			}

			names = new List<IName>();
			occurrences = new List<IOccurrence>();
			variants = new List<IVariant>();
		}

		#region ILiteralIndex methods
		/// <summary>
		///     Synchronize the index with data in the topic map.
		/// </summary>
		public override void Reindex()
		{
			Trace.WriteLine("Refreshing the index for {0}" + GetType().Name);

			names.Clear();
			occurrences.Clear();
			variants.Clear();

			foreach (ILocator locator in TopicMapSystem.Locators)
			{
				ReindexTopicMap(TopicMapSystem.GetTopicMap(locator));
			}
		}

		/// <summary>
		///     Retrieves the <see cref="T:TMAPI.Net.Core.IName"/>s in the topic map 
		///     which have a value equal to <paramref name="value"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="value">
		///     The value of the <see cref="T:TMAPI.Net.Core.IName"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IName"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IName> GetNames(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			List<IName> foundNames = new List<IName>();

			for (int i = 0; i < names.Count; i++)
			{
				if (names[i].Value == value)
				{
					foundNames.Add(names[i]);
				}
			}

			return foundNames.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map 
		///     whose value property matches <paramref name="value"/> and whose datatype 
		///     property is <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="value">
		///     The value of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IOccurrence> GetOccurrences(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			List<IOccurrence> foundOccurrences = new List<IOccurrence>();

			for (int i = 0; i < occurrences.Count; i++)
			{
				if (occurrences[i].Value == value &&
				    occurrences[i].Datatype.Reference == Datatypes.STRING)
				{
					foundOccurrences.Add(occurrences[i]);
				}
			}

			return foundOccurrences.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map 
		///     whose value property matches the IRI represented by <paramref name="value"/>. 
		///     Those <see cref="T:TMAPI.Net.Core.IOccurrence"/>s which have a datatype equal to 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a> and their 
		///     value property is equal to <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> are returned. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="value">
		///     The value of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IOccurrence> GetOccurrences(ILocator value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			List<IOccurrence> foundOccurrences = new List<IOccurrence>();

			for (int i = 0; i < occurrences.Count; i++)
			{
				if (occurrences[i].LocatorValue == value)
				{
					foundOccurrences.Add(occurrences[i]);
				}
			}

			return foundOccurrences.AsReadOnly();
		}

		/// <summary>
		/// Returns the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s in the topic map
		/// whose value property matches <paramref name="value"/> and whose <c>datatype</c> is <paramref name="datatype"/>.
		/// The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="value">The value of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.</param>
		/// <param name="datatype">The <c>datatype</c> of the <see cref="T:TMAPI.Net.Core.IOccurrence"/>s to be returned.</param>
		/// <returns>
		/// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IOccurrence"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		/// If the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IOccurrence> GetOccurrences(string value, ILocator datatype)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (datatype == null)
			{
				throw new ArgumentNullException("datatype");
			}

			List<IOccurrence> foundOccurrences = new List<IOccurrence>();

			for (int i = 0; i < occurrences.Count; i++)
			{
				if (occurrences[i].Datatype.Equals(datatype) &&
				    occurrences[i].Value == value)
				{
					foundOccurrences.Add(occurrences[i]);
				}
			}

			return foundOccurrences.AsReadOnly();
		}

		/// <summary>
		/// Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map whose
		/// value property matches <paramref name="value"/> and whose <c>datatype</c> property is
		/// <a href="http://www.w3.org/TR/xmlschema-2/#string"><c>xsd:string</c></a>.
		/// The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="value">The value of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.</param>
		/// <returns>
		/// An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		/// If the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IVariant> GetVariants(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			List<IVariant> foundVariants = new List<IVariant>();

			for (int i = 0; i < variants.Count; i++)
			{
				if (variants[i].Value == value &&
				    variants[i].Datatype.Reference == Datatypes.STRING)
				{
					foundVariants.Add(variants[i]);
				}
			}

			return foundVariants.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map whose 
		///     value property matches the IRI represented by <paramref name="value"/>. 
		///     Those <see cref="T:TMAPI.Net.Core.IVariant"/>s which have a datatype equal to 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a> and their 
		///     value property is equal to <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> are returned. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="value">
		///     The value of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IVariant> GetVariants(ILocator value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			List<IVariant> foundVariants = new List<IVariant>();

			for (int i = 0; i < variants.Count; i++)
			{
				if (variants[i].LocatorValue == value)
				{
					foundVariants.Add(variants[i]);
				}
			}

			return foundVariants.AsReadOnly();
		}

		/// <summary>
		///     Returns the <see cref="T:TMAPI.Net.Core.IVariant"/>s in the topic map 
		///     whose value property matches <paramref name="value"/> and whose datatye is <paramref name="datatype"/>. 
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="value">
		///     The value of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
		/// </param>
		/// <param name="datatype">
		///     The datatype of the <see cref="T:TMAPI.Net.Core.IVariant"/>s to be returned.
		/// </param>
		/// <returns>
		///     An unmodifiable collection of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IVariant> GetVariants(string value, ILocator datatype)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (datatype == null)
			{
				throw new ArgumentNullException("datatype");
			}

			List<IVariant> foundVariants = new List<IVariant>();

			for (int i = 0; i < variants.Count; i++)
			{
				if (variants[i].Value == value &&
				    variants[i].Datatype.Equals(datatype))
				{
					foundVariants.Add(variants[i]);
				}
			}

			return foundVariants.AsReadOnly();
		}
		#endregion

		/// <summary>
		/// Synchronize a single topic map.
		/// </summary>
		/// <param name="topicMap">The topic map.</param>
		void ReindexTopicMap(ITopicMap topicMap)
		{
			foreach (ITopic topic in topicMap.Topics)
			{
				names.AddRange(topic.Names);
				occurrences.AddRange(topic.Occurrences);

				foreach (IName name in topic.Names)
				{
					variants.AddRange(name.Variants);
				}
			}
		}
	}
}