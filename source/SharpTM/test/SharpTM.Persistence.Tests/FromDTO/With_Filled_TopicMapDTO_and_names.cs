// <copyright file="With_filled_TopicMapDTO_and_names.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.FromDTO
{
	public abstract class With_filled_TopicMapDTO_and_names : With_filled_TopicMapDTO
	{
		protected static NameDTO marcelHoyerName;

		Given The_name_for_Marcel_Hoyer =
			() =>
				{
					marcelHoyerName = TestHelper.CreateName("Marcel Hoyer");
					marcelHoyer.Names.Add(marcelHoyerName);
				};

	}
}