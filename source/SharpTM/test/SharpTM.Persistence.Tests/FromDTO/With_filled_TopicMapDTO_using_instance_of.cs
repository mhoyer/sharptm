// <copyright file="With_filled_TopicMapDTO_using_instance_of.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.FromDTO
{
	public abstract class With_filled_TopicMapDTO_using_instance_of : With_filled_TopicMapDTO
	{
		Given the_instance_of_relations =
			() =>
				{
					marcelHoyer.InstanceOf.TopicReferences.Add(personType.TopicReference);
					lutzMaicher.InstanceOf.TopicReferences.Add(personType.TopicReference);
					sharpTM.InstanceOf.TopicReferences.Add(projectType.TopicReference);
				};
	}
}