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
		protected readonly ITopicMapSystem topicMapSystem;
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
		/// <returns>A new instance of TopicMapSystemFactory.</returns>
		public static TopicMapSystemFactory NewTopicMapSystemFactoryInstance()
		{
			return TopicMapSystemFactory.NewInstance<Pixelplastic.TopicMaps.SharpTM.Core.TopicMapSystemFactory>();
		}
		#endregion
	}
}