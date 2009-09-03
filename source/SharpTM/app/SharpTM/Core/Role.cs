// <copyright file="Role.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using Pixelplastic.TopicMaps.SharpTM.Core.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IRole"/> interface.
	/// </summary>
	public class Role : Construct, IRole
	{
		internal RoleData roleData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Role"/> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="topicMap">The topic map.</param>
		/// <param name="initialPlayer">The initial player playing this role.</param>
		/// <param name="roleType">Type of the role. MUST NOT be null.</param>
		internal Role(IAssociation parent, ITopicMap topicMap, ITopic initialPlayer, ITopic roleType)
			: this(new RoleData(), parent, topicMap, initialPlayer, roleType)
		{}

		internal Role(RoleData data, IAssociation parent, ITopicMap topicMap, ITopic initialPlayer, ITopic roleType)
			: base(data, parent, topicMap)
		{
			if (initialPlayer == null && data.Player == null)
			{
				throw new ModelConstraintException(
					"The role player MUST NOT be null.",
					new ArgumentNullException("initialPlayer"));
			}

			if (roleType == null && data.Type == null)
			{
				throw new ModelConstraintException(
					"A role MUST NOT be untyped.",
					new ArgumentNullException("roleType"));
			}

			roleData = data;

			if (roleType != null) Type = roleType;
			if (initialPlayer != null) Player = initialPlayer;
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
				return roleData.Player;
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
					OnRolePlayerChanges(this, new RolePlayerChangedEventArgs(roleData.Player, value));
				}

				// notify the new player about it played role
				if (value is Topic)
				{
					((Topic) value).AddRolePlayed(this);
				}

				roleData.Player = value;
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
				return roleData.Reifier;
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
				return roleData.Type;
			}
			set
			{
				if (value == null) throw new ModelConstraintException("Type MUST NOT be null.");
				RoleTypeChanges(roleData.Type, value);
				roleData.Type = value;
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