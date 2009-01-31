// <copyright file="NameEqualsSpecs.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System.Collections.Generic;
using TMAPI.Net.Core;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Tests
{
	public class When_two_Name_instances_have_the_same_property_values_they_should_be_equal
		: TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static bool areEqual;
		static IName name1, name2;

		Given a_Name_instance =
			() => name1 = topic1.CreateName(type1, "NAME", scope1, scope2);
		Given another_Name_instance_with_the_same_properties =
			() => name2 = topic1.CreateName(type1, "NAME", scope1, scope2);

		Because of_the_comparison = () => areEqual = name1.Equals(name2);

		It should_equal = () => areEqual.ShouldBeTrue();
	}

	public class When_two_Name_instances_have_the_same_property_values_but_are_in_unconstrained_scope_they_should_be_equal
		: TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static bool areEqual;
		static IName name1, name2;

		Given a_Name_instance =
			() => name1 = topic1.CreateName(type1, "NAME", new List<ITopic>());
		Given another_Name_instance_with_the_same_properties =
			() => name2 = topic1.CreateName(type1, "NAME", new List<ITopic>());

		Because of_the_comparison = () => areEqual = name1.Equals(name2);

		It should_equal = () => areEqual.ShouldBeTrue();
	}

	public class When_two_Name_instances_have_a_different_value_they_should_NOT_be_equal
		: TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static bool areEqual;
		static IName name1, name2;

		Given a_Name_instance =
			() => name1 = topic1.CreateName(type1, "NAME", scope1, scope2);
		Given another_Name_instance_with_different_value =
			() => name2 = topic1.CreateName(type1, "DIFFERENT NAME", scope1, scope2);

		Because of_the_comparison = () => areEqual = name1.Equals(name2);

		It should_NOT_equal = () => areEqual.ShouldBeFalse();
	}

	public class When_two_Name_instances_have_a_different_type_they_should_NOT_be_equal
		: TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static bool areEqual;
		static IName name1, name2;

		Given a_Name_instance =
			() => name1 = topic1.CreateName(type1, "NAME", scope1, scope2);
		Given another_Name_instance_with_different_name_type =
			() => name2 = topic1.CreateName(type2, "NAME", scope1, scope2);

		Because of_the_comparison = () => areEqual = name1.Equals(name2);

		It should_NOT_equal = () => areEqual.ShouldBeFalse();
	}

	public class When_two_Name_instances_have_a_different_single_scope_they_should_NOT_be_equal
		: TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static bool areEqual;
		static IName name1, name2;

		Given a_Name_instance =
			() => name1 = topic1.CreateName(type1, "NAME", scope1);
		Given another_Name_instance_with_different_scope =
			() => name2 = topic1.CreateName(type1, "NAME", scope2);

		Because of_the_comparison = () => areEqual = name1.Equals(name2);

		It should_NOT_equal = () => areEqual.ShouldBeFalse();
	}

	public class When_two_Name_instances_have_a_different_multi_scope_they_should_NOT_be_equal
		: TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static bool areEqual;
		static IName name1, name2;

		Given a_Name_instance =
			() => name1 = topic1.CreateName(type1, "NAME", scope1);
		Given another_Name_instance_with_different_scopes =
			() => name2 = topic1.CreateName(type1, "NAME", scope1, scope2);

		Because of_the_comparison = () => areEqual = name1.Equals(name2);

		It should_NOT_equal = () => areEqual.ShouldBeFalse();
	}

	public class When_two_Name_instances_have_a_different_parent_they_should_NOT_be_equal
		: TopicMapContextSpecification_with_two_topics_types_and_scopes
	{
		static bool areEqual;
		static IName name1, name2;

		Given a_Name_instance =
			() => name1 = topic1.CreateName(type1, "NAME", scope1, scope2);
		Given another_Name_instance_with_different_parent_topic =
			() => name2 = topic2.CreateName(type1, "NAME", scope1, scope2);

		Because of_the_comparison = () => areEqual = name1.Equals(name2);

		It should_NOT_equal = () => areEqual.ShouldBeFalse();
	}
}