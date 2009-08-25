//------------------------------------------------------------------------------------------------- 
// <copyright file="LocatorDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the ItemIdentity type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	/// <summary>
	/// Implements a base locator class to be used for referencing
	/// other constructs.
	/// </summary>
	[System.Serializable]
	public class LocatorDTO
	{
		/// <summary>
		/// Gets or sets the <c>href</c>.
		/// </summary>
		/// <remarks>
		/// The <c>href</c> attribute always references an information resource using a 
		/// relative or absolute IRI valid according to [IETF RFC 3986] and 
		/// [IETF RFC 3987], but the meaning of the reference depends on context. 
		/// </remarks>
		/// <value>The <c>href</c> IRI.</value>
		[XmlAttribute("href", DataType = "anyURI")]
		public string HRef
		{
			get;
			set;
		}
	}
}