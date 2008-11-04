using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IOccurrence"/> interface.
	/// </summary>
	public class Occurrence : DatatypeAware, IOccurrence
	{
		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Occurrence"/> class.
		/// </summary>
		/// <param name="parent">The parent of this instance.</param>
		internal Occurrence(ITopic parent)
			: base(parent, parent.TopicMap)
		{
		}
		#endregion

		#region IOccurrence properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/> to which this occurrence belongs.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> to which this occurrence belongs.
		/// </returns>
		public new ITopic Parent
		{
			get
			{
				return base.Parent as ITopic;
			}
		}

		/// <summary>
		///     Gets or sets the type of this construct.
		/// </summary>
		/// <exception cref="ModelConstraintException">
		///     If the type is <c>null</c>.
		/// </exception>
		/// <remarks>
		///     Any previous type is overridden.
		/// </remarks>
		public new ITopic Type
		{
			get
			{
				return base.Type;
			}
			set
			{
				base.Type = value;
			}
		}
		#endregion
	}
}