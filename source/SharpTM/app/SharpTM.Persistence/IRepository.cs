// <copyright file="IRepository.cs" company="Pixelplastic">
// Copyright (©) Marcel Hoyer 2008. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence
{
	/// <summary>
	/// Defines the contract to implement specific repositories to store topic maps.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Saves the specified topic map.
		/// </summary>
		/// <param name="topicMap">The topic map to be saved.</param>
		void Save(ITopicMap topicMap);

		/// <summary>
		/// Loads a topic map into the specified <see cref="ITopicMapSystem">topic map system</see>.
		/// </summary>
		/// <param name="topicMapSystem">The topic map system that should contain the topic map.</param>
		/// <returns>The loaded topic map.</returns>
		ITopicMap Load(ITopicMapSystem topicMapSystem);
	}
}