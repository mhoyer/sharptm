//------------------------------------------------------------------------------------------------- 
// <copyright file="ScopeDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the ScopeDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	/// <summary>
	/// The scope element type is used to assign a scope to the statement represented by the parent element.
	/// </summary>
	[System.Serializable]
	[XmlType("scope", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("scope", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class ScopeDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ScopeDTO"/> class.
		/// </summary>
		public ScopeDTO()
		{
			TopicReferences = new List<LocatorDTO>();
		}

		/// <summary>
		/// Gets or sets the list of <see cref="LocatorDTO"/> objects.
		/// </summary>
		/// <remarks>
		/// The <c>topicRef</c> element type refers to a topic, either within the same XML 
		/// document or externally. The significance of the topic reference depends 
		/// on the context.
		/// </remarks>
		/// <value>The list of <see cref="LocatorDTO"/> objects.</value>
		[XmlElement("topicRef")]
		public List<LocatorDTO> TopicReferences
		{
			get;
			set;
		}
	}
}