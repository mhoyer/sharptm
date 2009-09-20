//------------------------------------------------------------------------------------------------- 
// <copyright file="InstanceOfDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the InstanceOfDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	/// <summary>
	/// The <c>instanceOf</c> element type is used to assign one or more types 
	/// to the topic represented by its parent element. The types are 
	/// always topics, indicated by the <c>instanceOf</c> element's child elements.
	/// </summary>
#if !SILVERLIGHT
	[System.Serializable]
#endif
	[XmlType("instanceOf", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("instanceOf", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class InstanceOfDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InstanceOfDTO"/> class.
		/// </summary>
		public InstanceOfDTO()
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
		public List<LocatorDTO> TopicReferences { get; set; }
	}
}