using TMAPI.Net.Core;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class ConstructTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
		private void ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierEmpty(IConstruct construct)
		{
			Assert.Empty(construct.ItemIdentifiers);

			ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifier(construct);

			Assert.Empty(construct.ItemIdentifiers);
		}

        private void ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierNotEmpty(IConstruct construct, ILocator initialItemIdentifier)
        {
			Assert.NotEmpty(construct.ItemIdentifiers);
			Assert.Contains(topicMapSystem.CreateLocator(TestTM1), construct.ItemIdentifiers);

			ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifier(construct);
		
			Assert.NotEmpty(construct.ItemIdentifiers);
			Assert.Contains(topicMapSystem.CreateLocator(TestTM1), construct.ItemIdentifiers);
		}

        private void ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifier(IConstruct construct)
        {
            var topicMap = construct.TopicMap;
            var itemIdentifier = topicMap.CreateLocator(TestTM1 + "/12345");
            construct.AddItemIdentifier(itemIdentifier);
            
            Assert.Contains(itemIdentifier, construct.ItemIdentifiers);
            Assert.Equal(construct, topicMap.GetConstructByItemIdentifier(itemIdentifier));
			
			if (!(construct is ITopic))
			{
				Assert.Throws<IdentityConstraintException>(() => topicMap.CreateTopicByItemIdentifier(itemIdentifier));
				Assert.Throws<IdentityConstraintException>(() => topicMap.CreateTopic().AddItemIdentifier(itemIdentifier));
			}

            construct.RemoveItemIdentifier(itemIdentifier);

            Assert.DoesNotContain(itemIdentifier, construct.ItemIdentifiers);
            Assert.Null(topicMap.GetConstructByItemIdentifier(itemIdentifier));
            Assert.Throws<ModelConstraintException>("Using null as item identifier is not allowed.", () => construct.AddItemIdentifier(null));
			
			Assert.Equal(construct, topicMap.GetConstructById(construct.Id));
		}

        [Fact]
        public void TestTopicMap()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierNotEmpty(
				topicMap, 
				topicMapSystem.CreateLocator(TestTM1));

            Assert.Null(topicMap.Parent);
            Assert.Equal(topicMap, topicMap.TopicMap);

            var id = topicMap.Id;
            Assert.Equal(topicMap, topicMap.GetConstructById(id));
        }

        [Fact]
        public void TestTopic()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopicBySubjectLocator(topicMap.CreateLocator(TestTM1 + "/12345"));

            ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierEmpty(topic);

            Assert.NotNull(topic.Parent);
            Assert.Equal(topicMap, topic.TopicMap);
        }

        [Fact]
        public void TestAssociation()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierEmpty(association);

            Assert.NotNull(association.Parent);
            Assert.Equal(topicMap, association.TopicMap);
			Assert.Empty(association.ItemIdentifiers);
        }

        [Fact]
        public void TestRole()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var role = association.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic());

            ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierEmpty(role);

            Assert.NotNull(role.Parent);
            Assert.Equal(topicMap, role.TopicMap);
			Assert.Empty(role.ItemIdentifiers);
        }

        [Fact]
        public void TestOccurrence()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var occurrence = topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence");

            ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierEmpty(occurrence);

            Assert.NotNull(occurrence.Parent);
            Assert.Equal(topicMap, occurrence.TopicMap);
        }

        [Fact]
        public void TestName()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("Name");

            ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierEmpty(name);

            Assert.NotNull(name.Parent);
            Assert.Equal(topicMap, name.TopicMap);
        }

        [Fact]
        public void TestVariant()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("Name");
            var variant = name.CreateVariant("Variant", topicMap.CreateTopic());

            ItemIdentifiers_AddRemoveAndRetrieveByItemIdentifierEmpty(variant);

            Assert.NotNull(variant.Parent);
            Assert.Equal(topicMap, variant.TopicMap);
        }
    	#endregion
    }
}