using TMAPI.Net.Core;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class ScopedTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        private static void Scope_AddScopeAndUsingInvalidScopeThrowsException(IScoped scoped)
        {
            var scopeSize = (scoped is IVariant) ? scoped.Scope.Count : 0;

            Assert.Equal(scopeSize, scoped.Scope.Count);

            var topicMap = scoped.TopicMap;
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();

            scoped.AddTheme(theme1);
            scopeSize++;

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.True(scoped.Scope.Contains(theme1));
            Assert.False(scoped.Scope.Contains(theme2));

            scoped.AddTheme(theme2);
            scopeSize++;

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.True(scoped.Scope.Contains(theme1));
            Assert.True(scoped.Scope.Contains(theme2));

            //  add theme1 again, themes arent't allowed to be added multiple times
            scoped.AddTheme(theme1);

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.True(scoped.Scope.Contains(theme1));
            Assert.True(scoped.Scope.Contains(theme2));

            scoped.RemoveTheme(theme2);
            scopeSize--;

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.True(scoped.Scope.Contains(theme1));
            Assert.False(scoped.Scope.Contains(theme2));

            scoped.RemoveTheme(theme1);
            scopeSize--;

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.Throws<ModelConstraintException>("Using null as scope is not allowed.", () => scoped.AddTheme(null));
        }

        [Fact]
        public void TestAssociation()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

            Scope_AddScopeAndUsingInvalidScopeThrowsException(topicMap.CreateAssociation(topicMap.CreateTopic()));
        }

        [Fact]
        public void TestOccurrence()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            Scope_AddScopeAndUsingInvalidScopeThrowsException(topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence"));
        }

        [Fact]
        public void TestName()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            Scope_AddScopeAndUsingInvalidScopeThrowsException(topic.CreateName("Name"));
        }

        [Fact]
        public void TestVariant()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("Name");

            Scope_AddScopeAndUsingInvalidScopeThrowsException(name.CreateVariant("Variant", topicMap.CreateTopic()));
        }
        #endregion
    }
}