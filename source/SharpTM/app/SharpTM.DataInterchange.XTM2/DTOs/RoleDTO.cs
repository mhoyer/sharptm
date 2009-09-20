//------------------------------------------------------------------------------------------------- 
// <copyright file="RoleDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the RoleDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	/// <summary>
	/// The <c>role</c> element type is used to assign an association role 
	/// to the association created by the association parent element.
	/// </summary>
#if !SILVERLIGHT
	[System.Serializable]
#endif
	[XmlType("role", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("role", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class RoleDTO : ConstructDTO, IReifiableDTO
	{
		/// <summary>
		/// Gets or sets the reifier.
		/// </summary>
		/// <remarks>
		/// The <c>reifier</c> attribute is used to refer from the topic map construct 
		/// on which it appears to the topic reifying that construct. The 
		/// reference is an IRI matching one of the topic's item identifiers. 
		/// </remarks>
		/// <value>The reifier.</value>
		[XmlAttribute("reifier", DataType = "anyURI")]
		public string Reifier { get; set; }

		/// <summary>
		/// Gets or sets the topic reference.
		/// </summary>
		/// <remarks>
		/// The <c>topicRef</c> element type refers to a topic, either within the same XML 
		/// document or externally. The significance of the topic reference depends 
		/// on the context.
		/// </remarks>
		/// <value>The topic reference.</value>
		[XmlElement("topicRef")]
		public LocatorDTO TopicReference { get; set; }

		/// <summary>
		/// Gets or sets the type of this role.
		/// </summary>
		/// <value>The type of this role.</value>
		[XmlElement("type")]
		public TypeDTO Type { get; set; }
	}
}