// <copyright file="IConnector.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2008-2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts
{
	/// <summary>
	/// Represents a generic connector interface.
	/// </summary>
	/// <typeparam name="TDTO">The DTO type, this connector is adressing.</typeparam>
	public interface IConnector<TDTO> where TDTO : IConstructDTO
	{
		/// <summary>
		/// Get the entity from the persistence store, or return null
		/// if it doesn't exist.
		/// </summary>
		/// <param name="id">The entity's id.</param>
		/// <returns>Either the entity that matches the id, or a null.</returns>
		TDTO Load(object id);

		/// <summary>
		/// Register the dto for save in the database when the unit of work
		/// is completed.
		/// </summary>
		/// <param name="dto">the entity to save</param>
		void Save(TDTO dto);
	}
}