// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopicMergeDetectionTest.cs">
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
//   Defines the TopicMergeDetectionTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Net.Core;
    using Xunit;

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
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var subjectIdentifier = topicMap.CreateLocator("http://sf.net/projects/tmapi");

            topic1.AddSubjectIdentifier(subjectIdentifier);

            Assert.Equal(1, topic1.SubjectIdentifiers.Count);
            Assert.True(topic1.SubjectIdentifiers.Contains(subjectIdentifier));
            if(TopicMapSystemFactory.HasFeature(AUTOMERGE_FEATURE))
            {
                topic2.AddSubjectIdentifier(subjectIdentifier);
                Assert.Equal(1, topicMap.Topics.Count);
                Assert.Equal(1, topicMap.Topics[0].SubjectIdentifiers.Count);
                Assert.Contains(subjectIdentifier, topicMap.Topics[0].SubjectIdentifiers);
            }
            else
            {
                var e = Assert.Throws<IdentityConstraintException>("Detected topic with identical subject identifier.", () => topic2.AddSubjectIdentifier(subjectIdentifier));
                Assert.Equal(topic2, e.Reporter);
                Assert.Equal(topic1, e.Existing);
                Assert.Equal(subjectIdentifier, e.Locator);
            }
        }

        [Fact]
        public void AddSubjectIdentifier_AddingDuplicateSubjectIdentifierOnSameTopicIsIgnored()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
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
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var subjectLocator = topicMap.CreateLocator("http://sf.net/projects/tmapi");

            topic1.AddSubjectLocator(subjectLocator);

            Assert.Equal(1, topic1.SubjectLocators.Count);
            Assert.True(topic1.SubjectLocators.Contains(subjectLocator));
            if (TopicMapSystemFactory.HasFeature(AUTOMERGE_FEATURE))
            {
                topic2.AddSubjectLocator(subjectLocator);
                Assert.Equal(1, topicMap.Topics.Count);
                Assert.Equal(1, topicMap.Topics[0].SubjectLocators.Count);
                Assert.Contains(subjectLocator, topicMap.Topics[0].SubjectLocators);
            }
            else
            {
                var e = Assert.Throws<IdentityConstraintException>("Detected topic with identical subject locator.",
                                                           () => topic2.AddSubjectLocator(subjectLocator));
                Assert.Equal(topic2, e.Reporter);
                Assert.Equal(topic1, e.Existing);
                Assert.Equal(subjectLocator, e.Locator);
            }
        }

        [Fact]
        public void AddSubjectLocator_AddingDuplicateSubjectLocatorOnSameTopicIsIgnored()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
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
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var locator = topicMap.CreateLocator("http://sf.net/projects/tmapi");

            topic1.AddSubjectIdentifier(locator);

            Assert.Equal(1, topic1.SubjectIdentifiers.Count);
            Assert.True(topic1.SubjectIdentifiers.Contains(locator));
            if (TopicMapSystemFactory.HasFeature(AUTOMERGE_FEATURE))
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
                var e = Assert.Throws<IdentityConstraintException>(
                    "Detected topic with subject identifier equal to this item identifier.",
                    () => topic2.AddItemIdentifier(locator));
                Assert.Equal(topic2, e.Reporter);
                Assert.Equal(topic1, e.Existing);
                Assert.Equal(locator, e.Locator);
            }
        }

        [Fact]
        public void AddItemIdentifier_AddingItemIdentifierEqualToSubjectIdentifierOnSameTopicIsAccepted()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
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
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var locator = topicMap.CreateLocator("http://sf.net/projects/tmapi");
            var topic1 = topicMap.CreateTopicByItemIdentifier(locator);
            var topic2 = topicMap.CreateTopic();

            Assert.Equal(1, topic1.ItemIdentifiers.Count);
            Assert.True(topic1.ItemIdentifiers.Contains(locator));
            Assert.Equal(topic1, topicMap.GetConstructByItemIdentifier(locator));
            if (TopicMapSystemFactory.HasFeature(AUTOMERGE_FEATURE))
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
                var e = Assert.Throws<IdentityConstraintException>(
                    "Detected topic with item identifier equal to this subject identifier.",
                    () => topic2.AddSubjectIdentifier(locator));
                Assert.Equal(topic2, e.Reporter);
                Assert.Equal(topic1, e.Existing);
                Assert.Equal(locator, e.Locator);
            }
        }

        [Fact]
        public void AddSubjectIdentifier_AddingSubjectIdentifierEqualToItemIdentifierOnSameTopicIsAccepted()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
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