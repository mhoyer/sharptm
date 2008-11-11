using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements a helper class for <see cref="IReifiable"/> constructs.
	/// </summary>
	internal class Reifiable
	{
		#region readonly & static fields
		/// <summary>
		/// Represents the parent <see cref="IReifiable"/> of this instance.
		/// </summary>
		private readonly IReifiable parent;
		#endregion

		#region fields
		/// <summary>
		/// Represents the current reifier of a <see cref="IReifiable"/> construct.
		/// </summary>
		private ITopic reifier;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Reifiable"/> helper class.
		/// </summary>
		/// <param name="parentReifiable">The parent reifiable.</param>
		internal Reifiable(IReifiable parentReifiable)
		{
			if (parentReifiable == null)
			{
				throw new ArgumentNullException("parentReifiable");
			}

			parent = parentReifiable;
		}
		#endregion

		#region properties
		/// <summary>
		/// Gets or sets the reifier of this construct.
		/// </summary>
		/// <value>The reifier.</value>
		/// <remarks>
		/// 	<list type="bullet">
		/// 		<item>If this construct is not reified <c>null</c> is returned.</item>
		/// 		<item>If the reifier is set to <c>null</c> an existing reifier should be removed.</item>
		/// 		<item>The reifier of this construct MUST NOT reify another information item.</item>
		/// 	</list>
		/// </remarks>
		/// <exception cref="ModelConstraintException">If the specified <tt>reifier</tt> reifies another construct.</exception>
		internal ITopic Reifier
		{
			get
			{
				return reifier;
			}
			set
			{
				if (reifier != null)
				{
					if (reifier is Topic)
					{
						((Topic) reifier).Reified = null;
					}
				}

				if (value != null)
				{
					if (value.Reified != null)
					{
						throw new ModelConstraintException(
							"The specified reifier reifies another construct.");
					}

					if (value is Topic)
					{
						((Topic) value).Reified = parent;
					}
				}

				reifier = value;
			}
		}
		#endregion
	}
}