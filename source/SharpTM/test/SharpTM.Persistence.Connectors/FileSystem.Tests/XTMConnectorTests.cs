// <copyright file="TopicMapRepository.cs" company="Pixelplastic">
// Copyright (©) Marcel Hoyer 2008. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Reflection;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit;
using Xunit.BDDExtension;
using Xunit.Extensions.AssertExtensions;

namespace Pixelplastic.TopicMaps.SharpTM.Repositories.XTM.Tests
{
	public class When_loading_the_music_xtm : With_XTMConnector
	{
		static string xtm;
		static TopicMapDTO topicMapDTO;

		Given an_XMT_file = () => xtm = typeof(When_loading_the_music_xtm).Namespace + ".music.xtm";

		Because of_loading_the_topic_map_DTO = 
			() => topicMapDTO = cnx.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(xtm));

		It should_create_a_DTO = () => topicMapDTO.ShouldBeType<TopicMapDTO>();
		It should_have_read_correct_number_of_associations =
			() => topicMapDTO.Associations.Count.ShouldEqual(6);
		It should_have_read_correct_number_of_topics =
			() => topicMapDTO.Topics.Count.ShouldEqual(46);

		It should_have_read_correct_number_of_names =
			() =>
			{
				int namesCount = 0;
				foreach (var topic in topicMapDTO.Topics)
				{
					namesCount += topic.Names.Count;
				}

				namesCount.ShouldEqual(54);
			};
	}

	public class When_loading_the_opera_xtm : With_XTMConnector
	{
		static string xtm;
		static TopicMapDTO topicMapDTO;

		Given an_XMT_file = () => xtm = typeof(When_loading_the_opera_xtm).Namespace + ".opera.xtm";

		Because of_mapping_the_topic_map_DTO = () => topicMapDTO = cnx.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(xtm));

		It should_create_a_DTO = () => topicMapDTO.ShouldBeType<TopicMapDTO>();

		[Fact(Skip = "Needs integration test using xpath.")]
		public new void Run()
		{ }
		//It should_have_read_correct_number_of_associations =
		//    () => topicMapDTO.Associations.Count.ShouldEqual(840);
		//It should_have_read_correct_number_of_topics =
		//    () => topicMapDTO.Topics.Count.ShouldEqual(46);

		//It should_have_read_correct_number_of_names =
		//    () =>
		//    {
		//        int namesCount = 0;
		//        foreach (var topic in topicMapDTO.Topics)
		//        {
		//            namesCount += topic.Names.Count;
		//        }

		//        namesCount.ShouldEqual(54);
		//    };
	}

	public class When_loading_an_XTM_with_illegal_path : With_XTMConnector
	{
		static string path;
		static Exception exception;

		Given an_illegal_path = () => path = @"C:\*";
		
		Because of_loading_the_XTM_file = 
			() => exception = Catch
				.Exception(() => cnx.Load(path));

		It should_throw_an_argument_exception =
			() => exception.ShouldBeType<ArgumentException>();
	}

	public class When_loading_an_XTM_with_null_path : With_XTMConnector
	{
		static Exception exception;

		Because of_loading_the_XTM_file =
			() => exception = Catch
				.Exception(() => cnx.Load((string)null));

		It should_throw_an_argument_exception =
			() => exception.ShouldBeType<ArgumentNullException>();
	}
}