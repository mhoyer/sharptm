// <copyright file="IDatatypeAwareDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	public interface IDatatypeAwareDTO
	{
		/// <summary>
		/// Gets or sets the resource data.
		/// </summary>
		/// <value>The resource data.</value>
		ResourceDataDTO ResourceData { get; set; }

		/// <summary>
		/// Gets or sets the resource reference.
		/// </summary>
		/// <remarks>
		/// The <c>topicRef</c> element type refers to a topic, either within the same XML 
		/// document or externally. The significance of the topic reference depends 
		/// on the context.
		/// </remarks>
		/// <value>The resource reference.</value>
		LocatorDTO ResourceReference { get; set; }
	}
}