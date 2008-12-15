using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements <see cref="EventArgs"/> for the <see cref="Role.OnRoleTypeChanges"/> event.
	/// </summary>
	public class RoleTypeChangesEventArgs : EventArgs
	{
		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="RoleTypeChangesEventArgs"/> class.
		/// </summary>
		/// <param name="oldRoleType">Old type of the role.</param>
		/// <param name="newRoleType">New type of the role.</param>
		public RoleTypeChangesEventArgs(ITopic oldRoleType, ITopic newRoleType)
		{
			OldRoleType = oldRoleType;
			NewRoleType = newRoleType;
		}
		#endregion

		#region properties
		/// <summary>
		/// Gets the new type of the role.
		/// </summary>
		/// <value>The new type of the role.</value>
		public ITopic NewRoleType
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the old type of the role.
		/// </summary>
		/// <value>The old type of the role.</value>
		public ITopic OldRoleType
		{
			get;
			private set;
		}
		#endregion
	}
}