// <copyright file="IRepository.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2008-2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.Contracts
{
	/// <summary>
	/// Represents a generic repository of objects.
	/// </summary>
	/// <typeparam name="T">The generic type of the repository.</typeparam>
	public interface IRepository<T>
	{
		/// <summary>
		/// Get the entity from the persistence store, or return null
		/// if it doesn't exist.
		/// </summary>
		/// <param name="id">The entity's id.</param>
		/// <returns>Either the entity that matches the id, or a null.</returns>
		T Load(object id);

		/// <summary>
		/// Register the entity for save in the database when the unit of work
		/// is completed.
		/// </summary>
		/// <param name="entity">the entity to save</param>
		/// <returns>The saved entity</returns>
		T Save(T entity);
	}
}