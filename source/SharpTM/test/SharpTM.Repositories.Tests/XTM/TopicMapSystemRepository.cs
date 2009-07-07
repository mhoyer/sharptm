// <copyright file="TopicMapSystemRepository.cs" company="Pixelplastic">
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
				.Exception(() => new TopicMapSystemRepository(path));

		It should_throw_an_argument_exception =
			() => exception.ShouldBeType<ArgumentException>();
	}

	public class When_instanciating_an_XTM_repository_with_null : BDDTest
	{
		static TopicMapSystemRepository tmsr;

		Because of_instantiating_an_XTM_repository =
			() => tmsr = new TopicMapSystemRepository(null);

		It should_use_the_default_storage_path =
			() => tmsr.StoragePath.ShouldEqual(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
	}

	public class When_instanciating_an_XTM_repository_with_an_empty_string : BDDTest
	{
		static TopicMapSystemRepository tmsr;

		Because of_instantiating_an_XTM_repository =
			() => tmsr = new TopicMapSystemRepository(string.Empty);

		It should_use_the_default_storage_path =
			() => tmsr.StoragePath.ShouldEqual(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
	}

	public class When_instanciating_an_XTM_repository_without_argument : BDDTest
	{
		static TopicMapSystemRepository tmsr;

		Because of_instantiating_an_XTM_repository =
			() => tmsr = new TopicMapSystemRepository();

		It should_use_the_default_storage_path =
			() => tmsr.StoragePath.ShouldEqual(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

	}

	public class When_instanciating_an_XTM_repository_with_relative_path : BDDTest
	{
		static TopicMapSystemRepository tmsr;

		Because of_instantiating_an_XTM_repository_with_wild_card = () => tmsr = new TopicMapSystemRepository(".");

		It should_initialize_an_absolut_storage_path = 
			() => tmsr.StoragePath.ShouldEqual(Environment.CurrentDirectory);

	}
}