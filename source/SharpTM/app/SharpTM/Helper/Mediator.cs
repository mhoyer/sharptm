// <copyright file="Mediator.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;
using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;

namespace Pixelplastic.TopicMaps.SharpTM.Helper
{
	public abstract class Mediator<TConstruct, TEntity>
		where TConstruct : Construct
		where TEntity : ConstructEntity
	{
		Dictionary<string, KeyValuePair<TConstruct, TEntity>> _index;
		readonly IRepository<TEntity> _repository;
		readonly Func<TConstruct, TEntity> _createAction;

		Dictionary<string, KeyValuePair<TConstruct, TEntity>> Index
		{
			get
			{
				if (_index == null) InitializeIndex();

				return _index;
			}
		}

		public Mediator(IRepository<TEntity> repository, Func<TConstruct, TEntity> createAction)
		{
			_repository = repository;
			_createAction = createAction;
		}

		public TConstruct Create(TEntity entity)
		{
			TConstruct construct = _createAction.Invoke(entity);
			Add(construct);
			_repository.Add(entity);

			return construct;
		}

		public void Delete(TConstruct construct)
		{
			TEntity entity = (TEntity) construct.Entity;
			Remove(entity);
			_repository.Remove(entity);
		}

		public bool Exists(Predicate<TEntity> pattern)
		{
			// TODO _repository.Exists(pattern);
			return _repository.GetAll().Exists(pattern);
		}

		public TConstruct Find(Predicate<TEntity> pattern)
		{
			// TODO _repository.Find(pattern);
			TEntity found = _repository.GetAll().Find(pattern);

			return Get(found);
		}

		public TConstruct Get(TEntity entity)
		{
			return Index[entity.Id].Key;
		}

		public TEntity Get(TConstruct construct)
		{
			return Index[construct.Id].Value;
		}

		private void InitializeIndex()
		{
			_index = new Dictionary<string, KeyValuePair<TConstruct, TEntity>>();

			_repository.GetAll().ForEach(entity => Create(entity));
		}

		public List<TConstruct> GetAll()
		{
			List<TConstruct> result = new List<TConstruct>();

			foreach (KeyValuePair<TConstruct, TEntity> pair in Index.Values)
			{
				result.Add(pair.Key);
			}

			return result;
		}

		public List<TTarget> GetAll<TTarget>(Converter<TConstruct, TTarget> mapper)
		{
			List<TTarget> result = new List<TTarget>();
			
			foreach (KeyValuePair<TConstruct, TEntity> pair in Index.Values)
			{
				result.Add(mapper.Invoke(pair.Key));
			}

			return result;
		}

		protected void Add(TConstruct construct)
		{
			TEntity entity = (TEntity) construct.Entity;
			Index.Add(entity.Id,
			          new KeyValuePair<TConstruct, TEntity>(construct, entity));
		}

		protected void Remove(TEntity entity)
		{
			Index.Remove(entity.Id);
		}
	}
}