// <copyright file="TopicMapRepository.cs" company="Pixelplastic">
// Copyright (©) Marcel Hoyer 2008. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM.Tests
{
	public class When_instanciating_an_XTM_repository_with_illegal_path : BDDTest
	{
		static string path;
		static Exception exception;

		Given an_illegal_path = () => path = @"C:\*";
		
		Because of_instantiating_an_XTM_repository = 
			() => exception = Catch
				.Exception(() => new TopicMapRepository(path));

		It should_throw_an_argument_exception =
			() => exception.ShouldBeType<ArgumentException>();
	}

	public class When_instanciating_an_XTM_repository_with_null : BDDTest
	{
		static TopicMapRepository tmsr;

		Because of_instantiating_an_XTM_repository =
			() => tmsr = new TopicMapRepository(null);

		It should_use_the_default_storage_path =
			() => tmsr.StoragePath.ShouldEqual(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
	}

	public class When_instanciating_an_XTM_repository_with_an_empty_string : BDDTest
	{
		static TopicMapRepository tmsr;

		Because of_instantiating_an_XTM_repository =
			() => tmsr = new TopicMapRepository(string.Empty);

		It should_use_the_default_storage_path =
			() => tmsr.StoragePath.ShouldEqual(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
	}

	public class When_instanciating_an_XTM_repository_without_argument : BDDTest
	{
		static TopicMapRepository tmsr;

		Because of_instantiating_an_XTM_repository =
			() => tmsr = new TopicMapRepository();

		It should_use_the_default_storage_path =
			() => tmsr.StoragePath.ShouldEqual(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

	}

	public class When_instanciating_an_XTM_repository_with_relative_path : BDDTest
	{
		static TopicMapRepository tmsr;

		Because of_instantiating_an_XTM_repository_with_wild_card = () => tmsr = new TopicMapRepository(".");

		It should_initialize_an_absolut_storage_path = 
			() => tmsr.StoragePath.ShouldEqual(Environment.CurrentDirectory);

	}

	public class When_loading_a_topic_map_system_with_null_id : With_Topic_Map_Repository
	{
		static object id;
		static Exception exception;

		Given an_illegal_id = () => id = null;
		Because of_loading = () => exception = Catch.Exception(() => tmr.Load(id));
		It should_throw_an_exception = () => exception.ShouldBeType<ArgumentNullException>();
	}

}