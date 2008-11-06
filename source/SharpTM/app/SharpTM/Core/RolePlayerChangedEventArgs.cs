using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements <see cref="EventArgs"/> for the <see cref="Role.OnRolePlayerChanges"/> event.
	/// </summary>
	public class RolePlayerChangedEventArgs : EventArgs
	{
		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="RolePlayerChangedEventArgs"/> class.
		/// </summary>
		/// <param name="oldPlayer">The old player.</param>
		/// <param name="newPlayer">The new player.</param>
		public RolePlayerChangedEventArgs(ITopic oldPlayer, ITopic newPlayer)
		{
			OldPlayer = oldPlayer;
			NewPlayer = newPlayer;
		}
		#endregion

		#region properties
		/// <summary>
		/// Gets the new player when <see cref="IRole.Player"/> property changes.
		/// </summary>
		/// <value>The new player.</value>
		public ITopic NewPlayer
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the old player when <see cref="IRole.Player"/> property changes.
		/// </summary>
		/// <value>The old player.</value>
		public ITopic OldPlayer
		{
			get;
			private set;
		}
		#endregion
	}
}