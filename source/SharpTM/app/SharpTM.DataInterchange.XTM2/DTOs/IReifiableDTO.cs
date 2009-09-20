// <copyright file="IReifiableDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs
{
	public interface IReifiableDTO
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
		string Reifier { get; set; }
	}
}