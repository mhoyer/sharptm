// <copyright file="IConnector.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts
{
	public interface IRepository<TTMAPIEntity> where TTMAPIEntity : IConstruct
	{
		TTMAPIEntity Load(object id);
		void Save(TTMAPIEntity tmapiEntity);
	}

	public interface IRepositoryV2<T>
	{
		List<T> GetAll();
		T GetById(string id);
		T GetByLocator(string locator);

		T Add(T entity);

		T Remove(T entity);
		T RemoveById(string id);
	}


	public interface ITopicMapRepository : IRepositoryV2<TopicMapEntity> { }
    
	public interface ITopicRepository : IRepositoryV2<TopicEntity> { }

	public interface IScopedRepository<TEntity> : IRepositoryV2<TEntity> { }

	public interface IAssociationRepository : IScopedRepository<AssociationEntity> { }

	public interface IOccurrenceRepository : IScopedRepository<AssociationEntity> { }
}