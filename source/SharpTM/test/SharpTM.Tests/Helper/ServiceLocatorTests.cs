// <copyright file="ServiceLocator.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Pixelplastic.TopicMaps.SharpTM.Helper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Tests.Helper
{
	public class When_using_the_service_locator : BDDTest
	{
		static TopicMapEntity instance;

		Given an_instance = () => instance = new TopicMapEntity();

		Because of_registering_the_instance = () => ServiceLocator.Register(instance);

		It should_provide_the_instance = () => ServiceLocator.GetInstance<TopicMapEntity>().ShouldEqual(instance);
	}
}