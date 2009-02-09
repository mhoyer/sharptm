// <copyright file="IToBeMerged.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Merging
{
	/// <summary>
	/// Defines an interface for merge classes.
	/// </summary>
	/// <typeparam name="T">Type of <see cref="IConstruct"/>.</typeparam>
	public interface IToBeMerged<T> where T : IConstruct
	{
		/// <summary>
		/// Gets the construct that should be merged using <see cref="Into"/> method.
		/// </summary>
		/// <value>The construct to be merged.</value>
		T ToBeMerged
		{
			get;
		}

		/// <summary>
		/// Merges <see cref="ToBeMerged"/> into the specified target.
		/// </summary>
		/// <param name="target">The target to merge in.</param>
		void Into(T target);
	}
}