// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopicMapSystemTest.cs">
//  TMAPI.Net was created collectively by the membership of the tmapinet-discuss mailing list 
//  (https://lists.sourceforge.net/lists/listinfo/tmapinet-discuss) with support by the 
//  tmapi-discuss mailing list (http://lists.sourceforge.net/mailman/listinfo/tmapi-discuss),
//  and is hereby released into the public domain; and comes with NO WARRANTY.
//  
//  No one owns TMAPI.Net: you may use it freely in both commercial and
//  non-commercial applications, bundle it with your software
//  distribution, include it on a CD-ROM, list the source code in a
//  book, mirror the documentation at your own web site, or use it in
//  any other way you see fit.
// </copyright>
// <summary>
//   Defines the TopicMapSystemTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Net.Core;
    using Xunit;
    using Xunit.Extensions;

    public class TopicMapSystemTest
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        public static readonly string TestTM2 = "mem://localhost/testm2";
        public static readonly string TestTM3 = "mem://localhost/testm3";
        public static readonly string TestTM4 = "mem://localhost/testm4";
        #endregion

        #region Constructor (used for preparations before tests)

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicMapSystemTest"/> class.
        /// </summary>
        public TopicMapSystemTest()
        {
        }

        #endregion

        #region Tests
        [Fact]
        public void Locators_GetEmptyListOfLocatorsAtBeginning()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();
            
            Assert.Equal(0, system.Locators.Count);
        }

        [Fact]
        public void CreateLocator_TestAnything()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var locator = system.CreateLocator(TestTM1);
            Assert.Equal(TestTM1, locator.Reference);
            Assert.NotEqual(TestTM2, locator.Reference);
        }

        [Fact]
        public void CreateTopicMap_OneTMByString()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            ITopicMap map1 = system.CreateTopicMap(TestTM1);
            Assert.NotNull(map1);
        }

        [Fact]
        public void CreateTopicMap_OneAndCheckLocatorsCount()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            ITopicMap map1 = system.CreateTopicMap(TestTM1);
            Assert.NotNull(map1);
            Assert.Equal(1, system.Locators.Count);
        }

        [Fact]
        public void CreateTopicMap_TwoAndCheckLocatorsCount()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            ITopicMap map1 = system.CreateTopicMap(TestTM1);
            ITopicMap map2 = system.CreateTopicMap(TestTM2);
            Assert.NotNull(map1);
            Assert.NotNull(map2);
            Assert.Equal(2, system.Locators.Count);
        }

        [Fact]
        public void CreateTopicMap_TwoBySameIri()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            system.CreateTopicMap(TestTM1);
            Assert.Throws<TopicMapExistsException>( () => 
                                                    system.CreateTopicMap(TestTM1)
                );      
        }


        [Fact]
        public void CreateTopicMap_TwoBySameLocator()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var locator = system.CreateLocator(TestTM1);
            system.CreateTopicMap(locator);
            Assert.Throws<TopicMapExistsException>(() =>
                                                   system.CreateTopicMap(locator)
                );
        }

        [Fact]
        public void CreateTopicMap_TwoIdenticalByIriAndLocator()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var locator = system.CreateLocator(TestTM1);
            system.CreateTopicMap(locator);
            Assert.Throws<TopicMapExistsException>(() =>
                                                   system.CreateTopicMap(TestTM1)
                );
        }


        [Fact]
        public void CreateTopicMap_TwoByTwoInstancesOfSameLocator()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var locator1 = system.CreateLocator(TestTM1);
            var locator2 = system.CreateLocator(TestTM1);
            system.CreateTopicMap(locator1);
            Assert.Throws<TopicMapExistsException>(() =>
                                                   system.CreateTopicMap(locator2)
                );
        }

        [Fact]
        public void CreateTopicMap_ByILocator()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var locator = system.CreateLocator(TestTM1);
            Assert.Equal(TestTM1, locator.Reference);
            system.CreateTopicMap(locator);
            var map1 = system.GetTopicMap(TestTM1);
            var map2 = system.GetTopicMap(locator);
            Assert.NotNull(map1);
            Assert.NotNull(map2);
            Assert.Equal(map1, map2);
        }

        [Fact]
        public void GetTopicMapByIri()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var tm1 = system.CreateTopicMap(TestTM1);
            var tm2 = system.GetTopicMap(TestTM1);
            Assert.Equal(tm1,tm2);
        }

        [Fact]
        public void GetTopicMapByILocator()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var locator = system.CreateLocator(TestTM1);
            var tm1 = system.CreateTopicMap(locator);
            var tm2 = system.GetTopicMap(TestTM1);
            Assert.Equal(tm1, tm2);
        }

        #region TMAPI test cases
        [Fact]
        public void GetTopicMap()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var locator = system.CreateLocator(TestTM1);
            system.CreateTopicMap(locator);
            var topicMap = system.GetTopicMap(locator);

            Assert.NotNull(topicMap);
            Assert.NotNull(topicMap.Id);
        }

        [Fact]
        public void CreateTopicMap_UsingExistingLocatorThrowsException()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var locator = system.CreateLocator(TestTM1);
            system.CreateTopicMap(locator);

            Assert.Throws<TopicMapExistsException>("A topic map under the same IRI exists already.", () => system.CreateTopicMap(locator));
        }

        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        public void CreateTopicMapSet(string tmURI)
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var topicMap = system.CreateTopicMap("http://www.example.org/" + tmURI);

            Assert.NotNull(topicMap);
        }

        [Fact]
        public void RemoveTopicMap()
        {
            var tmf = TMAPITestCase.NewTopicMapSystemFactoryInstance();
            var system = tmf.NewTopicMapSystem();

            var topicMap1 = system.CreateTopicMap("http://www.example.org/test1");
            var topicMap2 = system.CreateTopicMap("http://www.example.org/test2");
            var topicMap3 = system.CreateTopicMap("http://www.example.org/test3");

            Assert.NotNull(topicMap1);
            Assert.NotNull(topicMap2);
            Assert.NotNull(topicMap3);

            var TMCount = system.Locators.Count;

            topicMap3.Remove();

            Assert.Equal(TMCount - 1, system.Locators.Count);
        }
        #endregion
        #endregion
    }
}