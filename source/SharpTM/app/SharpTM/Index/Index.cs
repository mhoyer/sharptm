// <copyright file="Index.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;
using TMAPI.Net.Index;

namespace Pixelplastic.TopicMaps.SharpTM.Index
{
	/// <summary>
	/// Implements the <see cref="IIndex"/> interface.
	/// </summary>
	public abstract class Index : IIndex
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Index"/> class.
		/// </summary>
		/// <param name="topicMapSystem">The topic map system this index is based on.</param>
		/// <param name="isAutoUpdated">if set to <c>true</c> auto update will be enabled.</param>
		protected Index(ITopicMapSystem topicMapSystem, bool isAutoUpdated)
		{
			TopicMapSystem = topicMapSystem;
			AutoUpdated = isAutoUpdated;
			IsOpen = false;
		}

		#region IIndex properties
		/// <summary>
		///     Gets a value indicating whether the index is updated automatically.
		/// </summary>
		/// <remarks>
		///	    If the value of this is
		///     <c>true</c>, then the index is automatically kept synchronized with the topic
		///     map as values are changed. If the value is <c>false</c>, then the 
		///     <see cref="M:TMAPI.Net.Index.IIndex.Reindex"/> method must be called to 
		///     resynchronize the index with the topic map after values are changed.
		/// </remarks>
		/// <returns>
		///     <c>true</c> if index is updated automatically, <c>false</c> otherwise.
		/// </returns>
		public bool AutoUpdated
		{
			get;
			private set;
		}

		/// <summary>
		///     Gets a value indicating whether the index is open.
		/// </summary>
		/// <returns>
		///     <c>true</c> if index is already opened, <c>false</c> otherwise.
		/// </returns>
		public bool IsOpen
		{
			get;
			private set;
		}
		#endregion

		/// <summary>
		/// Gets the topic map system this <see cref="LiteralIndex"/> is about.
		/// </summary>
		/// <value>The topic map system.</value>
		public ITopicMapSystem TopicMapSystem
		{
			get;
			private set;
		}

		#region IIndex methods
		/// <summary>
		///     Close the index.
		/// </summary>
		public void Close()
		{
			IsOpen = false;
		}

		/// <summary>
		///     Open the index.
		///     This method must be invoked before using any other method
		///     exported by this interface or derived interfaces.
		/// </summary>
		public void Open()
		{
			IsOpen = true;
		}

		/// <summary>
		///     Synchronize the index with data in the topic map.
		/// </summary>
		public abstract void Reindex();
		#endregion
	}
}