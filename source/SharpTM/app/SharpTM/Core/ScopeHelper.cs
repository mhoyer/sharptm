// <copyright file="ScopeHelper.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	public class ScopeHelper
	{
		/// <summary>
		/// Adds a <see cref="T:TMAPI.Net.Core.ITopic"/> to the scope.
		/// </summary>
		/// <param name="scopable">The scopeable construct.</param>
		/// <param name="currentScope">The current scope.</param>
		/// <param name="theme">The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be added to the scope.</param>
		/// <exception cref="ModelConstraintException">
		/// If the <paramref name="theme"/> is <c>null</c>.
		/// </exception>
		public static void AddTheme(IScoped scopable, IList<ITopic> currentScope, ITopic theme)
		{
			if (theme == null)
				throw new ModelConstraintException(
					scopable, 
					"Themes cannot be null when adding to scope.", 
					new ArgumentNullException("theme"));

			if (currentScope.Contains(theme)) return;

			currentScope.Add(theme);
		}

		/// <summary>
		/// Adds a list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> to the current scope.
		/// </summary>
		/// <param name="scopable">The scopeable construct.</param>
		/// <param name="currentScope">The scope list.</param>
		/// <param name="themes">The list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> that should be added to the currentScope.</param>
		public static void AddThemes(IScoped scopable, IList<ITopic> currentScope, IEnumerable<ITopic> themes)
		{
			if (themes == null) return;

			foreach (ITopic theme in themes)
			{
				AddTheme(scopable, currentScope, theme);
			}
		}

		/// <summary>
		/// Removes a <see cref="T:TMAPI.Net.Core.ITopic"/> from the current scope.
		/// </summary>
		/// <param name="currentScope">The current scope.</param>
		/// <param name="theme">The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be removed from the scope.</param>
		public static void RemoveTheme(IList<ITopic> currentScope, ITopic theme)
		{
			if (theme == null && currentScope.Count == 0) return;

			currentScope.Remove(theme);
		}
	}
}