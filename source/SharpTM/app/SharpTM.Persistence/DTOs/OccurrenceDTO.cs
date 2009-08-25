//------------------------------------------------------------------------------------------------- 
// <copyright file="OccurrenceDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the OccurrenceDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	/// <summary>
	/// The <c>occurrence</c> element type is used to assign an 
	/// occurrence to the topic defined by the parent element.
	/// </summary>
#if !SILVERLIGHT
	[System.Serializable]
#endif
	[System.Diagnostics.DebuggerStepThrough]
	[XmlType("occurrence", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("occurrence", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class OccurrenceDTO : ConstructDTO, IReifiableDTO, IDatatypeAwareDTO, IScopedDTO
	{
		/// <summary>
		/// Gets or sets the reifier.
		/// </summary>
		/// /// <remarks>
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

		/// <summary>
		/// Gets or sets the type of this occurrence.
		/// </summary>
		/// <value>The type of this occurrence.</value>
		[XmlElement("type")]
		public TypeDTO Type
		{
			get;
			set;
		}
	}
}