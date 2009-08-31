using Xunit;

namespace TMAPI.Net.Tests.Core
{
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
            var topicMap1 = topicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = topicMapSystem.CreateTopicMap(TestTM2);
            var itemIdentifier = topicMapSystem.CreateLocator("http://www.example.org/12345");
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
            var topicMap1 = topicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = topicMapSystem.CreateTopicMap(TestTM2);
            var subjectIdentifier = topicMapSystem.CreateLocator("http://www.example.org/12345");
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
            var topicMap1 = topicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = topicMapSystem.CreateTopicMap(TestTM2);
            var subjectLocator = topicMapSystem.CreateLocator("http://www.example.org/12345");
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
            var topicMap1 = topicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = topicMapSystem.CreateTopicMap(TestTM2);
            var locator = topicMapSystem.CreateLocator("http://www.example.org/12345");
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
            var topicMap1 = topicMapSystem.CreateTopicMap(TestTM1);
            var topicMap2 = topicMapSystem.CreateTopicMap(TestTM2);
            var locator = topicMapSystem.CreateLocator("http://www.example.org/12345");
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