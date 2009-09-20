// <copyright file="Repository.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Repositories.InMemory
{
	public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : ConstructEntity
	{
		protected Dictionary<string, TEntity> Entities = new Dictionary<string, TEntity>();

		public List<TEntity> GetAll()
		{
			return new List<TEntity>(Entities.Values);
		}

		public TEntity GetById(string id)
		{
			TEntity entity;
			Entities.TryGetValue(id, out entity);

			return entity;
		}

		public TEntity GetByLocator(string locator)
		{
			foreach (TEntity entity in Entities.Values)
			{
				if (entity.ItemIdentifiers.Contains(locator))
				{
					return entity;
				}
			}

			return null;
		}

		public TEntity Add(TEntity entity)
		{
			Entities.Add(entity.Id, entity);

			return entity;
		}

		public TEntity Remove(TEntity entity)
		{
			Entities.Remove(entity.Id);

			return entity;
		}

		public TEntity RemoveById(string id)
		{
			return Remove(GetById(id));
		}

	}
}