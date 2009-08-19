//------------------------------------------------------------------------------------------------- 
// <copyright file="AssociationDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the AssociationDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	/// <summary>
	/// The <c>association</c> element type represents associations. 
	/// The <see cref="RoleDTO"/> child elements provide the association roles of the association.
	/// </summary>
	[System.Serializable]
	[XmlType("association", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("association", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class AssociationDTO : ConstructDTO, IReifiableDTO, IScopedDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AssociationDTO"/> class.
		/// </summary>
		public AssociationDTO()
		{
			Roles = new List<RoleDTO>();
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
		/// Gets or sets the list of <see cref="RoleDTO"/> objects.
		/// </summary>
		/// <value>The list of <see cref="RoleDTO"/> objects.</value>
		[XmlElement("role")]
		public List<RoleDTO> Roles
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
		/// Gets or sets the type of this association.
		/// </summary>
		/// <value>The type of this association.</value>
		[XmlElement("type")]
		public TypeDTO Type
		{
			get;
			set;
		}
	}
}