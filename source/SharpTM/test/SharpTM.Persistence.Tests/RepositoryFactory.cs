// <copyright file="RepositoryFactory.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public class When_trying_to_get_an_topic_map_repository_instance : BDDTest
	{
		static IRepository<TopicMapDTO> instance;

		Because of_trying_to_get_the_instance = () => instance = RepositoryFactory<TopicMapDTO>.GetRepositoryInstance();

		It should_return_an_instance = () => instance.ShouldNotBeNull();
	}
}