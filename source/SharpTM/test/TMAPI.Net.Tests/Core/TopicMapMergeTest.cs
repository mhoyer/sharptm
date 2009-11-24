// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopicMapMergeTest.cs">
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
//   Defines the TopicMapMergeTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Xunit;

    public class TopicMapMergeTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        public static readonly string TestTM2 = "mem://localhost/testm2";
        #endregion

        #region Tests
        [Fact]
        public void MergeIn_MergeTopicsByEqualItemIdentifiers()
        {
            var topicMap1 = TopicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = TopicMapSystem.CreateTopicMap(TestTM2);
            var itemIdentifier = TopicMapSystem.CreateLocator("http://www.example.org/12345");
            var topic1 = topicMap1.CreateTopicByItemIdentifier(itemIdentifier);
            var topic2 = topicMap2.CreateTopicByItemIdentifier(itemIdentifier);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(1, topicMap2.Topics.Count);

            topicMap1.MergeIn(topicMap2);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(topic1, topicMap1.GetConstructByItemIdentifier(itemIdentifier));
        }

        [Fact]
        public void MergeIn_MergeTopicsByEqualSubjectIdentifiers()
        {
            var topicMap1 = TopicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = TopicMapSystem.CreateTopicMap(TestTM2);
            var subjectIdentifier = TopicMapSystem.CreateLocator("http://www.example.org/12345");
            var topic1 = topicMap1.CreateTopicBySubjectIdentifier(subjectIdentifier);
            var topic2 = topicMap2.CreateTopicBySubjectIdentifier(subjectIdentifier);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(1, topicMap2.Topics.Count);

            topicMap1.MergeIn(topicMap2);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(topic1, topicMap1.GetTopicBySubjectIdentifier(subjectIdentifier));
        }

        [Fact]
        public void MergeIn_MergeTopicsByEqualSubjectLocators()
        {
            var topicMap1 = TopicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = TopicMapSystem.CreateTopicMap(TestTM2);
            var subjectLocator = TopicMapSystem.CreateLocator("http://www.example.org/12345");
            var topic1 = topicMap1.CreateTopicBySubjectLocator(subjectLocator);
            var topic2 = topicMap2.CreateTopicBySubjectLocator(subjectLocator);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(1, topicMap2.Topics.Count);

            topicMap1.MergeIn(topicMap2);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(topic1, topicMap1.GetTopicBySubjectLocator(subjectLocator));
        }

        [Fact]
        public void MergeIn_MergeTopicsByItemIdentifierEqualToSubjectIdentifierFromOtherMap()
        {
            var topicMap1 = TopicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = TopicMapSystem.CreateTopicMap(TestTM2);
            var locator = TopicMapSystem.CreateLocator("http://www.example.org/12345");
            var topic1 = topicMap1.CreateTopicByItemIdentifier(locator);
            var topic2 = topicMap2.CreateTopicBySubjectIdentifier(locator);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(1, topicMap2.Topics.Count);
            Assert.Equal(topic1, topicMap1.GetConstructByItemIdentifier(locator));
            Assert.Null(topicMap1.GetTopicBySubjectIdentifier(locator));

            topicMap1.MergeIn(topicMap2);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(topic1, topicMap1.GetConstructByItemIdentifier(locator));
            Assert.Equal(topic1, topicMap1.GetTopicBySubjectIdentifier(locator));
        }

        [Fact]
        public void MergeIn_MergeTopicsBySubjectIdentifierEqualToItemIdentifierFromOtherMap()
        {
            var topicMap1 = TopicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = TopicMapSystem.CreateTopicMap(TestTM2);
            var locator = TopicMapSystem.CreateLocator("http://www.example.org/12345");
            var topic1 = topicMap1.CreateTopicBySubjectIdentifier(locator);
            var topic2 = topicMap2.CreateTopicByItemIdentifier(locator);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(1, topicMap2.Topics.Count);
            Assert.Null(topicMap1.GetConstructByItemIdentifier(locator));
            Assert.Equal(topic1, topicMap1.GetTopicBySubjectIdentifier(locator));

            topicMap1.MergeIn(topicMap2);

            Assert.Equal(1, topicMap1.Topics.Count);
            Assert.Equal(topic1, topicMap1.GetConstructByItemIdentifier(locator));
            Assert.Equal(topic1, topicMap1.GetTopicBySubjectIdentifier(locator));
        }
        #endregion
    }
}