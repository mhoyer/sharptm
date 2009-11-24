// <copyright file="Association.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pixelplastic.TopicMaps.SharpTM.Core.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IAssociation"/> interface.
	/// </summary>
	public class Association : Construct, IAssociation
	{
		internal readonly AssociationData AssociationData;
	
		/// <summary>
		/// Initializes a new instance of the <see cref="Association"/> class.
		/// </summary>
		/// <param name="entity">The underlying domain object.</param>
		/// <param name="topicMap">The parent <see cref="ITopicMap"/> that contains this instance.</param>
		internal Association(AssociationEntity entity, ITopicMap topicMap)
			: base(entity, topicMap, topicMap)
		{
			if (entity == null) throw new ArgumentNullException("entity");

			AssociationData = new AssociationData();
		}

		#region IAssociation properties
		/// <summary>
		///    Gets the <see cref="T:TMAPI.Net.Core.ITopicMap"/> this association belongs to.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.ITopicMap"/> this association belongs to.
		/// </returns>
		public new ITopicMap Parent
		{
			get
			{
				return TopicMap;
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
				return AssociationData.Reifier;
			}
			set
			{
				ReificationHelper.Reify(this, value as Topic);
			}
		}

		/// <summary>
		///     Gets the roles participating in this association.
		/// </summary>
		/// <remarks>
		///     The return value may be empty but must never be <c>null</c>.
		/// </remarks>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IRole">IRoles</see>.
		/// </returns>
		public ReadOnlyCollection<IRole> Roles
		{
			get { return AssociationData.Roles; }
		}

		/// <summary>
		///     Gets the role types participating in this association.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of role types.
		/// </returns>
		public ReadOnlyCollection<ITopic> RoleTypes
		{
			get { return AssociationData.RoleTypes; }
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		///     An empty set represents the unconstrained scope.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		/// </returns>
		public ReadOnlyCollection<ITopic> Scope
		{
			get { return AssociationData.Scope; }
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
				return AssociationData.Type;
			}
			set
			{
				if (value == null) throw new ModelConstraintException(this, "Type MUST NOT be null.");
				AssociationData.Type = value;
			}
		}
		#endregion

		#region IAssociation methods
		/// <summary>
		///     Adds a <see cref="T:TMAPI.Net.Core.ITopic"/> to the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be added to the scope.
		/// </param>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="theme"/> is <c>null</c>.
		/// </exception>
		public void AddTheme(ITopic theme)
		{
			ScopeHelper.AddTheme(this, AssociationData.Scope, theme);
		}

		/// <summary>
		///     Removes a <see cref="T:TMAPI.Net.Core.ITopic"/> from the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be removed from the scope.
		/// </param>
		public void RemoveTheme(ITopic theme)
		{
			ScopeHelper.RemoveTheme(AssociationData.Scope, theme);
		}

		/// <summary>
		///     Creates a new <see cref="T:TMAPI.Net.Core.IRole"/> representing a role in this association.
		/// </summary>
		/// <param name="roleType">
		///     The role type; must not be <c>null</c>.
		/// </param>
		/// <param name="player">
		///     The role player; must not be <c>null</c>.
		/// </param>
		/// <returns>
		///     A newly created association role.
		/// </returns>
		/// <exception cref="T:TMAPI.Net.Core.ModelConstraintException">
		///     If the <paramref name="roleType"/> or <paramref name="player"/> is <c>null</c>.
		/// </exception>
		public IRole CreateRole(ITopic roleType, ITopic player)
		{
			if (roleType == null)
			{
				string message = String.Format(
					"Unable to create a role for association {0}. A role must have a role type.",
					Id);

				throw new ModelConstraintException(
					this, 
					message,
					new ArgumentNullException("roleType"));
			}

			if (player == null)
			{
				string message = String.Format(
					"Unable to create a role for association {0}. A role must have a player.",
					Id);

				throw new ModelConstraintException(
					this,
					message,
					new ArgumentNullException("player"));
			}

			// Create new role
			Role role = new Role(this, TopicMap, player, roleType);
			AddRole(role);

			// HACK should be solved by delegates
			if (TopicMap is TopicMap)
			{
				((TopicMap) TopicMap).AddConstruct(role);
			}

			return role;
		}

		/// <summary>
		/// Deletes this association from its parent container.
		/// </summary>
		/// <remarks>
		/// Clears the list of roles and the list of role types.
		/// </remarks>
		public new void Remove()
		{
			for (int i = Roles.Count; i > 0; i--)
			{
				Roles[i - 1].Remove();
			}

			AssociationData.Roles.Clear();
			AssociationData.RoleTypes.Clear();

			base.Remove();
		}

		/// <summary>
		///     Returns all roles with the specified <c>type</c>.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <param name="type">
		///     The type of the <see cref="T:TMAPI.Net.Core.IRole"/> instances to be returned, must not be <c>null</c>.
		/// </param>
		/// <returns>
		///     An unmodifiable (maybe empty) set of roles with the specified <c>type</c> property.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     If the <paramref name="type"/> is <c>null</c>.
		/// </exception>
		public ReadOnlyCollection<IRole> GetRolesByTopicType(ITopic type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			List<IRole> foundRoles = new List<IRole>();

			foreach (IRole role in Roles)
			{
				if (role.Type == type)
				{
					foundRoles.Add(role);
				}
			}

			return foundRoles.AsReadOnly();
		}
		#endregion

		/// <summary>
		/// Compares two <see cref="IAssociation"/> instances using the TMDM rules.
		/// </summary>
		/// <param name="association">The association instance to be compared.</param>
		/// <returns>[true] if both instances are equal. otherwise [false].</returns>
		/// <remarks>
		/// Associations are equal if [scope], [type], and [roles] properties are equal. 
		/// <see cref="http://www.isotopicmaps.org/sam/sam-model/#sect-association"/>
		/// </remarks>
		public bool Equals(IAssociation association)
		{
			return Equals(association, false);
		}

		/// <summary>
		/// Compares two <see cref="IAssociation"/> instances using the TMDM rules.
		/// </summary>
		/// <param name="association">The association instance to be compared.</param>
		/// <param name="ignoreRoles">if set to <c>true</c> the <see cref="Roles"/> are not included when comparing.</param>
		/// <returns>
		/// [true] if both instances are equal. otherwise [false].
		/// </returns>
		/// <remarks>
		/// Associations are equal if [scope], [type], and [roles] properties are equal.
		/// <see cref="http://www.isotopicmaps.org/sam/sam-model/#sect-association"/>
		/// </remarks>
		public bool Equals(IAssociation association, bool ignoreRoles)
		{
			if (association == this)
			{
				return true;
			}

			if (association == null ||
				association.Scope.Count != Scope.Count ||
				(!ignoreRoles && association.Roles.Count != Roles.Count) ||
				!association.Type.Equals(Type))
			{
				return false;
			}

			foreach (ITopic scope in Scope)
			{
				if (!association.Scope.Contains(scope))
				{
					return false;
				}
			}

			if (!ignoreRoles)
			{
				foreach (Role role in Roles)
				{
					foreach (IRole roleToBeCompared in association.Roles)
					{
						if (role.Equals(roleToBeCompared, true))
						{
							return true;
						}
					}
				}
			}

			return true;
		}

		internal void AddRole(Role role)
		{
			role.Parent = this;
			role.OnRemove += Role_OnRemove;
			role.OnRoleTypeChanges += Role_OnRoleTypeChanges;
			AssociationData.Roles.Add(role);

			if (!AssociationData.RoleTypes.Contains(role.Type))
			{
				AssociationData.RoleTypes.Add(role.Type);
			}
		}

		internal void RemoveRole(IRole roleToBeRemoved)
		{
			AssociationData.Roles.Remove(roleToBeRemoved);

			if (!AssociationData.Roles.Exists(r => r.Type == roleToBeRemoved.Type))
			{
				AssociationData.RoleTypes.Remove(roleToBeRemoved.Type);
			}

			if (roleToBeRemoved is Role)
			{
				((Role) roleToBeRemoved).Parent = null;
				((Role) roleToBeRemoved).OnRemove -= Role_OnRemove;
				((Role) roleToBeRemoved).OnRoleTypeChanges -= Role_OnRoleTypeChanges;
			}
		}

		/// <summary>
		/// Handles the <see cref="Construct.OnRemove"/> event of the <see cref="Role"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Role_OnRemove(object sender, EventArgs e)
		{
			if (sender != null && sender is IRole)
			{
				IRole removedRole = sender as IRole;
				RemoveRole(removedRole);
			}
		}

		/// <summary>
		/// Handles the <see cref="Role.OnRoleTypeChanges"/> event of the <see cref="Role"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="Pixelplastic.TopicMaps.SharpTM.Core.RoleTypeChangesEventArgs"/> instance containing the event data.</param>
		void Role_OnRoleTypeChanges(object sender, RoleTypeChangesEventArgs e)
		{
			if (e != null && e.OldRoleType != null)
			{
				AssociationData.RoleTypes.Remove(e.OldRoleType);
			}

			if (e != null && e.NewRoleType != null)
			{
				AssociationData.RoleTypes.Add(e.NewRoleType);
			}
		}
	}
}
