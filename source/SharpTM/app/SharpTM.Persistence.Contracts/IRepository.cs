// <copyright file="IRepository.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts
{
	public interface IRepository<T>
	{
		List<T> GetAll();
		T GetById(string id);
		T GetByLocator(string locator);

		T Add(T entity);

		T Remove(T entity);
		T RemoveById(string id);
	}

	public interface IScopedRepository<TEntity> : IRepository<TEntity> { }

	public interface ITopicMapRepository : IRepository<TopicMapEntity>
	{
		ITopicRepository TopicRepository { get; }
		IAssociationRepository AssociationRepository { get; }
	}

	public interface ITopicRepository : IRepository<TopicEntity> { }
	public interface IOccurrenceRepository : IScopedRepository<OccurrenceEntity> { }
	public interface INameRepository<TEntity> : IScopedRepository<NameEntity> { }
	public interface IVariantRepository : IScopedRepository<VariantEntity> { }

	public interface IAssociationRepository : IScopedRepository<AssociationEntity> { }
	public interface IRoleRepository : IRepository<RoleEntity> { }
}