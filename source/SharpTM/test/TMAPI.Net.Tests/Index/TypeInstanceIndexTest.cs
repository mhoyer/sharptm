using TMAPI.Net.Index;
using Xunit;

namespace TMAPI.Net.Tests.Index
{
    public class TypeInstanceIndexTest : TMAPITestCase
    {
		#region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        private static void UpdateIndex(IIndex index)
        {
            if (!index.AutoUpdated)
            {
                index.Reindex();
            }
        }

        [Fact]
        public void TestTopic()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ITypeInstanceIndex)topicMap.GetIndex<ITypeInstanceIndex>();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetTopics(null));
            Assert.Empty(index.TopicTypes);

            var topic = topicMap.CreateTopic();
            UpdateIndex(index);

            Assert.Empty(index.TopicTypes);
            Assert.Equal(1, index.GetTopics(null).Count);
            Assert.True(index.GetTopics(null).Contains(topic));

            var type1 = topicMap.CreateTopic();
            var type2 = topicMap.CreateTopic();
            UpdateIndex(index);

            Assert.Empty(index.TopicTypes);
            Assert.Equal(3, index.GetTopics(null).Count);
            Assert.True(index.GetTopics(null).Contains(topic));
            Assert.True(index.GetTopics(null).Contains(type1));
            Assert.True(index.GetTopics(null).Contains(type2));
            Assert.Empty(index.GetTopics(new[] {type1, type2}, false));
            Assert.Empty(index.GetTopics(new[] { type1, type2 }, true));

            topic.AddType(type1);
            UpdateIndex(index);

            Assert.Equal(1, index.TopicTypes.Count);
            Assert.True(index.TopicTypes.Contains(type1));
            Assert.Equal(2, index.GetTopics(null).Count);
            Assert.False(index.GetTopics(null).Contains(topic));
            Assert.True(index.GetTopics(null).Contains(type1));
            Assert.True(index.GetTopics(null).Contains(type2));
            Assert.Equal(1, index.GetTopics(type1).Count);
            Assert.True(index.GetTopics(type1).Contains(topic));
            Assert.Equal(1, index.GetTopics(new[] { type1, type2 }, false).Count);
            Assert.True(index.GetTopics(new[] { type1, type2 }, false).Contains(topic));
            Assert.Empty(index.GetTopics(new[] { type1, type2 }, true));

            //  topic now has two types
            topic.AddType(type2);
            UpdateIndex(index);

            Assert.Equal(2, index.TopicTypes.Count);
            Assert.True(index.TopicTypes.Contains(type1));
            Assert.True(index.TopicTypes.Contains(type2));
            Assert.Equal(2, index.GetTopics(null).Count);
            Assert.False(index.GetTopics(null).Contains(topic));
            Assert.True(index.GetTopics(null).Contains(type1));
            Assert.True(index.GetTopics(null).Contains(type2));
            Assert.Equal(1, index.GetTopics(type1).Count);
            Assert.True(index.GetTopics(type1).Contains(topic));
            Assert.Equal(1, index.GetTopics(type2).Count);
            Assert.True(index.GetTopics(type2).Contains(topic));
            Assert.Equal(1, index.GetTopics(new[] { type1, type2 }, false).Count);
            Assert.True(index.GetTopics(new[] { type1, type2 }, false).Contains(topic));
            Assert.Equal(1, index.GetTopics(new[] { type1, type2 }, true).Count);
            Assert.True(index.GetTopics(new[] { type1, type2 }, true).Contains(topic));

            topic.Remove();
            UpdateIndex(index);

            Assert.Empty(index.TopicTypes);
            Assert.Equal(2, index.GetTopics(null).Count);
            Assert.True(index.GetTopics(null).Contains(type1));
            Assert.True(index.GetTopics(null).Contains(type2));
            Assert.Empty(index.GetTopics(type1));
            Assert.Empty(index.GetTopics(type2));
            Assert.Empty(index.GetTopics(new[] { type1, type2 }, false));
            Assert.Empty(index.GetTopics(new[] { type1, type2 }, true));
        }

        [Fact]
        public void TestAssociation()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ITypeInstanceIndex)topicMap.GetIndex<ITypeInstanceIndex>();
            var type = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetAssociations(type));
            Assert.Empty(index.AssociationTypes);

            var typedAssociation = topicMap.CreateAssociation(topicMap.CreateTopic());
            UpdateIndex(index);

            Assert.Empty(index.GetAssociations(type));
            Assert.False(index.AssociationTypes.Contains(type));
            Assert.Equal(1, index.AssociationTypes.Count);

            typedAssociation.Type = type;
            UpdateIndex(index);

            Assert.NotEmpty(index.AssociationTypes);
            Assert.Equal(1, index.GetAssociations(type).Count);
            Assert.True(index.GetAssociations(type).Contains(typedAssociation));
            Assert.True(index.AssociationTypes.Contains(type));

            typedAssociation.Type = topicMap.CreateTopic();
            UpdateIndex(index);

            Assert.False(index.AssociationTypes.Contains(type));
            Assert.Equal(1, index.AssociationTypes.Count);

            typedAssociation.Type = type;
            typedAssociation.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetAssociations(type));
            Assert.Empty(index.AssociationTypes);
        }

        [Fact]
        public void TestRole()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ITypeInstanceIndex)topicMap.GetIndex<ITypeInstanceIndex>();
            var type = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetRoles(type));
            Assert.Empty(index.RoleTypes);

            var parent = topicMap.CreateAssociation(topicMap.CreateTopic());
            var typedRole = parent.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic());
            UpdateIndex(index);

            Assert.Equal(1, index.RoleTypes.Count);
            Assert.False(index.RoleTypes.Contains(type));

            typedRole.Type = type;
            UpdateIndex(index);

            Assert.Equal(1, index.RoleTypes.Count);
            Assert.Equal(1, index.GetRoles(type).Count);
            Assert.True(index.GetRoles(type).Contains(typedRole));

            typedRole.Type = topicMap.CreateTopic();
            UpdateIndex(index);

            Assert.Equal(1, index.RoleTypes.Count);
            Assert.False(index.RoleTypes.Contains(type));

            typedRole.Type = type;
            typedRole.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetRoles(type));
            Assert.Empty(index.RoleTypes);

            typedRole = parent.CreateRole(type, topicMap.CreateTopic());
            UpdateIndex(index);

            Assert.Equal(1, index.RoleTypes.Count);
            Assert.Equal(1, index.GetRoles(type).Count);
            Assert.True(index.GetRoles(type).Contains(typedRole));

            parent.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetRoles(type));
            Assert.Empty(index.RoleTypes);
        }

        [Fact]
        public void TestOccurrence()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ITypeInstanceIndex)topicMap.GetIndex<ITypeInstanceIndex>();
            var type = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(type));
            Assert.Empty(index.OccurrenceTypes);

            var parent = topicMap.CreateTopic();
            var typedOccurrence = parent.CreateOccurrence(topicMap.CreateTopic(), "Occurrence");
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(type));
            Assert.Equal(1, index.OccurrenceTypes.Count);
            Assert.False(index.OccurrenceTypes.Contains(type));

            typedOccurrence.Type = type;
            UpdateIndex(index);

            Assert.Equal(1, index.OccurrenceTypes.Count);
            Assert.True(index.OccurrenceTypes.Contains(type));
            Assert.Equal(1, index.GetOccurrences(type).Count);
            Assert.True(index.GetOccurrences(type).Contains(typedOccurrence));

            typedOccurrence.Type = topicMap.CreateTopic();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(type));
            Assert.Equal(1, index.OccurrenceTypes.Count);
            Assert.False(index.OccurrenceTypes.Contains(type));

            typedOccurrence.Type = type;
            typedOccurrence.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(type));
            Assert.Empty(index.OccurrenceTypes);
        }

        [Fact]
        public void TestName()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ITypeInstanceIndex)topicMap.GetIndex<ITypeInstanceIndex>();
            var type = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(type));
            Assert.Empty(index.NameTypes);

            var parent = topicMap.CreateTopic();
            var typedName = parent.CreateName("Name");
            UpdateIndex(index);

            Assert.Equal(1, index.NameTypes.Count);
            Assert.False(index.NameTypes.Contains(type));
            Assert.Empty(index.GetNames(type));

            typedName.Type = type;
            UpdateIndex(index);

            Assert.NotEmpty(index.NameTypes);
            Assert.True(index.NameTypes.Contains(type));
            Assert.Equal(1, index.GetNames(type).Count);
            Assert.True(index.GetNames(type).Contains(typedName));

            typedName.Type = topicMap.CreateTopic();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(type));
            Assert.Equal(1, index.NameTypes.Count);
            Assert.False(index.NameTypes.Contains(type));

            typedName.Type = type;
            typedName.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(type));
            Assert.Empty(index.NameTypes);
        }
        #endregion
    }
}