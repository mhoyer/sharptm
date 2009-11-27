// <copyright file="TopicMapSystem.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;
using TMAPI.Net.Core;
#if LOG4NET
using log4net;
#endif

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements a <see cref="ITopicMapSystem"/>.
	/// </summary>
	/// <remarks>
	/// Any TMAPI system must be capable of providing access to one or more 
	/// <see cref="ITopicMap"/> objects. A TMAPI system may be capable of 
	/// allowing a client to create new <see cref="ITopicMap"/> instances.
	/// </remarks>
	public class TopicMapSystem : ITopicMapSystem
	{
#if LOG4NET
		static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
#endif
		readonly Dictionary<string, bool> enabledFeatures;
		readonly Dictionary<ILocator, ITopicMap> _locators;
		readonly TopicMapMediator _topicMapMediator;

		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMapSystem"/> class.
		/// </summary>
		/// <param name="features">The list of enabled/disabled features.</param>
		internal TopicMapSystem(ITopicMapRepository topicMapRepository, Dictionary<string, bool> features)
		{
            if (topicMapRepository == null) throw new ArgumentNullException("topicMapRepository");

			Repository = topicMapRepository;
			_topicMapMediator = new TopicMapMediator(topicMapRepository, this);
			_locators = new Dictionary<ILocator, ITopicMap>();
			enabledFeatures = features ?? new Dictionary<string, bool>();
		}

		internal ITopicMapRepository Repository
		{
			get; private set;
		}

		#region ITopicMapSystem properties
		/// <summary>
		///     Gets all storage addresses of <see cref="T:TMAPI.Net.Core.ITopicMap"/> instances 
		///     known by this system.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ILocator"/>s which represent IRIs of known 
		///     <see cref="T:TMAPI.Net.Core.ITopicMap"/> instances.
		/// </returns>
		public ReadOnlyCollection<ILocator> Locators
		{
			get
			{
				return new ReadOnlyCollection<ILocator>(new List<ILocator>(_locators.Keys));
			}
		}
		#endregion

		#region ITopicMapSystem methods
		/// <summary>
		///     Applications SHOULD call this method when the TopicMapSystem instance is no longer required.
		///     Once the TopicMapSystem instance is closed, the TopicMapSystem and any object retrieved from 
		///     or created in this TopicMapSystem MUST NOT be used by the application.
		///     An implementation of the TopicMapSystem interface may use this method to clean up any 
		///     resources used by the implementation.
		/// </summary>
		public void Close()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		///     Returns a <see cref="T:TMAPI.Net.Core.ILocator"/> instance representing the specified IRI 
		///     <paramref name="reference"/>. The specified IRI <paramref name="reference"/> is assumed to be absolute.
		/// </summary>
		/// <param name="reference">
		///     A string which uses the IRI notation.
		/// </param>
		/// <returns>
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> representing the IRI <paramref name="reference"/>.
		/// </returns>
		public ILocator CreateLocator(string reference)
		{
			return new Locator(reference);
		}

		/// <summary>
		/// Creates a new <see cref="T:TMAPI.Net.Core.ITopicMap"/> and stores it within the system under the
		/// specified <paramref name="storageAddress"/>.
		/// </summary>
		/// <param name="storageAddress">The address which should be used to store the <see cref="T:TMAPI.Net.Core.ITopicMap"/>.
		/// <seealso href="http://www.ietf.org/rfc/rfc3987.txt">RFC: Internationalized Resource Identifiers (IRIs)</seealso></param>
		/// <returns>
		/// The newly created <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance.
		/// </returns>
		/// <exception cref="TopicMapExistsException">
		/// If this <see cref="ITopicMapSystem"/> already manages a <see cref="ITopicMap"/> under the specified IRI.
		/// </exception>
		public ITopicMap CreateTopicMap(ILocator storageAddress)
		{
#if LOG4NET
			log.InfoFormat("Creating a TopicMap by locator '{0}'.", iri);
#endif
			if (storageAddress == null)
				throw new ArgumentException("At least one locator required for a TopicMap.");

			if (_topicMapMediator.Exists(entity => entity.BaseLocator == storageAddress.Reference))
			{
				string message = string.Format(
					"A topic map with locator {0} still exists in this topic map system.",
					storageAddress);

				throw new TopicMapExistsException(message);
			}

			TopicMapEntity topicMapEntity = new TopicMapEntity();
			TopicMap topicMap = _topicMapMediator.Create(topicMapEntity, storageAddress);
			_locators.Add(storageAddress, topicMap);

			topicMap.OnRemove += TopicMap_OnRemove;

			return topicMap;
		}

		/// <summary>
		///     Creates a new <see cref="T:TMAPI.Net.Core.ITopicMap"/> and stores it within the system under the 
		///     specified <paramref name="iri"/>. The string is assumed to be in IRI notation.
		/// </summary>
		/// <param name="iri">
		///     The address which should be used to store the <see cref="T:TMAPI.Net.Core.ITopicMap"/>.
		///		<seealso href="http://www.ietf.org/rfc/rfc3987.txt">RFC: Internationalized Resource Identifiers (IRIs)</seealso>
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance.
		/// </returns>
		public ITopicMap CreateTopicMap(string iri)
		{
			return CreateTopicMap(CreateLocator(iri));
		}

		/// <summary>
		///     Returns a property in the underlying implementation of <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>. 
		///     A list of the core properties defined by TMAPI can be found at 
		///     <a href="http://tmapi.org/properties/">http://tmapi.org/properties/</a>. An implementation is free to 
		///     support properties other than the core ones.
		///     The properties supported by the <see cref="TopicMapSystem"/> and the value for each property is set when the 
		///     <see cref="TopicMapSystem" /> is created by a call to <see cref="TMAPI.Net.Core.TopicMapSystemFactory.NewTopicMapSystem"/> and 
		///     cannot be modified subsequently.
		/// </summary>
		/// <param name="propertyName">
		///     The name of the property to retrieve.
		/// </param>
		/// <returns>
		///     The value set for the property or <c>null</c> if no value is set for the specified <paramref name="propertyName"/>.
		/// </returns>
		public object GetProperty(string propertyName)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		///     Retrieves a <see cref="T:TMAPI.Net.Core.ITopicMap"/> managed by this system with the specified 
		///     storage address <paramref name="iri"/>. The string is assumed to be in IRI notation.
		/// </summary>
		/// <param name="iri">
		///     The storage address to retrieve the <see cref="T:TMAPI.Net.Core.ITopicMap"/> from.
		///		<seealso href="http://www.ietf.org/rfc/rfc3987.txt">RFC: Internationalized Resource Identifiers (IRIs)</seealso>
		/// </param>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance managed by this system which is stored at the 
		///     specified <paramref name="iri"/>, or <c>null</c> if no such <see cref="T:TMAPI.Net.Core.ITopicMap"/> is found.
		/// </returns>
		public ITopicMap GetTopicMap(string iri)
		{
			if (String.IsNullOrEmpty(iri)) return null;

			return _topicMapMediator.Find(entity => entity.BaseLocator == iri);
		}

		/// <summary>
		///     Retrieves a <see cref="T:TMAPI.Net.Core.ITopicMap"/> managed by this system with the specified 
		///     storage address <paramref name="iri"/>.
		/// </summary>
		/// <param name="iri">
		///     The storage address to retrieve the <see cref="T:TMAPI.Net.Core.ITopicMap"/> from.
		///		<seealso href="http://www.ietf.org/rfc/rfc3987.txt">RFC: Internationalized Resource Identifiers (IRIs)</seealso>
		/// </param>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.ITopicMap"/> instance managed by this system which is stored at the 
		///     specified <paramref name="iri"/>, or <c>null</c> if no such <see cref="T:TMAPI.Net.Core.ITopicMap"/> is found.
		/// </returns>
		public ITopicMap GetTopicMap(ILocator iri)
		{
			if (iri == null) return null;

		    return GetTopicMap(iri.Reference);
		}

		/// <summary>
		/// Returns the value of the feature specified by <paramref name="featureName"/> for this 
		/// <see cref="TopicMapSystem"/> instance.
		/// The features supported by the <see cref="TopicMapSystem"/> and the value for each feature is set when the 
		/// <see cref="TopicMapSystem"/> is created by a call to <see cref="TMAPI.Net.Core.TopicMapSystemFactory.NewTopicMapSystem"/> and 
		/// cannot be modified subsequently.
		/// </summary>
		/// <param name="featureName">
		///     The name of the feature to check.
		/// </param>
		/// <returns>
		///     <c>true</c> if the named feature is enabled this <see cref="TopicMapSystem"/> instance; 
		///     <c>false</c> if the named feature is disabled for this instance.
		/// </returns>
		public bool GetFeature(string featureName)
		{
			string feature = Features.MapToName(featureName);
			return enabledFeatures.ContainsKey(feature) && enabledFeatures[feature];
		}
		#endregion

		/// <summary>
		/// Removes a topic map identified by a <see cref="ILocator"/>.
		/// </summary>
		/// <param name="locator">The locator of the topic map to be removed.</param>
		internal void RemoveTopicMap(ILocator locator)
		{
			if (locator == null) return;
            RemoveTopicMap(GetTopicMap(locator) as TopicMap);
		}

		internal void RemoveTopicMap(TopicMap topicMap)
		{
			if (topicMap == null) return;
			_locators.Remove(topicMap.BaseLocator);
			_topicMapMediator.Delete(topicMap);
		}

		/// <summary>
		/// Handles the <see cref="TopicMap.OnRemove"/> event of a <see cref="TopicMap"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void TopicMap_OnRemove(object sender, EventArgs e)
		{
			RemoveTopicMap(sender as TopicMap);
		}
	}
}