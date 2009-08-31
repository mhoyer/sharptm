using TMAPI.Net.Core;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class TypedTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        private static void Type_SetTypeAndUsingInvalidTypeThrowsException(ITyped typed)
        {
            var topicMap = typed.TopicMap;
            var type = topicMap.CreateTopic();

            ITopic oldType = typed.Type;

            Assert.NotNull(oldType);

            typed.Type = type;

            Assert.Equal(type, typed.Type);

            typed.Type = oldType;

            Assert.Equal(oldType, typed.Type);
            Assert.Throws<ModelConstraintException>("Using null for type is not allowed.", () => typed.Type = null);
        }

        [Fact]
        public void TestAssociation()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Type_SetTypeAndUsingInvalidTypeThrowsException(topicMap.CreateAssociation(topicMap.CreateTopic()));
        }

        [Fact]
        public void TestRole()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            Type_SetTypeAndUsingInvalidTypeThrowsException(association.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic())); 
        }

        [Fact]
        public void TestOccurrrence()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            Type_SetTypeAndUsingInvalidTypeThrowsException(topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence"));
        }

        [Fact]
        public void TestName()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            Type_SetTypeAndUsingInvalidTypeThrowsException(topic.CreateName("Name"));
        }
        #endregion
    }
}