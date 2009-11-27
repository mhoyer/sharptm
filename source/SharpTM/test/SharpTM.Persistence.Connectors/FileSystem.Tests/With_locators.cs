// <copyright file="With_locators.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM.Tests
{
	public abstract class With_locators : BDDTest
	{
		protected static string baseLocator;
		protected static string fragment;
		protected static string relative;
		protected static string absolute;
		protected static LocatorDTO fragmentLocator;
		protected static LocatorDTO relativeLocator;
		protected static LocatorDTO absoluteLocator;

		Given a_base_locator = () => baseLocator = "http://sharptm.de/";
		Given a_fragment_locator =
			() =>
				{
					fragment = "#fragment";
					fragmentLocator = new LocatorDTO() { HRef = fragment };
				};
		Given a_relative_locator =
			() =>
				{
					relative = "relative";
					relativeLocator = new LocatorDTO() { HRef = relative };
				};
		Given an_absolute_locator =
			() =>
				{
					absolute = "http://sharptm.de/absolute";
					absoluteLocator = new LocatorDTO() { HRef = absolute };
				};
	}
}