using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements a helper class for <see cref="IScoped"/> constructs.
	/// </summary>
	internal class Scoped
	{
		#region readonly & static fields
		/// <summary>
		/// Represents the current list of topics that scope a <see cref="IScoped"/> construct.
		/// </summary>
		private readonly List<ITopic> scope;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Scoped"/> class.
		/// </summary>
		internal Scoped()
		{
			scope = new List<ITopic>();
			Scope = scope.AsReadOnly();
		}
		#endregion

		#region properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		///     An empty set represents the unconstrained scope.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		/// </returns>
		internal ReadOnlyCollection<ITopic> Scope
		{
			get;
			private set;
		}
		#endregion

		#region methods
		/// <summary>
		///     Adds a <see cref="T:TMAPI.Net.Core.ITopic"/> to the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be added to the scope.
		/// </param>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="theme"/> is <c>null</c>.
		/// </exception>
		internal void AddTheme(ITopic theme)
		{
			if (theme == null)
			{
				throw new ModelConstraintException("Themes cannot be null when adding to scope.", new ArgumentNullException("theme"));
			}

			if (scope.Contains(theme))
			{
				return;
			}

			scope.Add(theme);
		}

		/// <summary>
		///     Adds a list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> to the scope.
		/// </summary>
		/// <param name="themes">
		///     The list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> that should be added to the scope.
		/// </param>
		internal void AddThemes(IEnumerable<ITopic> themes)
		{
			if (themes == null)
			{
				return;
			}

			foreach (ITopic theme in themes)
			{
				AddTheme(theme);
			}
		}

		/// <summary>
		///     Removes a <see cref="T:TMAPI.Net.Core.ITopic"/> from the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be removed from the scope.
		/// </param>
		internal void RemoveTheme(ITopic theme)
		{
			if (theme == null && scope.Count == 0)
			{
				return;
			}

			scope.Remove(theme);
		}
		#endregion
	}
}