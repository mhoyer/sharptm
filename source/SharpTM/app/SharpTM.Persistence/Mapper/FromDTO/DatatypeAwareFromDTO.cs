// <copyright file="OccurrenceFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class DatatypeAwareFromDTO : ClassMapper<IDatatypeAwareDTO, IDatatypeAware>
	{
		private static DatatypeAwareFromDTO mapper;

		/// <summary>
		/// Gets an instance of a <see cref="DatatypeAwareFromDTO"/> mapper.
		/// </summary>
		/// <value>The instance.</value>
		public static DatatypeAwareFromDTO Instance
		{
			get
			{
				if (mapper == null)
				{
					mapper = new DatatypeAwareFromDTO();
				}

				return mapper;
			}
		}

		private DatatypeAwareFromDTO()
		{
			From(dto => dto.ResourceData)
				.To((datatypeAware, resourceData) =>
				    	{
				    		if (resourceData != null)
				    		{
				    			if (!string.IsNullOrEmpty(resourceData.Datatype))
				    			{
									// TODO What if the data type is #anyType? see http://www.isotopicmaps.org/sam/sam-xtm/#sect-xml-canonicalization
									if (resourceData.Datatype == "http://www.w3.org/2001/XMLSchema#anyType")
									{
										throw new NotSupportedException("Constructs with resources of data type = #anyType are not supported yet.");
									}

									datatypeAware.SetValue(
										resourceData.Text, 
										LocatorFromDTO.Create(datatypeAware.TopicMap, resourceData.Datatype));
				    			}
				    			else
				    			{
				    				datatypeAware.Value = resourceData.Text;
				    			}
				    		}
				    	});

			From(dto => dto.ResourceReference)
				.To((datatypeAware, resourceReference) =>
				    	{
				    		if (resourceReference != null)
				    		{
				    			datatypeAware.LocatorValue = 
									LocatorFromDTO.Create(datatypeAware.TopicMap, resourceReference);
				    		}
				    	});
		}
	}
}