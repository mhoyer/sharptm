// <copyright file="IScopedDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	public interface IScopedDTO
	{
		/// <summary>
		/// Gets or sets the scope.
		/// </summary>
		/// <value>The scope.</value>
		[XmlElement("scope")]
		ScopeDTO Scope { get; set; }
	}
}