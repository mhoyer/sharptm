// <copyright file="TopicMapToDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.ToDTO
{
	/// <summary>
	/// Converts a <see cref="ITopicMap"/> to a <see cref="TopicMapDTO"/>.
	/// <see cref="http://www.isotopicmaps.org/sam/sam-xtm/#d0e503"/>
	/// </summary>
	public class TopicMapToDTO : ClassMapper<ITopicMap, TopicMapDTO>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TopicMapToDTO"/> class.
		/// </summary>
		public TopicMapToDTO()
		{
			From(tm => tm.ItemIdentifiers)
				.To((xtm, itemIdentifiers) =>
				    	{
				    		foreach (ILocator locator in itemIdentifiers)
				    		{
				    			xtm.ItemIdentities.Add(LocatorToDTO.Create(locator));
				    		}
				    	});

			From(tm => tm.Associations)
				.To((xtm, associations) =>
				    	{
				    		foreach (IAssociation association in associations)
				    		{
				    			xtm.Associations.Add(AssociationToDTO.Create(association));
				    		}
				    	});

			From(tm => tm.Reifier)
				.To((xtm, reifier) =>
				    	{
				    		if (reifier != null)
				    		{
				    			xtm.Reifier = reifier.ItemIdentifiers[0].Reference;
				    		}
				    	});

			From(tm => tm.Topics)
				.To((xtm, topics) =>
				    	{
				    		foreach (ITopic topic in topics)
				    		{
				    			xtm.Topics.Add(TopicToDTO.Create(topic));
				    		}
				    	});
		}
	}
}