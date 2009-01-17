//------------------------------------------------------------------------------------------------- 
// <copyright file="TMAPITestCase.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the TMAPITestCase type.</summary>
//-------------------------------------------------------------------------------------------------
using TMAPI.Net.Core;
using SharpTM = Pixelplastic.TopicMaps.SharpTM;

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
			return TopicMapSystemFactory.NewInstance<SharpTM.Core.TopicMapSystemFactory>();
		}
		#endregion
	}
}