//------------------------------------------------------------------------------------------------- 
// <copyright file="ConstructDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the ConstructDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	/// <summary>
	/// Implements a base class for all topic map elements.
	/// </summary>
	public abstract class ConstructDTO : IConstructDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConstructDTO"/> class.
		/// </summary>
		public ConstructDTO()
		{
			ItemIdentities = new List<LocatorDTO>();
		}

		/// <summary>
		/// Gets or sets the list of <see cref="LocatorDTO"/> objects.
		/// </summary>
		/// <remarks>
		/// The <c>itemIdentity</c> element is used to assign an item identifier 
		/// to the topic map construct represented by its parent element.
		/// </remarks>
		/// <value>The list of <see cref="LocatorDTO"/> objects.</value>
		[XmlElement("itemIdentity")]
		public List<LocatorDTO> ItemIdentities
		{
			get;
			set;
		}
	}
}