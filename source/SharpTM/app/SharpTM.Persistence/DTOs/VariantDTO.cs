//------------------------------------------------------------------------------------------------- 
// <copyright file="VariantDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the VariantDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	/// <summary>
	/// The <c>variant</c> element type is used to add a variant name to a topic name.
	/// </summary>
	[System.Serializable]
	[XmlType("variant", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("variant", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class VariantDTO : ConstructDTO, IReifiableDTO, IScopedDTO, IDatatypeAwareDTO
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
		public string Reifier
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the resource data.
		/// </summary>
		/// <value>The resource data.</value>
		[XmlElement("resourceData", typeof(ResourceDataDTO))]
		public ResourceDataDTO ResourceData
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the resource reference.
		/// </summary>
		/// <remarks>
		/// The <c>topicRef</c> element type refers to a topic, either within the same XML 
		/// document or externally. The significance of the topic reference depends 
		/// on the context.
		/// </remarks>
		/// <value>The resource reference.</value>
		[XmlElement("resourceRef", typeof(LocatorDTO))]
		public LocatorDTO ResourceReference
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the scope.
		/// </summary>
		/// <value>The scope.</value>
		[XmlElement("scope")]
		public ScopeDTO Scope
		{
			get;
			set;
		}
	}
}