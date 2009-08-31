namespace TMAPI.Net.Core
{
    using System;

    /// <summary>
    /// This factory class provides access to a <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
    /// A new <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>. instance is created by invoking 
    /// the <see cref="M:TMAPI.Net.Core.TopicMapSystemFactory.NewTopicMapSystem"/> method.
    /// Configuration properties for the new <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>. instance 
    /// can be set by calling the <see cref="SetFeature"/> and 
    /// / or <see cref="SetProperty"/> methods prior to
    /// invoking <see cref="M:TMAPI.Net.Core.TopicMapSystemFactory.NewTopicMapSystem"/>. 
    /// </summary>
    public abstract class TopicMapSystemFactory
    {
        #region Methods

        /// <summary>
        /// Obtain a new instance of a <see cref="TopicMapSystemFactory"/>.
        /// </summary>
        /// <typeparam name="TFactory">
        /// The type of the factory.
        /// </typeparam>
        /// <returns>
        /// A new instance of <see cref="TopicMapSystemFactory"/>.
        /// </returns>
        /// <exception cref="FactoryConfigurationException">
        /// If instance cannot be instantiated.
        /// </exception>
        public static TopicMapSystemFactory NewInstance<TFactory>() where TFactory : TopicMapSystemFactory, new()
        {
            try
            {
                return new TFactory();
            }
            catch (Exception ex)
            {
                throw new FactoryConfigurationException(
                        String.Format(
                                "Unable to instantiate the TopicMapSystemFactory implementation {0}.",
                                typeof(TFactory).FullName),
                        ex);
            }
        }

        /// <summary>
        /// Returns the particular feature requested for in the underlying implementation of 
        /// <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
        /// </summary>
        /// <param name="featureName">
        /// The name of the feature to check.
        /// </param>
        /// <returns>
        /// <c>true</c>if the named feature is enabled for <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> 
        /// instances created by this factory; <c>false</c> if the named feature is disabled for 
        /// <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>instances created by this factory. 
        /// </returns>
        /// <exception cref="FeatureNotRecognizedException">
        /// If the underlying implementation does not recognize the named feature.
        /// </exception>
        public abstract bool GetFeature(string featureName);

        /// <summary>
        /// Gets the value of a property in the underlying implementation of <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
        /// A list of the core properties defined by TMAPI can be found at http://tmapi.org/properties/.
        /// An implementation is free to support properties other than the core ones. 
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to retrieve.
        /// </param>
        /// <returns>
        /// The value set for this property or <c>null</c> if no value is currently set for the property.
        /// </returns>
        public abstract object GetProperty(string propertyName);

        /// <summary>
        /// Returns if the particular feature is supported by the <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
        /// Opposite to <see cref="TMAPI.Net.Core.TopicMapSystemFactory.GetFeature"/> this method returns if 
        /// the requested feature is generally available / supported by the underlying <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> 
        /// and does not return the state (enabled/disabled) of the feature. 
        /// </summary>
        /// <param name="featureName">
        /// The name of the feature to check.
        /// </param>
        /// <returns>
        /// <c>true</c>if the requested feature is supported, otherwise <c>false</c>.
        /// </returns>
        public abstract bool HasFeature(string featureName);

        /// <summary>
        /// Obtain a new instance of a <see cref="TopicMapSystemFactory"/>.
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="TopicMapSystemFactory"/>.
        /// </returns>
        /// <exception cref="FactoryConfigurationException">
        /// If instance cannot be instantiated.
        /// </exception>
        public abstract TopicMapSystemFactory NewInstance();

        /// <summary>
        /// Creates a new <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> instance using the currently 
        /// configured factory parameters.
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
        /// </returns>
        /// <exception cref="TMAPIException">
        /// If a <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/> cannot be created which satisfies the requested configuration.
        /// </exception>
        public abstract ITopicMapSystem NewTopicMapSystem();

        /// <summary>
        /// Sets a particular feature in the underlying implementation of <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
        /// A list of the core features can be found at http://tmapi.org/features/. 
        /// </summary>
        /// <param name="featureName">
        /// The name of the feature to be set.
        /// </param>
        /// <param name="enable">
        /// <c>true</c>to enable the feature, <c>false</c> to disable it.
        /// </param>
        /// <exception cref="FeatureNotRecognizedException">
        /// If the underlying implementation does not recognize the named feature.
        /// </exception>
        /// <exception cref="FeatureNotSupportedException">
        /// If the underlying implementation recognizes the named feature but does not support enabling or 
        /// disabling it (as specified by <paramref name="enable"/>).
        /// </exception>
        public abstract void SetFeature(string featureName, bool enable);

        /// <summary>
        /// Sets a property in the underlying implementation of <see cref="T:TMAPI.Net.Core.ITopicMapSystem"/>.
        /// A list of the core properties defined by TMAPI can be found at http://tmapi.org/properties/.
        /// An implementation is free to support properties other than the core ones. 
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to be set.
        /// </param>
        /// <param name="value">
        /// The value to be set of this property or <c>null</c> to remove the property from the current factory configuration.
        /// </param>
        public abstract void SetProperty(string propertyName, object value);

        #endregion
    }
}