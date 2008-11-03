using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	///     Indicates that a <see cref="TMAPI.Net.Core.IConstruct"/> is reifiable.
	///     Every Topic Maps construct that is not a <see cref="T:TMAPI.Net.Core.ITopic"/> is reifiable.
	/// </summary>
	public abstract class Reifiable : Construct, IReifiable
	{
		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Reifiable"/> class.
		/// </summary>
		/// <param name="parent">The parent <see cref="IConstruct"/> containing this instance.</param>
		/// <param name="topicMap">The <see cref="ITopicMap"/> containing this instance.</param>
		/// <param name="initialIdentifier">The initial <see cref="ILocator"/> for this instance.</param>
		protected Reifiable(IConstruct parent, ITopicMap topicMap, ILocator initialIdentifier)
			: base(parent, topicMap, initialIdentifier)
		{
		}
		#endregion

		#region IReifiable properties
		/// <summary>
		///     Gets or sets the reifier of this construct.
		/// </summary>
		/// <remarks>
		///     <list type="bullet">
		///         <item>If this construct is not reified <c>null</c> is returned.</item>
		///         <item>If the reifier is set to <c>null</c> an existing reifier should be removed.</item>
		///         <item>The reifier of this construct MUST NOT reify another information item.</item>
		///     </list>
		/// </remarks>
		public ITopic Reifier
		{
			get;
			set;
		}
		#endregion
	}
}