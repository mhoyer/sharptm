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

namespace TMAPI.Net.UnitTests
{
	/// <summary>
	/// Base super class for all tests.
	/// </summary>
	/// <remarks>
	/// It will initialize a new <see cref="Net.Core.TopicMapSystemFactory"/> and a <see cref="ITopicMapSystem"/>.
	/// </remarks>
	public class TMAPITestCase
	{
		#region Fields
		/// <summary>
		/// Represents the current <see cref="ITopicMapSystem"/> for all tests.
		/// </summary>
		protected readonly ITopicMapSystem TopicMapSystem;

		/// <summary>
		/// Represents the current <see cref="Net.Core.TopicMapSystemFactory"/> instance for all tests.
		/// </summary>
		protected TopicMapSystemFactory TopicMapSystemFactory;

		/// <summary>
		/// Feature indicating that type-instance relations are modelled as associations.
		/// </summary>
		protected const string FeatureTypeInstanceAssociations = "http://tmapi.org/features/type-instance-associations";
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="TMAPITestCase"/> class.
		/// </summary>
		public TMAPITestCase()
		{
			TopicMapSystemFactory = NewTopicMapSystemFactoryInstance();
			TopicMapSystem = TopicMapSystemFactory.NewTopicMapSystem();
			TopicMap = TopicMapSystem.CreateTopicMap("http://sharptm.de/testmap");
		}
		#endregion

		#region properties
		/// <summary>
		/// Gets the topic map.
		/// </summary>
		protected ITopicMap TopicMap { get; private set; }
		#endregion

		#region methods
		/// <summary>
		/// Returns a new instance of <see cref="Net.Core.TopicMapSystemFactory"/>.
		/// </summary>
		/// <remarks>
		/// Tries to find an implementation (subclass) of <see cref="Net.Core.TopicMapSystemFactory"/> 
		/// and will invoke the <see cref="Net.Core.TopicMapSystemFactory.NewInstance"/> method.
		/// </remarks>
		/// <returns>A new instance of <see cref="Net.Core.TopicMapSystemFactory"/>.</returns>
		public static TopicMapSystemFactory NewTopicMapSystemFactoryInstance()
		{
			SimpleServiceLocator ssl = new SimpleServiceLocator();
			ssl.Register<ITopicMapRepository>(new TopicMapRepository());
			ServiceLocator.SetLocatorProvider(() => ssl);

			return TopicMapSystemFactory.NewInstance<SharpTMSystemFactory>();
		}

		/// <summary>
		/// Removes all topic maps.
		/// </summary>
		public void Dispose()
		{
			foreach (var locator in TopicMapSystem.Locators)
			{
				var topicMap = TopicMapSystem.GetTopicMap(locator);

				topicMap.Remove();
			}
		}

		/// <summary>
		/// Creates a topic with a random item identifier.
		/// </summary>
		/// <returns>
		/// The topic.
		/// </returns>
		protected ITopic CreateTopic()
		{
			return TopicMap.CreateTopic();
		}

		/// <summary>
		/// Creates an association with a random type and no roles.
		/// </summary>
		/// <returns>
		/// The association.
		/// </returns>
		protected IAssociation CreateAssociation()
		{
			return TopicMap.CreateAssociation(CreateTopic());
		}

		/// <summary>
		/// Creates a role which is part of a random association with a random player and type.
		/// </summary>
		/// <returns>
		/// The new role.
		/// </returns>
		protected IRole CreateRole()
		{
			return CreateAssociation().CreateRole(CreateTopic(), CreateTopic());
		}

		/// <summary>
		/// Creates an occurrence which is part of a random topic with a random type.
		/// </summary>
		/// <returns>
		/// The occurrence.
		/// </returns>
		protected IOccurrence CreateOccurrence()
		{
			return CreateTopic().CreateOccurrence(CreateTopic(), "Occurrence");
		}

		/// <summary>
		/// Creates a name which is part of a newly created topic using the default type name.
		/// </summary>
		/// <returns>
		/// The new name.
		/// </returns>
		protected IName CreateName()
		{
			return CreateTopic().CreateName("Name");
		}

		/// <summary>
		/// Creates a variant which is part of a newly created name.
		/// </summary>
		/// <returns>
		/// The variant.
		/// </returns>
		protected IVariant CreateVariant()
		{
			return CreateName().CreateVariant("Variant", CreateTopic());
		}

		#endregion
	}
}