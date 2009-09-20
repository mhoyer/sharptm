//------------------------------------------------------------------------------------------------- 
// <copyright file="TopicDTO.cs" company="Pixelplastic">
// Copyright (c) Marcel Hoyer 2009.  All rights reserved.
// </copyright>
// <summary>Defines the TopicDTO type.</summary>
//-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	/// <summary>
	/// The <c>topic</c> element type is used to represent topics, and acts 
	/// as a container and point of reference for topic information. 
	/// The child elements of the topic element provide identification 
	/// as well as names and occurrences, while association roles 
	/// played by the topic are specified outside the topic element. 
	/// </summary>
#if !SILVERLIGHT
	[System.Serializable]
#endif

	[XmlType("topic", AnonymousType = true, Namespace = "http://www.topicmaps.org/xtm/")]
	[XmlRoot("topic", Namespace = "http://www.topicmaps.org/xtm/", IsNullable = false)]
	public class TopicDTO : ConstructDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TopicDTO"/> class.
		/// </summary>
		public TopicDTO()
		{
			SubjectLocators = new List<LocatorDTO>();
			SubjectIdentifiers = new List<LocatorDTO>();
			Occurrences = new List<OccurrenceDTO>();
			Names = new List<NameDTO>();
		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <remarks>
		/// The id attribute provides a unique identifier within 
		/// the document for the topic, which is used to refer to it. 
		/// </remarks>
		/// <value>The id of this topic.</value>
		[XmlAttribute("id", DataType = "ID")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="InstanceOf"/> object.
		/// </summary>
		/// <value>The <see cref="InstanceOf"/> object.</value>
		[XmlElement("instanceOf")]
		public InstanceOfDTO InstanceOf { get; set; }

		/// <summary>
		/// Gets or sets the list of <see cref="NameDTO"/> objects.
		/// </summary>
		/// <value>The list of <see cref="NameDTO"/> objects.</value>
		[XmlElement("name", typeof(NameDTO))]
		public List<NameDTO> Names { get; set; }

		/// <summary>
		/// Gets or sets the list of <see cref="OccurrenceDTO"/> objects.
		/// </summary>
		/// <value>The list of <see cref="OccurrenceDTO"/> objects.</value>
		[XmlElement("occurrence", typeof(OccurrenceDTO))]
		public List<OccurrenceDTO> Occurrences { get; set; }

		/// <summary>
		/// Gets or sets the list of subject identifiers.
		/// </summary>
		/// <remarks>
		/// The <c>subjectIdentifier</c> element is used to assign a subject identifier 
		/// to the topic that is represented by its parent topic element. 
		/// </remarks>
		/// <value>The list of subject identifiers.</value>
		[XmlElement("subjectIdentifier", typeof(LocatorDTO))]
		public List<LocatorDTO> SubjectIdentifiers { get; set; }

		/// <summary>
		/// Gets or sets the list of subject locators.
		/// </summary>
		/// <remarks>
		/// The <c>subjectLocator</c> element is used to assign a subject locator 
		/// to the topic that is represented by its parent topic element.
		/// </remarks>
		/// <value>The list of subject locators.</value>
		[XmlElement("subjectLocator", typeof(LocatorDTO))]
		public List<LocatorDTO> SubjectLocators { get; set; }
	}
}