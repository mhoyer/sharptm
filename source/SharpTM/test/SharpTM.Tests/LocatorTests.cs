// <copyright file="LocatorTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Tests
{
	public class When_comparing_locators_with_different_anchors_using_operator : With_TopicMapSystem
	{
		static ILocator fooLocator;
		static ILocator barLocator;
		static bool isEqual;

		Given a_foo_locator = () => fooLocator = topicMapSystem.CreateLocator("http://sharptm.de#foo");
		Given a_bar_locator = () => barLocator = topicMapSystem.CreateLocator("http://sharptm.de#bar");

		Because of_comapring = () => isEqual = fooLocator == barLocator;

		It should_not_be_equal = () => isEqual.ShouldBeFalse();
	}

	public class When_comparing_locators_with_different_anchors_using_equals : With_TopicMapSystem
	{
		static ILocator fooLocator;
		static ILocator barLocator;
		static bool isEqual;

		Given a_foo_locator = () => fooLocator = topicMapSystem.CreateLocator("http://sharptm.de#foo");
		Given a_bar_locator = () => barLocator = topicMapSystem.CreateLocator("http://sharptm.de#bar");

		Because of_comapring = () => isEqual = fooLocator.Equals(barLocator);

		It should_not_be_equal = () => isEqual.ShouldBeFalse();
	}
}