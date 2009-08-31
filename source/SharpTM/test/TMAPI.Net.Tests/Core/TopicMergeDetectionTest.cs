using TMAPI.Net.Core;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class TopicMergeDetectionTest : TMAPITestCase
    {
        #region constants
        public const string TestTM1 = "mem://localhost/testm1";
    	public const string AUTOMERGE_FEATURE = "http://tmapi.org/features/automerge";
        #endregion

        #region Tests

        [Fact]
        public void AddSubjectIdentifier_DetectDuplicateSubjectIdentifier()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var subjectIdentifier = topicMap.CreateLocator("http://sf.net/projects/tmapi");

            topic1.AddSubjectIdentifier(subjectIdentifier);

            Assert.Equal(1, topic1.SubjectIdentifiers.Count);
            Assert.True(topic1.SubjectIdentifiers.Contains(subjectIdentifier));
            if(topicMapSystemFactory.HasFeature(AUTOMERGE_FEATURE))
            {
                topic2.AddSubjectIdentifier(subjectIdentifier);
                Assert.Equal(1, topicMap.Topics.Count);
                Assert.Equal(1, topicMap.Topics[0].SubjectIdentifiers.Count);
                Assert.Contains(subjectIdentifier, topicMap.Topics[0].SubjectIdentifiers);
            }
            else
            {
                Assert.Throws<IdentityConstraintException>("Detected topic with identical subject identifier.", () => topic2.AddSubjectIdentifier(subjectIdentifier));                
            }
        }

        [Fact]
        public void AddSubjectIdentifier_AddingDuplicateSubjectIdentifierOnSameTopicIsIgnored()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var subjectIdentifier = topicMap.CreateLocator("http://sf.net/projects/tmapi");

            topic.AddSubjectIdentifier(subjectIdentifier);

            Assert.Equal(1, topic.SubjectIdentifiers.Count);
            Assert.True(topic.SubjectIdentifiers.Contains(subjectIdentifier));
            Assert.Equal(topic, topicMap.GetTopicBySubjectIdentifier(subjectIdentifier));

            topic.AddSubjectIdentifier(subjectIdentifier);

            Assert.Equal(1, topic.SubjectIdentifiers.Count);
        }

        [Fact]
        public void AddSubjectLocator_DetectDuplicateSubjectLocator()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var subjectLocator = topicMap.CreateLocator("http://sf.net/projects/tmapi");

            topic1.AddSubjectLocator(subjectLocator);

            Assert.Equal(1, topic1.SubjectLocators.Count);
            Assert.True(topic1.SubjectLocators.Contains(subjectLocator));
			if (topicMapSystemFactory.HasFeature(AUTOMERGE_FEATURE))
            {
                topic2.AddSubjectLocator(subjectLocator);
                Assert.Equal(1, topicMap.Topics.Count);
                Assert.Equal(1, topicMap.Topics[0].SubjectLocators.Count);
                Assert.Contains(subjectLocator, topicMap.Topics[0].SubjectLocators);
            }
            else
            {
                Assert.Throws<IdentityConstraintException>("Detected topic with identical subject locator.",
                                               () => topic2.AddSubjectLocator(subjectLocator));
            }
        }

        [Fact]
        public void AddSubjectLocator_AddingDuplicateSubjectLocatorOnSameTopicIsIgnored()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var subjectLocator = topicMap.CreateLocator("http://sf.net/projects/tmapi");

            topic.AddSubjectLocator(subjectLocator);

            Assert.Equal(1, topic.SubjectLocators.Count);
            Assert.True(topic.SubjectLocators.Contains(subjectLocator));
            Assert.Equal(topic, topicMap.GetTopicBySubjectLocator(subjectLocator));

            topic.AddSubjectLocator(subjectLocator);

            Assert.Equal(1, topic.SubjectLocators.Count);
        }

        [Fact]
        public void AddItemIdentifier_DetectItemIdentifierEqualToSubjectIdentifier()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var locator = topicMap.CreateLocator("http://sf.net/projects/tmapi");

            topic1.AddSubjectIdentifier(locator);

            Assert.Equal(1, topic1.SubjectIdentifiers.Count);
            Assert.True(topic1.SubjectIdentifiers.Contains(locator));
			if (topicMapSystemFactory.HasFeature(AUTOMERGE_FEATURE))
            {
                topic2.AddItemIdentifier(locator);
                Assert.Equal(1, topicMap.Topics.Count);
                // one iid as the autogenerated iid was removed
                // Assert.Equal(1, topicMap.Topics[0].ItemIdentifiers.Count);
                Assert.Contains(locator, topicMap.Topics[0].ItemIdentifiers);
                Assert.Equal(1, topicMap.Topics[0].SubjectIdentifiers.Count);
                Assert.Contains(locator, topicMap.Topics[0].SubjectIdentifiers);
            }
            else
            {
                Assert.Throws<IdentityConstraintException>(
                    "Detected topic with subject identifier equal to this item identifier.",
                    () => topic2.AddItemIdentifier(locator));
            }
        }

        [Fact]
        public void AddItemIdentifier_AddingItemIdentifierEqualToSubjectIdentifierOnSameTopicIsAccepted()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var locator = topicMap.CreateLocator("http://sf.net/projects/tmapi");
            var topic = topicMap.CreateTopicBySubjectIdentifier(locator);

            Assert.Equal(1, topic.SubjectIdentifiers.Count);
            Assert.True(topic.SubjectIdentifiers.Contains(locator));
            Assert.Empty(topic.ItemIdentifiers);
            Assert.Equal(topic, topicMap.GetTopicBySubjectIdentifier(locator));
            Assert.Null(topicMap.GetConstructByItemIdentifier(locator));

            topic.AddItemIdentifier(locator);

            Assert.Equal(1, topic.SubjectIdentifiers.Count);
            Assert.True(topic.SubjectIdentifiers.Contains(locator));
            Assert.Equal(1, topic.ItemIdentifiers.Count);
            Assert.True(topic.ItemIdentifiers.Contains(locator));
            Assert.Equal(topic, topicMap.GetTopicBySubjectIdentifier(locator));
            Assert.Equal(topic, topicMap.GetConstructByItemIdentifier(locator));
        }

        [Fact]
        public void AddSubjectIdentifier_DetectSubjectIdentifierEqualToItemIdentifier()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var locator = topicMap.CreateLocator("http://sf.net/projects/tmapi");
            var topic1 = topicMap.CreateTopicByItemIdentifier(locator);
            var topic2 = topicMap.CreateTopic();

            Assert.Equal(1, topic1.ItemIdentifiers.Count);
            Assert.True(topic1.ItemIdentifiers.Contains(locator));
            Assert.Equal(topic1, topicMap.GetConstructByItemIdentifier(locator));
			if (topicMapSystemFactory.HasFeature(AUTOMERGE_FEATURE))
            {
                topic2.AddSubjectIdentifier(locator);
                Assert.Equal(1, topicMap.Topics.Count);
                // Assert.Equal(1, topicMap.Topics[0].ItemIdentifiers.Count);
                Assert.Contains(locator, topicMap.Topics[0].ItemIdentifiers);
                Assert.Equal(1, topicMap.Topics[0].SubjectIdentifiers.Count);
                Assert.Contains(locator, topicMap.Topics[0].SubjectIdentifiers);
            }
            else
            {
                Assert.Throws<IdentityConstraintException>(
                    "Detected topic with item identifier equal to this subject identifier.",
                    () => topic2.AddSubjectIdentifier(locator));
            }
        }

        [Fact]
        public void AddSubjectIdentifier_AddingSubjectIdentifierEqualToItemIdentifierOnSameTopicIsAccepted()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var locator = topicMap.CreateLocator("http://sf.net/projects/tmapi");
            var topic = topicMap.CreateTopicByItemIdentifier(locator);

            Assert.Equal(1, topic.ItemIdentifiers.Count);
            Assert.True(topic.ItemIdentifiers.Contains(locator));
            Assert.Empty(topic.SubjectIdentifiers);
            Assert.Equal(topic, topicMap.GetConstructByItemIdentifier(locator));
            Assert.Null(topicMap.GetTopicBySubjectIdentifier(locator));

            topic.AddSubjectIdentifier(locator);

            Assert.Equal(1, topic.SubjectIdentifiers.Count);
            Assert.True(topic.SubjectIdentifiers.Contains(locator));
            Assert.Equal(1, topic.ItemIdentifiers.Count);
            Assert.True(topic.ItemIdentifiers.Contains(locator));
            Assert.Equal(topic, topicMap.GetTopicBySubjectIdentifier(locator));
            Assert.Equal(topic, topicMap.GetConstructByItemIdentifier(locator));
        }
        #endregion
    }
}