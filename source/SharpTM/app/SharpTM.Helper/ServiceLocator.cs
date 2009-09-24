// <copyright file="ServiceLocator.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Helper
{
	public class ServiceLocator
	{
		static Dictionary<Type, object> _instances = new Dictionary<Type, object>();

		public static void Register<T>(T instance)
		{
			Type type = typeof(T);

			if (_instances.ContainsKey(type))
			{
				_instances[type] = instance;
			}
			else
			{
				_instances.Add(type, instance);
			}
		}

		public static T GetInstance<T>()
		{
			Type type = typeof(T);

			if (_instances.ContainsKey(type))
			{
				return (T) _instances[type];
			}

			return default(T);
		}
	}
}