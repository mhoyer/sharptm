// <copyright file="IConstructDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	public interface IConstructDTO
	{
		/// <summary>
		/// Gets or sets the list of <see cref="LocatorDTO"/> objects.
		/// </summary>
		/// <remarks>
		/// The <c>itemIdentity</c> element is used to assign an item identifier 
		/// to the topic map construct represented by its parent element.
		/// </remarks>
		/// <value>The list of <see cref="LocatorDTO"/> objects.</value>
		[XmlElement("itemIdentity")]
		List<LocatorDTO> ItemIdentities { get; set; }
	}
}