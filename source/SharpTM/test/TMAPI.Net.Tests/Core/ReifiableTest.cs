// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReifiableTest.cs">
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
//   Defines the ReifiableTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Net.Core;
    using Xunit;

    public class ReifiableTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        private static void TestReification(IReifiable reifiable)
        {
            var topicMap = reifiable.TopicMap;
            var reifier = topicMap.CreateTopic();

            Assert.Null(reifiable.Reifier);
            Assert.Null(reifier.Reified);

            reifiable.Reifier = reifier;

            Assert.Equal(reifier, reifiable.Reifier);
            Assert.Equal(reifiable, reifier.Reified);

            reifiable.Reifier = null;

            Assert.Null(reifiable.Reifier);
            Assert.Null(reifier.Reified);

            TestReificationWithSameReifier(reifiable);
        }

        /// <summary>
        /// Tests the duplicate reification with same reifier.
        /// </summary>
        /// <remarks>
        /// Assigning the *same* reifier is allowed, the TM processor MUST NOT raise an exception.
        /// </remarks>
        /// <param name="reifiable">The reifiable.</param>
        private static void TestReificationWithSameReifier(IReifiable reifiable)
        {
            var topicMap = reifiable.TopicMap;
            var reifier = topicMap.CreateTopic();

            reifiable.Reifier = reifier;

            Assert.Equal(reifier, reifiable.Reifier);
            Assert.Equal(reifiable, reifier.Reified);

            Assert.DoesNotThrow(delegate() { reifiable.Reifier = reifier; });
        }

        private static void TestReificationCollosion(IReifiable reifiable)
        {
            var topicMap = reifiable.TopicMap;
            var reifier = topicMap.CreateTopic();

            Assert.Null(reifiable.Reifier);
            Assert.Null(reifier.Reified);

            var otherReifiable = topicMap.CreateAssociation(topicMap.CreateTopic());
            otherReifiable.Reifier = reifier;

            Assert.Equal(reifier, otherReifiable.Reifier);
            Assert.Equal(otherReifiable, reifier.Reified);

            var e = Assert.Throws<ModelConstraintException>("The reifier reifies already another construct.", () => reifiable.Reifier = reifier);
            Assert.Equal(reifiable, e.Reporter);

            otherReifiable.Reifier = null;

            Assert.Null(otherReifiable.Reifier);
            Assert.Null(reifier.Reified);

            reifiable.Reifier = reifier;

            Assert.Equal(reifier, reifiable.Reifier);
            Assert.Equal(reifiable, reifier.Reified);
        }

        [Fact]
        public void TestTopicMap()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            TestReification(topicMap);
        }

        [Fact]
        public void TestTopicMapReifierCollosion()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            TestReificationCollosion(topicMap);
        }

        [Fact]
        public void TestAssociation()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            TestReification(association);
        }

        [Fact]
        public void TestAssociationReifierCollosion()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            TestReificationCollosion(association);
        }

        [Fact]
        public void TestRole()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var role = association.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic());

            TestReification(role);
        }

        [Fact]
        public void TestRoleReifierCollosion()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var role = association.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic());

            TestReificationCollosion(role);
        }

        [Fact]
        public void TestOccurrence()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var occurrence = topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence");

            TestReification(occurrence);
        }

        [Fact]
        public void TestOccurrenceReifierCollosion()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var occurrence = topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence");

            TestReificationCollosion(occurrence);
        }

        [Fact]
        public void TestName()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("Name");

            TestReification(name);
        }

        [Fact]
        public void TestNameReifierCollosion()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("Name");

            TestReificationCollosion(name);
        }

        [Fact]
        public void TestVariant()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("Name");
            var variant = name.CreateVariant("Variant", topicMap.CreateTopic());

            TestReification(variant);
        }

        [Fact]
        public void TestVariantReifierCollosion()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("Name");
            var variant = name.CreateVariant("Variant", topicMap.CreateTopic());

            TestReificationCollosion(variant);
        }
        #endregion
    }
}