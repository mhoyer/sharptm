// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceTest.cs">
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
//   Defines the OccurrenceTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Xunit;

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
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
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