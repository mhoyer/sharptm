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
	public class When_registering_an_instance_without_key : With_simple_service_locator
	{
		static TopicMapEntity instance;

		Given an_instance = () => instance = new TopicMapEntity();

		Because of_registering_the_instance = () =>  ssl.Register(instance);

		It should_provide_the_instance = () => ServiceLocator.Current.GetInstance<TopicMapEntity>().ShouldEqual(instance);
		It should_provide_the_instance_in_GetAllInstances =
			() => ServiceLocator.Current.GetAllInstances<TopicMapEntity>().ShouldContain(instance);
	}

	public class When_registering_an_instance_using_a_key : With_simple_service_locator
	{
		static TopicMapEntity instance;

		Given an_instance = () => instance = new TopicMapEntity();

		Because of_registering_the_instance = () => ssl.Register(instance, "foo");

		It should_provide_the_instance = () => ServiceLocator.Current.GetInstance<TopicMapEntity>("foo").ShouldEqual(instance);
		It should_provide_the_instance_in_GetAllInstances =
			() => ServiceLocator.Current.GetAllInstances<TopicMapEntity>().ShouldContain(instance);
	}

	public class With_simple_service_locator : BDDTest
	{
		public static SimpleServiceLocator ssl;

		Given a_simple_service_locator = () => ssl = new SimpleServiceLocator();
		Given the_service_locator = () => ServiceLocator.SetLocatorProvider(() => ssl);
	}
}