// <copyright file="RepositoryFactory.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Reflection;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence
{
	public class RepositoryFactory
	{
		public static TRepository GetInstance<TRepository>()
			where TRepository : new()
		{
			// TODO 
			return new TRepository();
		}

		public static TRepository GetInstance<TRepository>(string assemblyName)
		{
			Assembly assembly;
			Type interfaceType = typeof(TRepository);

			try
			{
				assembly = Assembly.Load(assemblyName);
			}
			catch (Exception ex)
			{
				throw new RepositoryException(
					String.Format(
						"Unable to get a repository instance for {0}.", 
						interfaceType.FullName), 
					ex);
			}

			foreach (Type type in assembly.GetTypes())
			{
				if (type.IsClass &&
					!type.IsAbstract &&
					interfaceType.IsAssignableFrom(type))
				{
					ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
					ConstructorInfo constructor = Array.Find(constructors, ci => ci.GetParameters().Length == 0);

					if (constructor == null)
						throw new RepositoryException(
							String.Format(
								"Unable to instanciate {0}. No public, parameterless constructor found.", 
								type.FullName));

					return (TRepository)constructor.Invoke(null);
				}
			}

			throw new RepositoryException(
				String.Format(
					"Unable to find a matching implementation for {0} in {1}.",
					interfaceType.FullName,
					assembly));
		}
	}
}