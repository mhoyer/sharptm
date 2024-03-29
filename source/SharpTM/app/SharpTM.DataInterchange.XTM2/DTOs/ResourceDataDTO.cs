//------------------------------------------------------------------------------------------------- 
// <copyright file="ResourceDataDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the ResourceDataDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	/// <summary>
	/// The <c>resourceData</c> element type represents an information resource in 
	/// the form of content contained within the XTM document. This information 
	/// resource may be either a variant name or an occurrence, and it can have 
	/// a <c>datatype</c>. 
	/// </summary>
#if !SILVERLIGHT
	[System.Serializable]
#endif
	[XmlType(TypeName = "any-markup", Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("resourceData", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class ResourceDataDTO
	{
		/// <summary>
		/// Gets or sets the datatype.
		/// </summary>
		/// <value>The datatype.</value>
		[XmlAttribute("datatype", DataType = "anyURI")]
		public string Datatype { get; set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text of this resource.</value>
		[XmlText]
		public string Text { get; set; }
	}
}