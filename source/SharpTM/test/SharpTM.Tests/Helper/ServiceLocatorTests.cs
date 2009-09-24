// <copyright file="ServiceLocator.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using Microsoft.Practices.ServiceLocation;
using Pixelplastic.TopicMaps.SharpTM.Helper;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Tests.Helper
{
	public class When_using_the_service_locator : BDDTest
	{
		static TopicMapEntity instance;
		static SimpleServiceLocator ssl;

		Given an_instance = () => instance = new TopicMapEntity();
		Given a_simple_service_locator = () => ssl = new SimpleServiceLocator();
		Given the_service_locator = () => ServiceLocator.SetLocatorProvider(() => ssl);

		Because of_registering_the_instance = () =>  ssl.Register(instance);

		It should_provide_the_instance = () => ServiceLocator.Current.GetInstance<TopicMapEntity>().ShouldEqual(instance);
	}
}