// <copyright file="ScopeFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System.Collections.Generic;
using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	public class ScopeFromDTO : ClassMapper<IScopedDTO, IScoped>
	{
		static readonly ScopeFromDTO mapper = new ScopeFromDTO();

		public ScopeFromDTO()
		{
			From(dto => dto.Scope)
				.To((scoped, scopeDTO)
				    =>
				    	{
				    		if (scopeDTO != null)
				    		{
				    			foreach (LocatorDTO scope in scopeDTO.TopicReferences)
				    			{
				    				scoped.AddTheme(TopicFromDTO.FindOrCreate(scoped.TopicMap, scope));
				    			}
				    		}
				    	});
		}

		public static ScopeFromDTO Instance
		{
			get
			{
				return mapper;
			}
		}

		public static IList<ITopic> Create(ITopicMap parent, ScopeDTO scope)
		{
			IList<ITopic> scopeThemes = new List<ITopic>();

			foreach (LocatorDTO locator in scope.TopicReferences)
			{
				scopeThemes.Add(TopicFromDTO.FindOrCreate(parent, locator));
			}

			return scopeThemes;
		}
	}
}