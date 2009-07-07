// <copyright file="VariantToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO
{
	public class VariantToDTO : ConstructToDTO<IVariant, VariantDTO>
	{
		public VariantToDTO()
		{
			From(variant => variant)
				.To((dto, variant) =>
				    	{
							new ScopeToDTO().Map(variant, dto);
							new ReifiableToDTO().Map(variant, dto);

				    		if(variant.Datatype.Reference == "http://www.w3.org/2001/XMLSchema#anyURI")
				    		{
								dto.ResourceReference = new LocatorDTO();
				    			dto.ResourceReference.HRef = variant.LocatorValue.Reference;
				    		}
							else
				    		{
								dto.ResourceData = new ResourceDataDTO();
								dto.ResourceData.Text = variant.Value;
							}
				    	});
		}

		public static VariantDTO Create(IVariant variant)
		{
			return new VariantToDTO().Map(variant, new VariantDTO());
		}
	}
}