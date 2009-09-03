// <copyright file="Role.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IRole"/> interface.
	/// </summary>
	public class Role : Construct, IRole
	{
		/// <summary>
		/// Represents the type of that construct.
		/// </summary>
		ITopic _type;

		/// <summary>
		/// Represents the current topic playing this role.
		/// </summary>
		ITopic player;

		/// <summary>
		/// Represents the topic that reifies this role.
		/// </summary>
		internal Topic reifier;

		/// <summary>
		/// Initializes a new instance of the <see cref="Role"/> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="topicMap">The topic map.</param>
		/// <param name="initialPlayer">The initial player playing this role.</param>
		/// <param name="roleType">Type of the role. MUST NOT be null.</param>
		internal Role(IAssociation parent, ITopicMap topicMap, ITopic initialPlayer, ITopic roleType)
			: base(parent, topicMap)
		{
			if (initialPlayer == null)
			{
				throw new ModelConstraintException(
					"The role player MUST NOT be null.",
					new ArgumentNullException("initialPlayer"));
			}

			if (roleType == null)
			{
				throw new ModelConstraintException(
					"A role MUST NOT be untyped.",
					new ArgumentNullException("roleType"));
			}

			Type = roleType;
			Player = initialPlayer;
		}

		/// <summary>
		/// Occurs when the <see cref="Player"/> property changes.
		/// </summary>
		public event EventHandler<RolePlayerChangedEventArgs> OnRolePlayerChanges;

		/// <summary>
		/// Occurs when the <see cref="Type"/> property changes.
		/// </summary>
		public event EventHandler<RoleTypeChangesEventArgs> OnRoleTypeChanges;

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
			internal set
			{
				base.Parent = value;
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

				// notify the player changes event
				if (OnRolePlayerChanges != null)
				{
					OnRolePlayerChanges(this, new RolePlayerChangedEventArgs(player, value));
				}

				// notify the new player about it played role
				if (value is Topic)
				{
					((Topic) value).AddRolePlayed(this);
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
		public ITopic Reifier
		{
			get
			{
				return reifier;
			}
			set
			{
				ReificationHelper.Reify(this, value as Topic);
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
				return _type;
			}
			set
			{
				if (value == null) throw new ModelConstraintException("Type MUST NOT be null.");
				RoleTypeChanges(_type, value);
				_type = value;
			}
		}
		#endregion

		/// <summary>
		/// Compares two <see cref="IRole"/> instances using the TMDM rules.
		/// </summary>
		/// <param name="role">The role instance to be compared.</param>
		/// <returns>
		/// [true] if both instances are equal. otherwise [false].
		/// </returns>
		/// <remarks>
		/// Association role items are equal if the values of their 
		/// [type], [player], and [parent] properties are equal. 
		/// </remarks>
		public bool Equals(IRole role)
		{
			return Equals(role, false);
		}

		/// <summary>
		/// Compares two <see cref="IRole"/> instances using the TMDM rules.
		/// </summary>
		/// <param name="role">The role instance to be compared.</param>
		/// <param name="ignoreParent">if set to <c>true</c> the parent <see cref="IAssociation"/> will be ignored.</param>
		/// <returns>
		/// [true] if both instances are equal. otherwise [false].
		/// </returns>
		/// <remarks>
		/// Association role items are equal if the values of their 
		/// [type], [player], and [parent] properties are equal. 
		/// </remarks>
		public bool Equals(IRole role, bool ignoreParent)
		{
			if (role == this)
			{
				return true;
			}

			if (role == null ||
			    !role.Type.Equals(Type) ||
			    !role.Player.Equals(Player) ||
			    (!ignoreParent && !role.Parent.Equals(Parent)))
			{
				return false;
			}

			return true;
		}

		void RoleTypeChanges(ITopic oldRoleType, ITopic newRoleType)
		{
			if (OnRoleTypeChanges != null && oldRoleType != newRoleType)
			{
				OnRoleTypeChanges(this, new RoleTypeChangesEventArgs(oldRoleType, newRoleType));
			}
		}
	}
}