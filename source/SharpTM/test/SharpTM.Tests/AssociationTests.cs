// <copyright file="AssociationTests.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using Pixelplastic.TopicMaps.SharpTM.Core;
using Pixelplastic.TopicMaps.SharpTM.Core.DTOs;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Tests
{
	public class When_creating_an_association_with_empty_DTO : With_TopicMap
	{
		static AssociationData _assocData;
		static Exception exception;

		Given context = () => _assocData = new AssociationData();

		Because of_creating_the_association = () => exception = Catch.Exception(() => Association.Load(_assocData, topicMap));

		It should_throw_a_model_constraint_exception = () => exception.ShouldBeType<ModelConstraintException>();
	}

	public class When_creating_an_association_with_filled_DTO : With_TopicMap
	{
		static ITopic associationType;
		static AssociationData _assocData;
		static Association association;

		Given an_association_type = () => associationType = topicMap.CreateTopic();
		Given the_association_DTO = () => _assocData = new AssociationData { Type = associationType };

		Because of_creating_the_association = () => association = Association.Load(_assocData, topicMap);

		It should_initialize_the_association_type = () => association.Type.ShouldEqual(associationType);
	}

}