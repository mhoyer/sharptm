using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IRole"/> interface.
	/// </summary>
	public class Role : Construct, IRole
	{
		#region fields
		/// <summary>
		/// Represents the current topic playing this role.
		/// </summary>
		private ITopic player;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Role"/> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="topicMap">The topic map.</param>
		/// <param name="initialPlayer">The initial player playing this role.</param>
		internal Role(IAssociation parent, ITopicMap topicMap, ITopic initialPlayer)
			: base(parent, topicMap)
		{
			if (initialPlayer == null)
			{
				throw new ModelConstraintException(
					"The role player MUST NOT be null.",
					new ArgumentNullException("initialPlayer"));
			}

			Player = initialPlayer;
		}
		#endregion

		#region IRole properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.IAssociation"/> to which this role belongs.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.IAssociation"/> to which this role belongs.
		/// </returns>
		public new IAssociation Parent
		{
			get
			{
				return base.Parent as IAssociation;
			}
		}

		/// <summary>
		///     Gets or sets the <see cref="T:TMAPI.Net.Core.ITopic"/> playing this role.
		/// </summary>
		/// <exception cref="ModelConstraintException">
		///     If trying to set the role player to <c>null</c>.
		/// </exception>
		public ITopic Player
		{
			get
			{
				return player;
			}
			set
			{
				if (value == null)
				{
					throw new ModelConstraintException("The role player MUST NOT be null.");
				}

				player = value;
			}
		}

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
		public new ITopic Reifier
		{
			get
			{
				return base.Reifier;
			}
			set
			{
				base.Reifier = value;
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