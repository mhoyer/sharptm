//------------------------------------------------------------------------------------------------- 
// <copyright file="TopicMapDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the TopicMapDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	/// <summary>
	/// The <c>topicMap</c> element type is the document element of all XTM documents. 
	/// It acts as a container for the topic map, and can be used to reify it, 
	/// but has no further significance.
	/// </summary>
#if !SILVERLIGHT
	[System.Serializable]
#endif
	[XmlType("topicMap", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("topicMap", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class TopicMapDTO : ConstructDTO, IReifiableDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMapDTO"/> class.
		/// </summary>
		public TopicMapDTO()
		{
			Associations = new List<AssociationDTO>();
			MergeMaps = new List<LocatorDTO>();
			Topics = new List<TopicDTO>();
			Version = "2.0";
		}

		/// <summary>
		/// Gets or sets the list of <see cref="AssociationDTO"/> objects.
		/// </summary>
		/// <value>The list of <see cref="AssociationDTO"/> objects.</value>
		[XmlElement("association", typeof(AssociationDTO))]
		public List<AssociationDTO> Associations { get; set; }

		/// <summary>
		/// Gets or sets the list of <see cref="MergeMap"/> objects.
		/// </summary>
		/// <remarks>
		/// The <c>mergeMap</c> element type refers to an external XTM document that is to be 
		/// merged into the topic map that contains the <see cref="MergeMap"/> element.
		/// </remarks>
		/// <value>The list of <see cref="MergeMap"/> objects.</value>
		[XmlElement("mergeMap")]
		public List<LocatorDTO> MergeMaps { get; set; }

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
		/// Gets or sets the list of topics.
		/// </summary>
		/// <value>The list of topics.</value>
		[XmlElement("topic", typeof(TopicDTO))]
		public List<TopicDTO> Topics { get; set; }

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <remarks>
		/// The version attribute is used to specify which version of XTM 
		/// the document conforms to. For XTM 2.0 documents this shall be "2.0". 
		/// </remarks>
		/// <value>The XTM version.</value>
		[XmlAttribute("version")]
		public string Version { get; set; }
	}
}