using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;
using TMAPI.Net.Index;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class TopicMapTests : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        public static readonly string TestTM2 = "mem://localhost/testm2";
        public static readonly string TestTopic1 = "mem://localhost/testm1/topic1";
        public static readonly string TestTopic2 = "mem://localhost/testm1/topic2";
        public static readonly string TestTopic3 = "mem://localhost/testm1/topic3";
        public static readonly string TestSubjectIdentifier = "mem://localhost/testm1/identifier";
        public static readonly string TestSubjectLocator = "mem://localhost/testm1/locator";
        #endregion

        #region Tests
        [Fact]
        public void Constructor_TestInitialisation()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
			Assert.NotNull(tm.Associations);
			Assert.NotNull(tm.Topics);

			Assert.Empty(tm.Associations);
			Assert.Empty(tm.Topics);
		}

        [Fact]
        public void Topics_TestEmptyReadOnlyCollection()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            var topics = tm.Topics;
            Assert.Empty(topics);
            Assert.IsAssignableFrom<ReadOnlyCollection<ITopic>>(tm.Topics);
        }

        [Fact]
        public void CreateTopic_Test()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            ITopic topic = tm.CreateTopic();
            var topics = tm.Topics;
            Assert.Equal(1, topic.ItemIdentifiers.Count);
            Assert.Equal(1, topics.Count);
            Assert.Equal(topic, topics[0]);
        }

        [Fact]
        public void CreateTopicByItemIdentifier_AddOneTopic()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            ILocator locator = tm.CreateLocator(TestTopic1);
            ITopic topic = tm.CreateTopicByItemIdentifier(locator);
            var topics = tm.Topics;
            Assert.Equal(1, topic.ItemIdentifiers.Count);
            Assert.Equal(locator, topic.ItemIdentifiers[0]);
            Assert.Equal(1, topics.Count);
            Assert.Equal(topic, topics[0]);
        }

        [Fact]
        public void CreateTopicByItemIdentifier_AddThreeTopics()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            ILocator locator1 = tm.CreateLocator(TestTopic1);
            ILocator locator2 = tm.CreateLocator(TestTopic2);
            ILocator locator3 = tm.CreateLocator(TestTopic3);
            ITopic topic1 = tm.CreateTopicByItemIdentifier(locator1);
            ITopic topic2 = tm.CreateTopicByItemIdentifier(locator2);
            ITopic topic3 = tm.CreateTopicByItemIdentifier(locator3);
            var topics = tm.Topics;
            Assert.Equal(locator1, topic1.ItemIdentifiers[0]);
            Assert.Equal(locator2, topic2.ItemIdentifiers[0]);
            Assert.Equal(locator3, topic3.ItemIdentifiers[0]);
            Assert.Equal(3, topics.Count);
            Assert.Equal(topic1, topics[0]);
            Assert.Equal(topic2, topics[1]);
            Assert.Equal(topic3, topics[2]);
        }

        [Fact]
        public void CreateTopicBySubjectIdentifier_CreateOneTopic()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            ILocator identifier = topicMapSystem.CreateLocator(TestSubjectIdentifier);
            ITopic topic = tm.CreateTopicBySubjectIdentifier(identifier);
            var topics = tm.Topics;
            Assert.Equal(1, topics.Count);
            Assert.Equal(topic, topics[0]);
            Assert.Equal(1, topic.SubjectIdentifiers.Count);
            Assert.Equal(identifier, topic.SubjectIdentifiers[0]);
        }

        [Fact]
        public void CreateTopicBySubjectLocator_CreateOneTopic()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            ILocator locator = topicMapSystem.CreateLocator(TestSubjectLocator);
            ITopic topic = tm.CreateTopicBySubjectLocator(locator);
            var topics = tm.Topics;
            Assert.Equal(1, topics.Count);
            Assert.Equal(topic, topics[0]);
            Assert.Equal(1, topic.SubjectLocators.Count);
            Assert.Equal(locator, topic.SubjectLocators[0]);
        }

        [Fact]
        public void Associations_TestForEmptyAssocListAtBeginning()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            Assert.Empty(tm.Associations);
            Assert.IsAssignableFrom<ReadOnlyCollection<IAssociation>>(tm.Associations);
        }

        [Fact]
        public void CreateAssociation_OneScope()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            var type = tm.CreateTopic();
            var scope = tm.CreateTopic();
            var association = tm.CreateAssociation(type, scope);
            Assert.NotNull(association);
            Assert.Equal(1,tm.Associations.Count);
            Assert.Equal(association, tm.Associations[0]);
            Assert.Equal(1, association.Scope.Count);  
            Assert.Equal(scope, association.Scope[0]);
            
            Assert.NotNull(association.Type);
            Assert.True(association.Scope.Contains(scope));
        }

        [Fact]
        public void CreateAssociation_ByScopeList()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            var type = tm.CreateTopic();
            var scope1 = tm.CreateTopic();
            var scope2 = tm.CreateTopic();
            IList<ITopic> scopes = new List<ITopic> {scope1, scope2};
            var association = tm.CreateAssociation(type, scopes);
            Assert.NotNull(association);
            Assert.IsType<ReadOnlyCollection<ITopic>>(association.Scope);
            Assert.Equal(2, association.Scope.Count);
            Assert.Equal(scope1, association.Scope[0]);
            Assert.Equal(scope2, association.Scope[1]);
        }

        // remark: added test
        [Fact]
        public void CreateAssociation_CreateTwoOfSameTypeAndRemove()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            var type = tm.CreateTopic();
            var roletype1 = tm.CreateTopic();
            var roletype2 = tm.CreateTopic();
            var player1 = tm.CreateTopic();
            var player2 = tm.CreateTopic();
            var player3 = tm.CreateTopic();

            var association1 = tm.CreateAssociation(type);
            var role1 = association1.CreateRole(roletype1, player1);
            var role2 = association1.CreateRole(roletype2, player2);

            var association2 = tm.CreateAssociation(type);
            var role3 = association2.CreateRole(roletype1, player1);
            var role4 = association2.CreateRole(roletype2, player3);

            Assert.Equal(2, tm.Associations.Count);
            
            Assert.Contains(association1, tm.Associations);
            Assert.Equal(type, association1.Type);
            Assert.Equal(2, association1.Roles.Count);
            Assert.Contains(role1, association1.Roles);
            Assert.Contains(role2, association1.Roles);
            Assert.Equal(roletype1, role1.Type);
            Assert.Equal(player1, role1.Player);
            Assert.Equal(roletype2, role2.Type);
            Assert.Equal(player2, role2.Player);

            Assert.Contains(association2, tm.Associations);
            Assert.Equal(type, association2.Type);
            Assert.Equal(2, association2.Roles.Count);
            Assert.Contains(role3, association2.Roles);
            Assert.Contains(role4, association2.Roles);
            Assert.Equal(roletype1, role3.Type);
            Assert.Equal(player1, role3.Player);
            Assert.Equal(roletype2, role4.Type);
            Assert.Equal(player3, role4.Player);

            Assert.Equal(2, player1.RolesPlayed.Count);
            Assert.Equal(1, player2.RolesPlayed.Count);
            Assert.Equal(1, player3.RolesPlayed.Count);

            association1.Remove();

            Assert.Equal(1, tm.Associations.Count);
            Assert.Contains(association2, tm.Associations);
            Assert.Equal(1, player1.RolesPlayed.Count);
            Assert.Equal(0, player2.RolesPlayed.Count);
            Assert.Equal(1, player3.RolesPlayed.Count);

            association2.Remove();

            Assert.Equal(0, tm.Associations.Count);
            Assert.Equal(0, player1.RolesPlayed.Count);
            Assert.Equal(0, player2.RolesPlayed.Count);
            Assert.Equal(0, player3.RolesPlayed.Count);

        }

        [Fact]
        public void Parent_ReturnsNull()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            Assert.Null(tm.Parent);
        }

        [Fact]
        public void GetConstructById_TestForImplementation()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            tm.GetConstructById("anyID");
        }

        [Fact]
        public void GetConstructByItemIdentifier_RetrieveATopic()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            var iid = tm.CreateLocator(TestTopic1);
            ITopic topic = tm.CreateTopicByItemIdentifier(iid);
            IConstruct tmc = tm.GetConstructByItemIdentifier(iid);
            Assert.Equal(topic, tmc);
        }

        [Fact]
        public void GetTopicBySubjectIdentifier_RetrieveATopic()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            var sid = tm.CreateLocator(TestSubjectIdentifier);
            ITopic topic = tm.CreateTopicBySubjectIdentifier(sid);
            IConstruct tmc = tm.GetTopicBySubjectIdentifier(sid);
            Assert.Equal(topic, tmc);
        }

        [Fact]
        public void GetTopicBySubjectLocator_RetrieveATopic()
        {
            ITopicMap tm = topicMapSystem.CreateTopicMap(TestTM1);
            var slo = tm.CreateLocator(TestSubjectLocator);
            ITopic topic = tm.CreateTopicBySubjectLocator(slo);
            IConstruct tmc = tm.GetTopicBySubjectLocator(slo);
            Assert.Equal(topic, tmc);
        }
        
        [Fact]
        public void ItemIdentifiers_TestForReadOnlyCollection()
        {
            var tm = topicMapSystem.CreateTopicMap(TestTM1);
            Assert.IsAssignableFrom<ReadOnlyCollection<ILocator>>(tm.ItemIdentifiers);
        }

        [Fact]
        public void AddItemIdentifiers_AddNewIID()
        {
            var tm = topicMapSystem.CreateTopicMap(TestTM1);
            var iid = tm.CreateLocator("mem://bla/fisch");
            
			tm.AddItemIdentifier(iid);
            Assert.Equal(2, tm.ItemIdentifiers.Count);
            Assert.Contains(iid, tm.ItemIdentifiers);
        }

        [Fact]
        public void RemoveItemIdentifiers_RemoveNewIID()
        {
            var tm = topicMapSystem.CreateTopicMap(TestTM1);
            var iid = tm.CreateLocator("mem://bla/fisch");
            
			tm.AddItemIdentifier(iid);
            Assert.Equal(2, tm.ItemIdentifiers.Count);
            Assert.Contains(iid, tm.ItemIdentifiers);

			tm.RemoveItemIdentifier(tm.CreateLocator(TestTM1));
			Assert.Equal(1, tm.ItemIdentifiers.Count);
			Assert.Contains(iid, tm.ItemIdentifiers);
			Assert.DoesNotContain(tm.CreateLocator(TestTM1), tm.ItemIdentifiers);
            
			tm.RemoveItemIdentifier(iid);
			Assert.Empty(tm.ItemIdentifiers);
        }

        #region TMAPI test cases
        [Fact]
        public void Parent_ParentIsNull()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Null(topicMap.Parent);
        }

        [Fact]
        public void CreateTopicBySubjectIdentifier()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var subjectIdentifier = topicMap.CreateLocator("http://www.example.org");

            Assert.Empty(topicMap.Topics);

            var topic = topicMap.CreateTopicBySubjectIdentifier(subjectIdentifier);

            Assert.Equal(1, topicMap.Topics.Count);
            Assert.True(topicMap.Topics.Contains(topic));
            Assert.Equal(1, topic.SubjectIdentifiers.Count);
            Assert.Empty(topic.ItemIdentifiers);
            Assert.Empty(topic.SubjectLocators);

            var subjectIdentifier2 = topic.SubjectIdentifiers[0];

            Assert.Equal(subjectIdentifier, subjectIdentifier2);
        }

        [Fact]
        public void CreateTopicBySubjectIdentifier_UsingInvalidSubjectIdentifierThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Throws<ModelConstraintException>("Using null as a subject identifier for creating a nwe topic is not allowed.", () => topicMap.CreateTopicBySubjectIdentifier(null));
        }

        [Fact]
        public void CreateTopicBySubjectLocator()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var subjectLocator = topicMap.CreateLocator("http://www.example.org");

            Assert.Empty(topicMap.Topics);

            var topic = topicMap.CreateTopicBySubjectLocator(subjectLocator);

            Assert.Equal(1, topicMap.Topics.Count);
            Assert.True(topicMap.Topics.Contains(topic));
            Assert.Equal(1, topic.SubjectLocators.Count);
            Assert.Empty(topic.ItemIdentifiers);
            Assert.Empty(topic.SubjectIdentifiers);

            var subjectLocator2 = topic.SubjectLocators[0];

            Assert.Equal(subjectLocator, subjectLocator2);
        }

        [Fact]
        public void CreateTopicBySubjectLocator_UsingInvalidSubjectLocatorThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Throws<ModelConstraintException>("Using null as a subject locator for creating a nwe topic is not allowed.", () => topicMap.CreateTopicBySubjectLocator(null));
        }

        [Fact]
        public void CreateTopicByItemIdentifier()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var itemIdentifier = topicMap.CreateLocator("http://www.example.org");

            Assert.Empty(topicMap.Topics);

            var topic = topicMap.CreateTopicByItemIdentifier(itemIdentifier);

            Assert.Equal(1, topicMap.Topics.Count);
            Assert.True(topicMap.Topics.Contains(topic));
            Assert.Equal(1, topic.ItemIdentifiers.Count);
            Assert.Empty(topic.SubjectIdentifiers);
            Assert.Empty(topic.SubjectLocators);

            var itemIdentifier2 = topic.ItemIdentifiers[0];

            Assert.Equal(itemIdentifier, itemIdentifier2);
        }

        [Fact]
        public void CreateTopicByItemIdentifier_UsingInvalidItemIdentifierThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Throws<ModelConstraintException>("Using null as a item identifier for creating a nwe topic is not allowed.", () => topicMap.CreateTopicByItemIdentifier(null));
        }

        [Fact]
        public void CreateTopic_CreateTopicWithAutomaticAssigendItemIdentifier()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Empty(topicMap.Topics);

            var topic = topicMap.CreateTopic();

            Assert.Equal(1, topicMap.Topics.Count);
            Assert.True(topicMap.Topics.Contains(topic));
            Assert.Equal(1, topic.ItemIdentifiers.Count);
            Assert.Empty(topic.SubjectIdentifiers);
            Assert.Empty(topic.SubjectLocators);
        }

        [Fact]
        public void GetTopicBySubjectIdentifier()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var subjectIdentifier = topicMap.CreateLocator("http://www.example.org");
            var topic1 = topicMap.GetTopicBySubjectIdentifier(subjectIdentifier);

            Assert.Null(topic1);

            var topic2 = topicMap.CreateTopicBySubjectIdentifier(subjectIdentifier);
            topic1 = topicMap.GetTopicBySubjectIdentifier(subjectIdentifier);

            Assert.NotNull(topic1);
            Assert.Equal(topic2, topic1);

            topic2.Remove();
            topic1 = topicMap.GetTopicBySubjectIdentifier(subjectIdentifier);

            Assert.Null(topic1);
        }

        [Fact]
        public void GetTopicBySubjectLocator()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var subjectLocator = topicMap.CreateLocator("http://www.example.org");
            var topic1 = topicMap.GetTopicBySubjectLocator(subjectLocator);

            Assert.Null(topic1);

            var topic2 = topicMap.CreateTopicBySubjectLocator(subjectLocator);
            topic1 = topicMap.GetTopicBySubjectLocator(subjectLocator);

            Assert.NotNull(topic1);
            Assert.Equal(topic2, topic1);

            topic2.Remove();
            topic1 = topicMap.GetTopicBySubjectLocator(subjectLocator);

            Assert.Null(topic1);
        }

        [Fact]
        public void CreateAssociation_CreateAssociationWithType()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var type = topicMap.CreateTopic();

            Assert.Empty(topicMap.Associations);

            var association = topicMap.CreateAssociation(type);

            Assert.Equal(1, topicMap.Associations.Count);
            Assert.True(topicMap.Associations.Contains(association));
            Assert.Empty(association.Roles);
            Assert.Equal(type, association.Type);
            Assert.Empty(association.Scope);
        }

        [Fact]
        public void CreateAssociation_CreateAssociationWithTypeScopeCollection()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var type = topicMap.CreateTopic();
            var theme = topicMap.CreateTopic();

            Assert.Empty(topicMap.Associations);

            var association = topicMap.CreateAssociation(type, new List<ITopic> { theme });

            Assert.Equal(1, topicMap.Associations.Count);
            Assert.True(topicMap.Associations.Contains(association));
            Assert.Empty(association.Roles);
            Assert.Equal(type, association.Type);
            Assert.Equal(1, association.Scope.Count);
            Assert.True(association.Scope.Contains(theme));
        }

        [Fact]
        public void CreateAssociation_CreateAssociationWithTypeScopeArray()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();

            Assert.Empty(topicMap.Associations);

            var association = topicMap.CreateAssociation(type, theme1, theme2);

            Assert.Equal(1, topicMap.Associations.Count);
            Assert.True(topicMap.Associations.Contains(association));
            Assert.Empty(association.Roles);
            Assert.Equal(type, association.Type);
            Assert.Equal(2, association.Scope.Count);
            Assert.True(association.Scope.Contains(theme1));
            Assert.True(association.Scope.Contains(theme2));
        }

        [Fact]
        public void CreateAssociation_UsingInvalidTypeScopeArrayThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Throws<ModelConstraintException>("Using null as association type is not allowed.", () => topicMap.CreateAssociation(null));
        }

        [Fact]
        public void CreateAssociation_UsingInvalidTypeScopeCollectionThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Throws<ModelConstraintException>("Using null as association type is not allowed.", () => topicMap.CreateAssociation(null, new List<ITopic> { topicMap.CreateTopic() }));
        }

        [Fact]
        public void CreateAssociation_UsingTypeInvalidScopeArrayThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Throws<ModelConstraintException>("Using null as association type is not allowed.", () => topicMap.CreateAssociation(null, null));
        }

        [Fact]
        public void CreateAssociation_UsingTypeInvalidScopeCollectionThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Assert.Throws<ModelConstraintException>("Using null as association type is not allowed.", () => topicMap.CreateAssociation(null, (IList<ITopic>)null));
        }

        [Fact]
        public void GetIndex_UsingInvalidIndexClassThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            //  TODO:   hier noch einen spezielleren exception-typ verwenden?
            //          IndexNotSupportedException
            //          alles andere wirkt so nach "ein fehler ist aufgetreten bitte melden sie sich beim katasteramt"...
            Assert.Throws<TMAPIException>("", () => topicMap.GetIndex<UnknownIndex>());
        }

        private class UnknownIndex : IIndex
        {
            public bool AutoUpdated { get { return false; }}
            public bool IsOpen { get { return false; }}
            public void Close() {}
            public void Open() {}
            public void Reindex() {}
        }
        #endregion
        #endregion
    }
}