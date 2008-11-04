using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IAssociation"/> interface.
	/// </summary>
	public class Association : Construct, IAssociation
	{
		#region readonly & static fields
		/// <summary>
		/// Represents the list of current roles played by this association.
		/// </summary>
		private readonly List<IRole> roles;

		/// <summary>
		/// Represents the list of role types this association is involved.
		/// </summary>
		private readonly List<ITopic> roleTypes;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Association"/> class.
		/// </summary>
		/// <param name="topicMap">The parent <see cref="ITopicMap"/> that contains this instance.</param>
		/// <param name="associationType">
		///     The association type, MUST NOT be <c>null</c>.
		/// </param>
		/// <param name="initialThemes">
		///     A collection of themes or <c>null</c> if the association should be in the unconstrained scope.
		/// </param>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="associationType"/> is <c>null</c>.
		/// </exception>
		internal Association(ITopicMap topicMap, ITopic associationType, IEnumerable<ITopic> initialThemes)
			: base(topicMap, topicMap)
		{
			if (associationType == null)
			{
				throw new ModelConstraintException(
					"The type of an association MUST NOT be null.",
					new ArgumentNullException("associationType"));
			}

			roles = new List<IRole>();
			roleTypes = new List<ITopic>();
			scope = new List<ITopic>();

			Type = associationType;
			
			if (initialThemes != null)
			{
				scope.AddRange(initialThemes);
			}
		}
		#endregion

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
			get
			{
				return roles.AsReadOnly();
			}
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
			get
			{
				return roleTypes.AsReadOnly();
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		///     An empty set represents the unconstrained scope.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		/// </returns>
		public new ReadOnlyCollection<ITopic> Scope
		{
			get
			{
				return base.Scope;
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
		public new void AddTheme(ITopic theme)
		{
			base.AddTheme(theme);
		}

		/// <summary>
		///     Removes a <see cref="T:TMAPI.Net.Core.ITopic"/> from the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be removed from the scope.
		/// </param>
		public new void RemoveTheme(ITopic theme)
		{
			base.RemoveTheme(theme);
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
					message,
					new ArgumentNullException("roleType"));
			}

			if (player == null)
			{
				string message = String.Format(
					"Unable to create a role for association {0}. A role must have a player.",
					Id);

				throw new ModelConstraintException(
					message,
					new ArgumentNullException("player"));
			}

			throw new NotImplementedException();
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
			throw new System.NotImplementedException();
		}
		#endregion
	}
}