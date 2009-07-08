// <copyright file="RepositoryFactory.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.IO;
using System.Reflection;
using Pixelplastic.TopicMaps.SharpTM.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence
{
	public class RepositoryFactory<T>
		where T : ConstructDTO
	{
		static IRepository<T> instance;

		public static IRepository<T> GetRepositoryInstance()
		{
			if (instance == null)
			{
				try
				{
					Assembly asm = Assembly.Load(Properties.Settings.Default.PersistenceAssembly);

					foreach (Type type in asm.GetTypes())
					{
						if (typeof(IRepository<T>).IsAssignableFrom(type))
						{
							instance = (IRepository<T>)asm.CreateInstance(type.FullName);
							break;
						}
					}
				}
				catch(FileNotFoundException ex)
				{
					throw new RepositoryExcetion(
						string.Format("Unable to find the configured repository assembly \"{0}\". Please check the config file.",
						              Properties.Settings.Default.PersistenceAssembly),
						ex);
				}
			}

			return instance;
		}
	}
}