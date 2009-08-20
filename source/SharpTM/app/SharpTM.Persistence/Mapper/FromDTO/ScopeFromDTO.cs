// <copyright file="ScopeFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class ScopeFromDTO : ClassMapper<ScopeDTO, IScoped>
	{
		private static readonly ScopeFromDTO mapper = new ScopeFromDTO();
		
		public static ScopeFromDTO Instance
		{
			get
			{
				return mapper;
			}
		}

		public ScopeFromDTO()
		{
			From(scopeDTO => scopeDTO.TopicReferences)
				.To((scoped, topicReferences)
				    =>
				    	{
				    		foreach (LocatorDTO scope in topicReferences)
				    		{
				    			scoped.AddTheme(TopicFromDTO.FindOrCreate(scoped.TopicMap, scope));
				    		}
				    	});
		}

		public static IList<ITopic> Create(ITopicMap parent, ScopeDTO scope)
		{
			IList<ITopic> scopeThemes = new List<ITopic>();

			foreach (LocatorDTO dto in scope.TopicReferences)
			{
				scopeThemes.Add(TopicFromDTO.FindOrCreate(parent, dto));
			}

			return scopeThemes;
		}
	}
}