//------------------------------------------------------------------------------------------------- 
// <copyright file="NameDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the NameDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	/// <summary>
	/// The <c>name</c> element type is used to add topic names to the topic represented by 
	/// the parent topic element. The child elements of the name element provide the 
	/// property values of the topic name item. 
	/// </summary>
#if !SILVERLIGHT
	[System.Serializable]
#endif
	[XmlType("name", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("name", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class NameDTO : ConstructDTO, IReifiableDTO, IScopedDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NameDTO"/> class.
		/// </summary>
		public NameDTO()
		{
			Variants = new List<VariantDTO>();
		}

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
		/// Gets or sets the type of this name.
		/// </summary>
		/// <value>The type of this name.</value>
		[XmlElement("type")]
		public TypeDTO Type
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		[XmlElement("value")]
		public string Value
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the list of <see cref="VariantDTO"/> objects.
		/// </summary>
		/// <value>The list of <see cref="VariantDTO"/> objects.</value>
		[XmlElement("variant")]
		public List<VariantDTO> Variants
		{
			get;
			set;
		}
	}
}