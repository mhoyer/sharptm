using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class OccurrenceTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        public static readonly string TestLocator1 = TestTM1 + "/locator1";
        public static readonly string TestLocator2 = TestTM1 + "/locator2";
        #endregion

        #region Tests
        [Fact]
        public void TestOccurrenceParentRelationship()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();

            Assert.Empty(parent.Occurrences);

            var occurrence = parent.CreateOccurrence(topicMap.CreateTopic(), "Occurrence");

            Assert.Equal(parent, occurrence.Parent);
            Assert.Equal(1, parent.Occurrences.Count);
            Assert.True(parent.Occurrences.Contains(occurrence));

            occurrence.Remove();

            Assert.Empty(parent.Occurrences);
        }
        #endregion
    }
}