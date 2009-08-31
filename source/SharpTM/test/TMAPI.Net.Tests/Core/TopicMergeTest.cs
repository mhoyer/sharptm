using TMAPI.Net.Core;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class TopicMergeTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        [Fact]
        public void MergeIn_TypesAreMergedToo()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();

            topic2.AddType(type);
            topic1.MergeIn(topic2);

            Assert.True(topic1.Types.Contains(type));
        }
        
        [Fact]
        public void MergeIn_TopicsReifiingDiffrentTMCsCannotBeMerged()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var association1 = topicMap.CreateAssociation(topicMap.CreateTopic());
            var association2 = topicMap.CreateAssociation(topicMap.CreateTopic());

            association1.Reifier = topic1;
            association2.Reifier = topic2;

            Assert.Equal(topic1, association1.Reifier);
            Assert.Equal(topic2, association2.Reifier);
            Assert.Throws<ModelConstraintException>("Topics reifiing diffrent Topic Maps constructs cannot be merged.", () => topic1.MergeIn(topic2));
        }

        [Fact]
        public void MergeIn_TopicOvertakesAllRolesPlayedOfTheOtherTopic()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var role = association.CreateRole(topicMap.CreateTopic(), topic2);

            Assert.Equal(4, topicMap.Topics.Count);
            Assert.False(topic1.RolesPlayed.Contains(role));
            Assert.True(topic2.RolesPlayed.Contains(role));

            topic1.MergeIn(topic2);

            Assert.True(topic1.RolesPlayed.Contains(role));
        }

        [Fact]
        public void MergeIn_SubjectIdentifiersAreOvertaken()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var subjectIdentifier1 = topicMap.CreateLocator("http://psi.exmaple.org/sid-1");
            var subjectIdentifier2 = topicMap.CreateLocator("http://psi.exmaple.org/sid-2");
            var topic1 = topicMap.CreateTopicBySubjectIdentifier(subjectIdentifier1);
            var topic2 = topicMap.CreateTopicBySubjectIdentifier(subjectIdentifier2);

            Assert.True(topic1.SubjectIdentifiers.Contains(subjectIdentifier1));
            Assert.False(topic1.SubjectIdentifiers.Contains(subjectIdentifier2));
            Assert.False(topic2.SubjectIdentifiers.Contains(subjectIdentifier1));
            Assert.True(topic2.SubjectIdentifiers.Contains(subjectIdentifier2));

            topic1.MergeIn(topic2);

            Assert.Equal(2, topic1.SubjectIdentifiers.Count);
            Assert.True(topic1.SubjectIdentifiers.Contains(subjectIdentifier1));
            Assert.True(topic1.SubjectIdentifiers.Contains(subjectIdentifier2));
        }

        [Fact]
        public void MergeIn_SubjectLocatorsAreOvertaken()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var subjectLocator1 = topicMap.CreateLocator("http://tinytim.sf.net");
            var subjectLocator2 = topicMap.CreateLocator("http://tinytim.sourceforge.net");
            var topic1 = topicMap.CreateTopicBySubjectLocator(subjectLocator1);
            var topic2 = topicMap.CreateTopicBySubjectLocator(subjectLocator2);

            Assert.True(topic1.SubjectLocators.Contains(subjectLocator1));
            Assert.False(topic1.SubjectLocators.Contains(subjectLocator2));
            Assert.False(topic2.SubjectLocators.Contains(subjectLocator1));
            Assert.True(topic2.SubjectLocators.Contains(subjectLocator2));

            topic1.MergeIn(topic2);

            Assert.Equal(2, topic1.SubjectLocators.Count);
            Assert.True(topic1.SubjectLocators.Contains(subjectLocator1));
            Assert.True(topic1.SubjectLocators.Contains(subjectLocator2));
        }

        [Fact]
        public void MergeIn_ItemIdentifiersAreOvertaken()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var itemIdentifier1 = topicMap.CreateLocator("http://tinytim.sf.net/test#1");
            var itemIdentifier2 = topicMap.CreateLocator("http://tinytim.sf.net/test#2");
            var topic1 = topicMap.CreateTopicByItemIdentifier(itemIdentifier1);
            var topic2 = topicMap.CreateTopicByItemIdentifier(itemIdentifier2);

            Assert.True(topic1.ItemIdentifiers.Contains(itemIdentifier1));
            Assert.False(topic1.ItemIdentifiers.Contains(itemIdentifier2));
            Assert.False(topic2.ItemIdentifiers.Contains(itemIdentifier1));
            Assert.True(topic2.ItemIdentifiers.Contains(itemIdentifier2));

            topic1.MergeIn(topic2);

            Assert.Equal(2, topic1.ItemIdentifiers.Count);
            Assert.True(topic1.ItemIdentifiers.Contains(itemIdentifier1));
            Assert.True(topic1.ItemIdentifiers.Contains(itemIdentifier2));
        }

        [Fact]
        public void MergeIn_DetectDuplicatesAndKeepReifier()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var reifier = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();

            var name1 = topic1.CreateName(type, "Name");
            var name2 = topic2.CreateName(type, "Name");

            Assert.Equal(4, topicMap.Topics.Count);

            name1.Reifier = reifier;

            Assert.Equal(reifier, name1.Reifier);
            Assert.Equal(1, topic1.Names.Count);
            Assert.True(topic1.Names.Contains(name1));
            Assert.Equal(1, topic2.Names.Count);
            Assert.True(topic2.Names.Contains(name2));

            topic1.MergeIn(topic2);

            Assert.Equal(3, topicMap.Topics.Count);
            Assert.Equal(1, topic1.Names.Count);

            var name = topic1.Names[0];

            Assert.Equal(reifier, name.Reifier);
        }

        [Fact]
        public void MergeIn_DetectDuplicatesAndMergeReifiers()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var reifier1 = topicMap.CreateTopic();
            var reifier2 = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();

            var name1 = topic1.CreateName(type, "Name");
            var name2 = topic2.CreateName(type, "Name");

            Assert.Equal(5, topicMap.Topics.Count);

            name1.Reifier = reifier1;
            name2.Reifier = reifier2;

            Assert.Equal(reifier1, name1.Reifier);
            Assert.Equal(reifier2, name2.Reifier);
            Assert.Equal(1, topic1.Names.Count);
            Assert.True(topic1.Names.Contains(name1));
            Assert.Equal(1, topic2.Names.Count);
            Assert.True(topic2.Names.Contains(name2));

            topic1.MergeIn(topic2);

            Assert.Equal(3, topicMap.Topics.Count);
            Assert.Equal(1, topic1.Names.Count);

            var name = topic1.Names[0];

            ITopic reifier = null;

            //  retrieve new created merged reifier
            foreach (var topic in topicMap.Topics)
            {
                if (!topic.Equals(topic1) && !topic.Equals(type))
                {
                    reifier = topic; 
                }
            }

            Assert.Equal(reifier, name.Reifier);
        }

        [Fact]
        public void MergeIn_DetectDuplicateAssociations()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var roleType = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var association1 = topicMap.CreateAssociation(type);
            var role1 = association1.CreateRole(roleType, topic1);
            var association2 = topicMap.CreateAssociation(type);
            var role2 = association2.CreateRole(roleType, topic2);

            Assert.Equal(4, topicMap.Topics.Count);
            Assert.Equal(2, topicMap.Associations.Count);
            Assert.True(topic1.RolesPlayed.Contains(role1));
            Assert.True(topic2.RolesPlayed.Contains(role2));
            Assert.Equal(1, topic1.RolesPlayed.Count);
            Assert.Equal(1, topic2.RolesPlayed.Count);

            topic1.MergeIn(topic2);

            Assert.Equal(3, topicMap.Topics.Count);
            Assert.Equal(1, topicMap.Associations.Count);

            var role = topic1.RolesPlayed[0];

            Assert.Equal(roleType, role.Type);
        }

        [Fact]
        public void MergeIn_DetectDuplicateNames()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var name1 = topic1.CreateName("Name");
            var name2 = topic2.CreateName("Name");
            var name3 = topic2.CreateName("Another Name");

            Assert.Equal(1, topic1.Names.Count);
            Assert.True(topic1.Names.Contains(name1));
            Assert.Equal(2, topic2.Names.Count);
            Assert.True(topic2.Names.Contains(name2));
            Assert.True(topic2.Names.Contains(name3));

            topic1.MergeIn(topic2);

            Assert.Equal(2, topic1.Names.Count);
        }

        [Fact]
        public void MergeIn_DetectDuplicateNamesAndMoveVariants()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var name1 = topic1.CreateName("Name");
            var name2 = topic2.CreateName("Name");
            var variant = name2.CreateVariant("Variant", topicMap.CreateTopic());

            Assert.Equal(1, topic1.Names.Count);
            Assert.True(topic1.Names.Contains(name1));
            Assert.Empty(name1.Variants);
            Assert.Equal(1, topic2.Names.Count);
            Assert.True(topic2.Names.Contains(name2));
            Assert.Equal(1, name2.Variants.Count);
            Assert.True(name2.Variants.Contains(variant));

            topic1.MergeIn(topic2);

            Assert.Equal(1, topic1.Names.Count);

            var name = topic1.Names[0];

            Assert.Equal(1, name.Variants.Count);

            var movedVariant = name.Variants[0];

            Assert.Equal("Variant", movedVariant.Value);
        }

        [Fact]
        public void MergeIn_DetectDuplicateNamesAndCombineItemIdentifiers()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var itemIdentifier1 = topicMap.CreateLocator("http://example.org/iid-1");
            var itemIdentifier2 = topicMap.CreateLocator("http://example.org/iid-2");
            var name1 = topic1.CreateName("Name");
            var name2 = topic2.CreateName("Name");

            name1.AddItemIdentifier(itemIdentifier1);
            name2.AddItemIdentifier(itemIdentifier2);

            Assert.True(name1.ItemIdentifiers.Contains(itemIdentifier1));
            Assert.True(name2.ItemIdentifiers.Contains(itemIdentifier2));
            Assert.Equal(1, topic1.Names.Count);
            Assert.True(topic1.Names.Contains(name1));
            Assert.Equal(1, topic2.Names.Count);
            Assert.True(topic2.Names.Contains(name2));

            topic1.MergeIn(topic2);

            Assert.Equal(1, topic1.Names.Count);

            var name = topic1.Names[0];

            Assert.Equal(2, name.ItemIdentifiers.Count);
            Assert.True(name.ItemIdentifiers.Contains(itemIdentifier1));
            Assert.True(name.ItemIdentifiers.Contains(itemIdentifier2));
            Assert.Equal("Name", name.Value);
        }

        [Fact]
        public void MergeIn_DetectDuplicateOccurrences()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var occurrenceType = topicMap.CreateTopic();
            var occurrence1 = topic1.CreateOccurrence(occurrenceType, "Occurrence");
            var occurrence2 = topic2.CreateOccurrence(occurrenceType, "Occurrence");
            var occurrence3 = topic2.CreateOccurrence(occurrenceType, "Another Occurrence");

            Assert.Equal(1, topic1.Occurrences.Count);
            Assert.True(topic1.Occurrences.Contains(occurrence1));
            Assert.Equal(2, topic2.Occurrences.Count);
            Assert.True(topic2.Occurrences.Contains(occurrence2));
            Assert.True(topic2.Occurrences.Contains(occurrence3));

            topic1.MergeIn(topic2);

            Assert.Equal(2, topic1.Occurrences.Count);
        }

        [Fact]
        public void MergeIn_DetectDuplicateOccurrencesAndCombineItemIdentifiers()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic1 = topicMap.CreateTopic();
            var topic2 = topicMap.CreateTopic();
            var itemIdentifier1 = topicMap.CreateLocator("http://example.org/iid-1");
            var itemIdentifier2 = topicMap.CreateLocator("http://example.org/iid-2");
            var occurrenceType = topicMap.CreateTopic();
            var occurrence1 = topic1.CreateOccurrence(occurrenceType, "Occurrence");
            var occurrence2 = topic2.CreateOccurrence(occurrenceType, "Occurrence");

            occurrence1.AddItemIdentifier(itemIdentifier1);
            occurrence2.AddItemIdentifier(itemIdentifier2);

            Assert.True(occurrence1.ItemIdentifiers.Contains(itemIdentifier1));
            Assert.True(occurrence2.ItemIdentifiers.Contains(itemIdentifier2));
            Assert.Equal(1, topic1.Occurrences.Count);
            Assert.True(topic1.Occurrences.Contains(occurrence1));
            Assert.Equal(1, topic2.Occurrences.Count);
            Assert.True(topic2.Occurrences.Contains(occurrence2));

            topic1.MergeIn(topic2);

            Assert.Equal(1, topic1.Occurrences.Count);

            var occurrence = topic1.Occurrences[0];

            Assert.Equal(2, occurrence.ItemIdentifiers.Count);
            Assert.True(occurrence.ItemIdentifiers.Contains(itemIdentifier1));
            Assert.True(occurrence.ItemIdentifiers.Contains(itemIdentifier2));
            Assert.Equal("Occurrence", occurrence.Value);
        }
        #endregion
    }
}