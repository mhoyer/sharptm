// <copyright file="SharpTMSystemFactory.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="TMAPI.Net.Core.TopicMapSystemFactory"/> for <see cref="SharpTM"/>.
	/// </summary>
	public class SharpTMSystemFactory : TopicMapSystemFactory
	{
		readonly Dictionary<string, bool> enabledFeatures;

		public SharpTMSystemFactory()
		{
			enabledFeatures = new Dictionary<string, bool>();

			ReadFeatureSettings(Features.AutomaticMerging);
			ReadFeatureSettings(Features.LocatorAddressNotationFeatures);
			ReadFeatureSettings(Features.MergingSupportFeatures);
			ReadFeatureSettings(Features.ReadOnlySystem);
			ReadFeatureSettings(Features.TopicMapsModelFeatures);
		}

		/// <summary>
		///     Returns the particular feature requested for in the underlying implementation of 
		///     <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
		/// </summary>
		/// <param name="featureUri">
		///     The URI of the feature.
		/// </param>
		/// <returns>
		///     <c>true</c> if the named feature is enabled for <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> 
		///     instances created by this factory; <c>false</c> if the named feature is disabled for 
		///     <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> instances created by this factory. 
		/// </returns>
		/// <exception cref="FeatureNotRecognizedException">
		///     If the underlying implementation does not recognize the named feature.
		/// </exception>
		public override bool GetFeature(string featureUri)
		{
			string featureName = Features.MapToName(featureUri);

			return enabledFeatures.ContainsKey(featureName) && enabledFeatures[featureName];
		}

		/// <summary>
		///     Gets the value of a property in the underlying implementation of <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
		///     A list of the core properties defined by TMAPI can be found at http://tmapi.org/properties/.
		///     An implementation is free to support properties other than the core ones. 
		/// </summary>
		/// <param name="propertyName">
		///     The name of the property to retrieve.
		/// </param>
		/// <returns>
		///     The value set for this property or <c>null</c> if no value is currently set for the property.
		/// </returns>
		public override object GetProperty(string propertyName)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		///     Returns if the particular feature is supported by the <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
		///     Opposite to <see cref="TMAPI.Net.Core.TopicMapSystemFactory.GetFeature"/> this method returns if 
		///     the requested feature is generally available / supported by the underlying <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> 
		///     and does not return the state (enabled/disabled) of the feature. 
		/// </summary>
		/// <param name="featureUri">
		///     The URI of the feature.
		/// </param>
		/// <returns>
		///     <c>true</c> if the requested feature is supported, otherwise <c>false</c>.
		/// </returns>
		public override bool HasFeature(string featureUri)
		{
			if (featureUri == null)
			{
				throw new ArgumentNullException("featureUri");
			}

			if (Features.AutomaticMerging.StartsWith(featureUri) ||
			    Features.MergingSupportFeatures.StartsWith(featureUri) ||
			    Features.LocatorAddressNotationFeatures.StartsWith(featureUri))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		///     Obtain a new instance of a <see cref="TMAPI.Net.Core.TopicMapSystemFactory"/>.
		/// </summary>
		/// <returns>
		///     A new instance of <see cref="TMAPI.Net.Core.TopicMapSystemFactory"/>.
		/// </returns>
		/// <exception cref="FactoryConfigurationException">
		///     If instance cannot be instantiated.
		/// </exception>
		public override TopicMapSystemFactory NewInstance()
		{
			return new SharpTMSystemFactory();
		}

		/// <summary>
		///     Creates a new <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> instance using the currently 
		///     configured factory parameters.
		/// </summary>
		/// <returns>
		///     A new instance of a <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
		/// </returns>
		/// <exception cref="TMAPIException">
		///     If a <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> cannot be created which satisfies the requested configuration.
		/// </exception>
		public override ITopicMapSystem NewTopicMapSystem()
		{
			var topicMapRepository = ServiceLocator.Current.GetInstance<ITopicMapRepository>();
		    return NewTopicMapSystem(topicMapRepository);
		}

        public ITopicMapSystem NewTopicMapSystem(ITopicMapRepository topicMapRepository)
        {
            if (topicMapRepository == null) throw new ArgumentNullException("topicMapRepository");
				
            return new TopicMapSystem(topicMapRepository, enabledFeatures);
        }

		/// <summary>
		///     Sets a particular feature in the underlying implementation of <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
		///     A list of the core features can be found at http://tmapi.org/features/. 
		/// </summary>
		/// <param name="featureUri">
		///     The name of the feature to be set.
		/// </param>
		/// <param name="enable">
		///     <c>true</c> to enable the feature, <c>false</c> to disable it.
		/// </param>
		/// <exception cref="FeatureNotRecognizedException">
		///     If the underlying implementation does not recognize the named feature.
		/// </exception>
		/// <exception cref="FeatureNotSupportedException">
		///     If the underlying implementation recognizes the named feature but does not support enabling or 
		///     disabling it (as specified by <paramref name="enable"/>).
		/// </exception>
		public override void SetFeature(string featureUri, bool enable)
		{
			string featureName = Features.MapToName(featureUri);

			if (!HasFeature(featureUri))
			{
				throw new FeatureNotSupportedException(String.Format("Feature {0} is not supported.", featureName));
			}

			if (!enabledFeatures.ContainsKey(featureName))
			{
				enabledFeatures.Add(featureName, false);
			}

			enabledFeatures[featureName] = enable;
		}

		/// <summary>
		///     Sets a property in the underlying implementation of <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
		///     A list of the core properties defined by TMAPI can be found at http://tmapi.org/properties/.
		///     An implementation is free to support properties other than the core ones. 
		/// </summary>
		/// <param name="propertyName">
		///     The name of the property to be set.
		/// </param>
		/// <param name="value">
		///     The value to be set of this property or <c>null</c> to remove the property from the current factory configuration.
		/// </param>
		public override void SetProperty(string propertyName, object value)
		{
			throw new System.NotImplementedException();
		}

		void ReadFeatureSettings(string featureUri)
		{
			string featureName = Features.MapToName(featureUri);
#if SILVERLIGHT
			enabledFeatures.Add(featureName, featureUri != Features.ReadOnlySystem);
#else
			enabledFeatures.Add(
				featureName,
				(bool)Properties.Settings.Default[featureName]);
#endif
		}
	}
}