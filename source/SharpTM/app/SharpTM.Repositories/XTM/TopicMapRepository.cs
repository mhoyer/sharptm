// <copyright file="TopicMapRepository.cs" company="Pixelplastic">
// Copyright (©) Marcel Hoyer 2008. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.IO;
using Pixelplastic.TopicMaps.SharpTM.Contracts;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM
{
	/// <summary>
	/// Implements <see cref="IRepository{T}"/> for a <see cref="ITopicMap"/> repository.
	/// </summary>
	public class TopicMapRepository : IRepository<ITopicMap>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMapRepository"/> class.
		/// </summary>
		public TopicMapRepository() :
			this (Properties.Settings.Default.DefaultStoragePath)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMapRepository"/> class.
		/// </summary>
		/// <param name="xtmStoragePath">The XTM storage path.</param>
		public TopicMapRepository(string xtmStoragePath)
		{
			if (string.IsNullOrEmpty(xtmStoragePath))
			{
				xtmStoragePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			}

			if (!Directory.Exists(xtmStoragePath))
			{
				throw new ArgumentException(String.Format("Directory {0} does not exist.", xtmStoragePath), "xtmStoragePath");
			}

			StoragePath = Path.GetFullPath(xtmStoragePath);
		}

		/// <summary>
		/// Gets the storage path for XTM files.
		/// </summary>
		/// <value>The storage path for XTM files.</value>
		public string StoragePath
		{
			get;
			private set;
		}

		public ITopicMap Load(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}

			throw new NotImplementedException();
		}

		public ITopicMap Save(ITopicMap entity)
		{
			throw new NotImplementedException();
		}
	}
}