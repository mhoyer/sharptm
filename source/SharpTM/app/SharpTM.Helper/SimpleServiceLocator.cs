// <copyright file="ServiceLocator.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.ServiceLocation;

namespace Pixelplastic.TopicMaps.SharpTM.Helper
{
	public class SimpleServiceLocator : ServiceLocatorImplBase
	{
		readonly Dictionary<Type, Dictionary<string, object>> _namedInstances;
		readonly Dictionary<Type, object> _instances;

		public SimpleServiceLocator()
		{
			_instances = new Dictionary<Type, object>();
			_namedInstances = new Dictionary<Type, Dictionary<string, object>>();
		}

		public void Register<T>(T instance)
		{
			Register(instance, null);
		}

		public void Register<T>(T instance, string key)
		{
			Type type = typeof(T);

			if (key == null)
			{
				if (!_instances.ContainsKey(type)) _instances.Add(type, null);
				_instances[type] = instance;
			}
			else
			{
				if (!_namedInstances.ContainsKey(type)) _namedInstances.Add(type, new Dictionary<string, object>());
				if (!_namedInstances[type].ContainsKey(key)) _namedInstances[type].Add(key, null);
				_namedInstances[type][key] = instance;
			}
		}

		protected override object DoGetInstance(Type serviceType, string key)
		{
			if (key == null && 
				_instances.ContainsKey(serviceType))
			{
				return _instances[serviceType];
			}

			if (key != null && 
				_namedInstances.ContainsKey(serviceType) && 
				_namedInstances[serviceType].ContainsKey(key))
			{
				return _namedInstances[serviceType][key];
			}

			throw new ActivationException("No instance registered for given service type.");
		}

		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			List<object> result = new List<object>();

			if (_namedInstances.ContainsKey(serviceType))
			{
				result.AddRange(_namedInstances[serviceType].Values);
			}

			if (_instances.ContainsKey(serviceType))
			{
				result.Add(_instances[serviceType]);
			}

			return result;
		}
	}
}