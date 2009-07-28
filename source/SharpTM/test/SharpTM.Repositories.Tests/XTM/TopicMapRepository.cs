// <copyright file="TopicMapRepository.cs" company="Pixelplastic">
// Copyright (©) Marcel Hoyer 2008. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM.Tests
{
	public class When_deserializing_the_music_xtm : BDDTest
	{
		static object result;
		static string xtm;

		Given an_XMT_file = () => xtm = typeof(When_deserializing_the_music_xtm).Namespace + ".music.xtm";
		Because of_deserialization = () =>
		                           	{
		                           		var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(xtm);
		                           		XmlSerializer xs = new XmlSerializer(typeof(TopicMapDTO));
		                           		result = xs.Deserialize(XmlReader.Create(stream));
		                           	};

		It should_create_a_DTO = () => result.ShouldBeType<TopicMapDTO>();
		It should_have_read_correct_number_of_associations = 
			() => ((TopicMapDTO) result).Associations.Count.ShouldEqual(6);
		It should_have_read_correct_number_of_topics =
			() => ((TopicMapDTO)result).Topics.Count.ShouldEqual(46);

		It should_have_read_correct_number_of_names =
			() =>
				{
					int namesCount = 0;
					foreach (var topic in ((TopicMapDTO)result).Topics)
					{
						namesCount += topic.Names.Count;
					}
					
					namesCount.ShouldEqual(54);
				};
	}

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

	public class When_loading_a_topic_map_system_with_valid_id: With_Topic_Map_Repository
	{
		[Fact(Skip = "Not implemented yet.")]
		public new void Run() { }


		//static string id;
		//static TopicMapDTO tmDTO;

		//Given a_legal_id = () => id = "http://pixelplastic.de/topicmaps/public";
		//Because of_loading = () => tmDTO = tmr.Load(id);
		//It should_load_the_topic_map_as_DTO = () => tmDTO.ShouldNotBeNull();
		//It should_load_the_correct_topic_map = () => tmDTO.ItemIdentities.Find(iid => iid.HRef == id).ShouldNotBeNull();
	}

}