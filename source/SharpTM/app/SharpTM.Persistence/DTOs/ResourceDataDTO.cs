//------------------------------------------------------------------------------------------------- 
// <copyright file="ResourceDataDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the ResourceDataDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	/// <summary>
	/// The <c>resourceData</c> element type represents an information resource in 
	/// the form of content contained within the XTM document. This information 
	/// resource may be either a variant name or an occurrence, and it can have 
	/// a <c>datatype</c>. 
	/// </summary>
	[System.Serializable]
	[XmlType(TypeName = "any-markup", Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("resourceData", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class ResourceDataDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceDataDTO"/> class.
		/// </summary>
		public ResourceDataDTO()
		{
			Texts = new List<string>();
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text of this resource.</value>
		[XmlText]
		public List<string> Texts
		{
			get;
			set;
		}
	}
}