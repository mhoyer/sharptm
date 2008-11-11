using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IOccurrence"/> interface.
	/// </summary>
	public class Occurrence : DatatypeAware, IOccurrence
	{
		#region readonly & static fields
		/// <summary>
		/// Represents the current instance of <see cref="Typed"/> construct helper.
		/// </summary>
		private readonly Typed typed;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Occurrence"/> class.
		/// </summary>
		/// <param name="parent">The parent of this instance.</param>
		/// <param name="type">The type of this instance.</param>
		internal Occurrence(ITopic parent, ITopic type)
			: base(parent, parent.TopicMap)
		{
			if (type == null)
			{
				throw new ModelConstraintException(
					"An occurrence type MUST NOT be null.",
					new ArgumentNullException("type"));
			}

			typed = new Typed(type);
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
		public ITopic Type
		{
			get
			{
				return typed.Type;
			}
			set
			{
				typed.Type = value;
			}
		}
		#endregion
	}
}