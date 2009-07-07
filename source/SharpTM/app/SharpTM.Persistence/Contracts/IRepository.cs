// <copyright file="IRepository.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2008-2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Contracts
{
	/// <summary>
	/// Represents a generic repository of objects.
	/// </summary>
	/// <typeparam name="T">The generic type of the repository.</typeparam>
	public interface IRepository<T>
	{
		/// <summary>
		/// Counts the number of instances matching the criteria.
		/// </summary>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>The number of matching instances.</returns>
		long Count(Predicate<T> criteria);

		/// <summary>
		/// Counts the overall number of instances.
		/// </summary>
		/// <returns>The number of all instances.</returns>
		long Count();

		/// <summary>
		/// Create an instance of <typeparamref name="T"/>, mapping it to the concrete class 
		/// if needed.
		/// </summary>
		/// <returns>The created instance.</returns>
		T Create();

		/// <summary>
		/// Register the entity for deletion when the unit of work
		/// is completed. 
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		void Delete(T entity);

		/// <summary>
		/// Registers all entities for deletion when the unit of work
		/// is completed.
		/// </summary>
		void DeleteAll();

		/// <summary>
		/// Registers all entities for deletion that match the supplied
		/// criteria condition when the unit of work is completed.
		/// </summary>
		/// <param name="where">Criteria condition to select the rows to be deleted</param>
		void DeleteAll(Predicate<T> where);

		/// <summary>
		/// Check if any instance matches the criteria.
		/// </summary>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>
		/// 	<c>true</c> if an instance is found; otherwise <c>false</c>.
		/// </returns>
		bool Exists(Predicate<T> criteria);

		/// <summary>
		/// Check if any instance of the type exists
		/// </summary>
		/// <returns><c>true</c> if an instance is found; otherwise <c>false</c>.</returns>
		bool Exists();

		/// <summary>
		/// Loads all the entities that match the criteria
		/// by order
		/// </summary>
		/// <param name="order">The order to sort the results.</param>
		/// <param name="criteria">The criteria to look for</param>
		/// <returns>All the entities that match the criteria</returns>
		ICollection<T> FindAll(Comparison<T> order, params Predicate<T>[] criteria);

		/// <summary>
		/// Finds all the entities that match the criteria by order.
		/// </summary>
		/// <param name="criteria">the criteria to look for</param>
		/// <param name="orders">The order handlers to sort the results.</param>
		/// <returns>All the entities that match the criteria</returns>
		ICollection<T> FindAll(Predicate<T> criteria, params Comparison<T>[] orders);

		/// <summary>
		/// Loads all the entities that match the criteria
		/// by order.
		/// </summary>
		/// <param name="criteria">the criteria to look for</param>
		/// <param name="orders"> the order to load the entities</param>
		/// <param name="firstResult">the first result to load</param>
		/// <param name="maxResults">the number of result to load</param>
		/// <returns>All the entities that match the criteria</returns>
		ICollection<T> FindAll(
			Predicate<T> criteria,
			int firstResult,
			int maxResults,
			params Comparison<T>[] orders);

		/// <summary>
		/// Loads all the entities that match the criteria by order.
		/// </summary>
		/// <param name="orders">The comparator to order the result.</param>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>All the entities that match the criteria.</returns>
		ICollection<T> FindAll(Comparison<T>[] orders, params Predicate<T>[] criteria);

		/// <summary>
		/// Loads all the entities that match the criteria.
		/// </summary>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>All the entities that match the criteria.</returns>
		ICollection<T> FindAll(params Predicate<T>[] criteria);

		/// <summary>
		/// Loads all the entities that match the criteria, and allow paging.
		/// </summary>
		/// <param name="firstResult">The first result to load.</param>
		/// <param name="numberOfResults">Total number of results to load.</param>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>Number of results of entities that match the criteria.</returns>
		ICollection<T> FindAll(int firstResult, int numberOfResults, params Predicate<T>[] criteria);

		/// <summary>
		/// Loads all the entities that match the criteria, with paging 
		/// and ordering by a single field.
		/// <param name="firstResult">The first result to load</param>
		/// <param name="numberOfResults">Total number of results to load</param>
		/// <param name="selectionOrder">The field the repository should order by.</param>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>Number of results of entities that match the criteria.</returns>
		/// </summary>
		ICollection<T> FindAll(
			int firstResult,
			int numberOfResults,
			Comparison<T> selectionOrder,
			params Predicate<T>[] criteria);

		/// <summary>
		/// Loads all the entities that match the criteria, with paging 
		/// and ordering by a multiple fields.
		/// </summary>
		/// <param name="firstResult">The first result to load.</param>
		/// <param name="numberOfResults">Total number of results to load.</param>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>Number of results of entities that match the criteria.</returns>
		/// <param name="selectionOrder">The fields the repository should order by</param>
		ICollection<T> FindAll(
			int firstResult,
			int numberOfResults,
			Comparison<T>[] selectionOrder,
			params Predicate<T>[] criteria);

		/// <summary>
		/// Find the entity based on a criteria.
		/// </summary>
		/// <param name="criteria">The criteria to look for.</param>
		/// <param name="orders">Optional ordering.</param>
		/// <returns>The entity or null.</returns>
		T FindFirst(Predicate<T> criteria, params Comparison<T>[] orders);

		/// <summary>
		/// Find the first entity of type.
		/// </summary>
		/// <param name="orders">Optional ordering.</param>
		/// <returns>The entity or null.</returns>
		T FindFirst(params Comparison<T>[] orders);

		/// <summary>
		/// Find a single entity based on a criteria.
		/// Throws if there is more than one result.
		/// </summary>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>The entity or null.</returns>
		T FindOne(params Predicate<T>[] criteria);

		/// <summary>
		/// Find a single entity based on a criteria.
		/// Throws if there is more than one result.
		/// </summary>
		/// <param name="criteria">The criteria to look for.</param>
		/// <returns>The entity or null.</returns>
		T FindOne(Predicate<T> criteria);

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

		/// <summary>
		/// Saves or updates the entity, based on its unsaved-value.
		/// </summary>
		/// <param name="entity">The entity to save or update.</param>
		/// <returns>The saved or updated entity</returns>
		T SaveOrUpdate(T entity);

		/// <summary>
		/// Saves or updates a copy of entity, based on its unsaved-value.
		/// </summary>
		/// <param name="entity">The entity to be saved.</param>
		/// <returns>The saved entity</returns>
		T SaveOrUpdateCopy(T entity);

		/// <summary>
		/// Register the entity for update in the database when the unit of work
		/// is completed.
		/// </summary>
		/// <param name="entity">The entity to be updated.</param>
		void Update(T entity);
	}
}