//------------------------------------------------------------------------------------------------- 
// <copyright file="TMAPITestCase.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the TMAPITestCase type.</summary>
//-------------------------------------------------------------------------------------------------
using Microsoft.Practices.ServiceLocation;
using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories.InMemory;
using TMAPI.Net.Core;

namespace TMAPI.Net.Tests
{
	/// <summary>
	/// Base super class for all tests.
	/// </summary>
	/// <remarks>
	/// It will initialize a new <see cref="TopicMapSystemFactory"/> and a <see cref="ITopicMapSystem"/>.
	/// </remarks>
	public class TMAPITestCase
	{
		#region Fields
		/// <summary>
		/// Represents the current <see cref="ITopicMapSystem"/> for all tests.
		/// </summary>
		protected readonly ITopicMapSystem topicMapSystem;

		/// <summary>
		/// Represents the current <see cref="TopicMapSystemFactory"/> instance for all tests.
		/// </summary>
		protected TopicMapSystemFactory topicMapSystemFactory;

		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="TMAPITestCase"/> class.
		/// </summary>
		public TMAPITestCase()
		{
			topicMapSystemFactory = NewTopicMapSystemFactoryInstance();
			topicMapSystem = topicMapSystemFactory.NewTopicMapSystem();
		}
		#endregion

		#region methods
		/// <summary>
		/// Returns a new instance of <see cref="TopicMapSystemFactory"/>.
		/// </summary>
		/// <remarks>
		/// Tries to find an implementation (subclass) of <see cref="TopicMapSystemFactory"/> 
		/// and will invoke the <see cref="TopicMapSystemFactory.NewInstance"/> method.
		/// </remarks>
		/// <returns>A new instance of <see cref="TopicMapSystemFactory"/>.</returns>
		public static TopicMapSystemFactory NewTopicMapSystemFactoryInstance()
		{
			SimpleServiceLocator ssl = new SimpleServiceLocator();
			ssl.Register<ITopicMapRepository>(new TopicMapRepository());
			ServiceLocator.SetLocatorProvider(() => ssl);

			return TopicMapSystemFactory.NewInstance<SharpTMSystemFactory>();
		}
		#endregion
	}
}