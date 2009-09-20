//------------------------------------------------------------------------------------------------- 
// <copyright file="TypeDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the TypeDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	/// <summary>
	/// The <c>type</c> element type is used to assign a type to the topic map construct 
	/// represented by its parent element. The type is always a topic, indicated 
	/// by the type element's child element. 
	/// </summary>
#if !SILVERLIGHT
	[System.Serializable]
#endif
	[XmlType("type", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("type", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class TypeDTO
	{
		public TypeDTO()
		{
		}

		public TypeDTO(string typeLocator)
		{
			TopicReference = new LocatorDTO { HRef = typeLocator };
		}

		/// <summary>
		/// Gets or sets the topic reference of this type.
		/// </summary>
		/// <remarks>
		/// The <c>topicRef</c> element type refers to a topic, either within the same XML 
		/// document or externally. The significance of the topic reference depends 
		/// on the context.
		/// </remarks>
		/// <value>The topic reference.</value>
		[XmlElement("topicRef")]
		public LocatorDTO TopicReference { get; set; }
	}
}