// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopicTest.cs">
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
//   Defines the TopicTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using System;
    using System.Collections.Generic;
    using Net.Core;
    using Xunit;

    public class TopicTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        public static readonly string TestLocator1 = TestTM1 + "/locator1";
        public static readonly string TestLocator2 = TestTM1 + "/locator2";
        #endregion

        #region Tests
        [Fact]
        public void TestTopicParentRelationship()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            
            Assert.Empty(topicMap.Topics);
            
            var topic = topicMap.CreateTopic();
            Assert.Equal(topicMap, topic.Parent);
            Assert.Equal(1, topicMap.Topics.Count);
            Assert.True(topicMap.Topics.Contains(topic));

            topic.Remove();

            Assert.Empty(topicMap.Topics);
        }

        [Fact]
        public void AddSubjectIdentifier_UsingInvalidIdentifierThrowsEcxeption()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null as subject identifier is not allowed.", () => topic.AddSubjectIdentifier(null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void AddSubjectLocator_UsingInvalidIdentifierThrowsEcxeption()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null as subject locator is not allowed.", () => topic.AddSubjectLocator(null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void SubjectIdentifiers_AddAndDeleteSubjectIdentifiers()
        {
            var subjectIdentifier1 = TopicMapSystem.CreateLocator(TestLocator1);
            var subjectIdentifier2 = TopicMapSystem.CreateLocator(TestLocator2);
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopicBySubjectIdentifier(subjectIdentifier1);

            Assert.Equal(1, topic.SubjectIdentifiers.Count);
            Assert.True(topic.SubjectIdentifiers.Contains(subjectIdentifier1));

            topic.AddSubjectIdentifier(subjectIdentifier2);

            Assert.Equal(2, topic.SubjectIdentifiers.Count);
            Assert.True(topic.SubjectIdentifiers.Contains(subjectIdentifier2));

            topic.RemoveSubjectIdentifier(subjectIdentifier1);

            Assert.Equal(1, topic.SubjectIdentifiers.Count);
            Assert.True(topic.SubjectIdentifiers.Contains(subjectIdentifier2));
        }

        [Fact]
        public void SubjectLocators_AddAndDeleteSubjectLocators()
        {
            var subjectLocator1 = TopicMapSystem.CreateLocator(TestLocator1);
            var subjectLocator2 = TopicMapSystem.CreateLocator(TestLocator2);
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopicBySubjectLocator(subjectLocator1);

            Assert.Equal(1, topic.SubjectLocators.Count);
            Assert.True(topic.SubjectLocators.Contains(subjectLocator1));

            topic.AddSubjectLocator(subjectLocator2);

            Assert.Equal(2, topic.SubjectLocators.Count);
            Assert.True(topic.SubjectLocators.Contains(subjectLocator2));

            topic.RemoveSubjectLocator(subjectLocator1);

            Assert.Equal(1, topic.SubjectLocators.Count);
            Assert.True(topic.SubjectLocators.Contains(subjectLocator2));
        }

        [Fact]
        public void Types_AddAndDeleteTypes()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type1 = topicMap.CreateTopic();
            var type2 = topicMap.CreateTopic();

            Assert.Empty(topic.Types);

            topic.AddType(type1);

            Assert.Equal(1, topic.Types.Count);
            Assert.True(topic.Types.Contains(type1));

            topic.AddType(type2);

            Assert.Equal(2, topic.Types.Count);
            Assert.True(topic.Types.Contains(type2));

            topic.RemoveType(type1);

            Assert.Equal(1, topic.Types.Count);
            Assert.True(topic.Types.Contains(type2));

            topic.RemoveType(type2);

            Assert.Empty(topic.Types);
        }

        [Fact]
        public void Types_UsingInvalidTypeThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null as type is not allowed.", () => topic.AddType(null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void GetRolesPlayed_TestRoleFilter()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var player = topicMap.CreateTopic();
            var type1 = topicMap.CreateTopic();
            var type2 = topicMap.CreateTopic();
            var unusedType = topicMap.CreateTopic();
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            Assert.Empty(player.GetRolesPlayedByTopicType(type1));
            Assert.Empty(player.GetRolesPlayedByTopicType(type2));
            Assert.Empty(player.GetRolesPlayedByTopicType(unusedType));

            var role = association.CreateRole(type1, player);

            Assert.Equal(1, player.GetRolesPlayedByTopicType(type1).Count);
            Assert.True(player.GetRolesPlayedByTopicType(type1).Contains(role));
            Assert.Empty(player.GetRolesPlayedByTopicType(type2));
            Assert.Empty(player.GetRolesPlayedByTopicType(unusedType));

            role.Type = type2;

            Assert.Equal(1, player.GetRolesPlayedByTopicType(type2).Count);
            Assert.True(player.GetRolesPlayedByTopicType(type2).Contains(role));
            Assert.Empty(player.GetRolesPlayedByTopicType(type1));
            Assert.Empty(player.GetRolesPlayedByTopicType(unusedType));

            role.Remove();

            Assert.Empty(player.GetRolesPlayedByTopicType(type1));
            Assert.Empty(player.GetRolesPlayedByTopicType(type2));
            Assert.Empty(player.GetRolesPlayedByTopicType(unusedType));

            Assert.Throws<ArgumentNullException>("Using null for filtering roles is not allowed.", () => player.GetRolesPlayedByTopicType(null));
        }

        [Fact]
        public void GetRolesPlayed_TestRoleAssociationFilter()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var player = topicMap.CreateTopic();
            var assocType1 = topicMap.CreateTopic();
            var assocType2 = topicMap.CreateTopic();
            var roleType1 = topicMap.CreateTopic();
            var roleType2 = topicMap.CreateTopic();
            var association = topicMap.CreateAssociation(assocType1);

            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType2));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType2));

            var role1 = association.CreateRole(roleType1, player);

            Assert.Equal(1, player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType1).Count);
            Assert.True(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType1).Contains(role1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType2));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType2));

            var role2 = association.CreateRole(roleType2, player);

            Assert.Equal(1, player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType1).Count);
            Assert.True(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType1).Contains(role1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType2));
            Assert.Equal(1, player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType1).Count);
            Assert.True(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType1).Contains(role2));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType2));

            //role2.Type = roleType1;

            //// note: test must fail here as role1 and role2 were merged
            //Assert.Equal(2, player.GetRolesPlayed(roleType1, assocType1).Count);
            //Assert.True(player.GetRolesPlayed(roleType1, assocType1).Contains(role1));
            //Assert.True(player.GetRolesPlayed(roleType1, assocType1).Contains(role2));
            //Assert.Empty(player.GetRolesPlayed(roleType1, assocType2));
            //Assert.Empty(player.GetRolesPlayed(roleType2, assocType1));
            //Assert.Empty(player.GetRolesPlayed(roleType2, assocType2));

            role2.Remove();

            Assert.Equal(1, player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType1).Count);
            Assert.True(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType1).Contains(role1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType2));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType2));

            association.Remove();

            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, assocType2));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType1));
            Assert.Empty(player.GetRolesPlayedByTopicTypeAndAssociationType(roleType2, assocType2));

            Assert.Throws<ArgumentNullException>("Using null for filtering roles is not allowed.", () => player.GetRolesPlayedByTopicTypeAndAssociationType(roleType1, null));
            Assert.Throws<ArgumentNullException>("Using null for filtering roles is not allowed.", () => player.GetRolesPlayedByTopicTypeAndAssociationType(null, assocType1));
        }

        [Fact]
        public void GetOccurrences_TestOccurrenceFilter()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type1 = topicMap.CreateTopic();
            var type2 = topicMap.CreateTopic();
            var unusedType = topicMap.CreateTopic();

            Assert.Empty(topic.GetOccurrencesByTopicType(type1));
            Assert.Empty(topic.GetOccurrencesByTopicType(type2));
            Assert.Empty(topic.GetOccurrencesByTopicType(unusedType));

            var occurrence = topic.CreateOccurrence(type1, "Occurrence");

            Assert.Equal(1, topic.GetOccurrencesByTopicType(type1).Count);
            Assert.True(topic.GetOccurrencesByTopicType(type1).Contains(occurrence));
            Assert.Empty(topic.GetOccurrencesByTopicType(type2));
            Assert.Empty(topic.GetOccurrencesByTopicType(unusedType));

            occurrence.Type = type2;

            Assert.Equal(1, topic.GetOccurrencesByTopicType(type2).Count);
            Assert.True(topic.GetOccurrencesByTopicType(type2).Contains(occurrence));
            Assert.Empty(topic.GetOccurrencesByTopicType(type1));
            Assert.Empty(topic.GetOccurrencesByTopicType(unusedType));

            occurrence.Remove();

            Assert.Empty(topic.GetOccurrencesByTopicType(type1));
            Assert.Empty(topic.GetOccurrencesByTopicType(type2));
            Assert.Empty(topic.GetOccurrencesByTopicType(unusedType));

            Assert.Throws<ArgumentNullException>("Using null for filtering occurrences is not allowed.", () => topic.GetOccurrencesByTopicType(null));
        }

        [Fact]
        public void GetNames_TestNameFilter()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type1 = topicMap.CreateTopic();
            var type2 = topicMap.CreateTopic();
            var unusedType = topicMap.CreateTopic();

            Assert.Empty(topic.GetNamesByTopicType(type1));
            Assert.Empty(topic.GetNamesByTopicType(type2));
            Assert.Empty(topic.GetNamesByTopicType(unusedType));

            var name = topic.CreateName(type1, "Name");

            Assert.Equal(1, topic.GetNamesByTopicType(type1).Count);
            Assert.True(topic.GetNamesByTopicType(type1).Contains(name));
            Assert.Empty(topic.GetNamesByTopicType(type2));
            Assert.Empty(topic.GetNamesByTopicType(unusedType));

            name.Type = type2;

            Assert.Equal(1, topic.GetNamesByTopicType(type2).Count);
            Assert.True(topic.GetNamesByTopicType(type2).Contains(name));
            Assert.Empty(topic.GetNamesByTopicType(type1));
            Assert.Empty(topic.GetNamesByTopicType(unusedType));

            name.Remove();

            Assert.Empty(topic.GetNamesByTopicType(type1));
            Assert.Empty(topic.GetNamesByTopicType(type2));
            Assert.Empty(topic.GetNamesByTopicType(unusedType));

            Assert.Throws<ArgumentNullException>("Using null for filtering names is not allowed.", () => topic.GetNamesByTopicType(null));
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeString()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var value = "Occurrence";
            var dataType = topicMap.CreateLocator("http://www.w3.org/2001/XMLSchema#string");

            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value);

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Empty(occurrence.Scope);
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value, occurrence.Value);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeURI()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var value = topicMap.CreateLocator("http://www.example.org/");
            var dataType = topicMap.CreateLocator("http://www.w3.org/2001/XMLSchema#anyURI");
            
            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value);

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Empty(occurrence.Scope);
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value.Reference, occurrence.Value);
            Assert.Equal(value, occurrence.LocatorValue);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeStringExplicitDatatype()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var value = "Occurrence";
            var dataType = topicMap.CreateLocator("http://www.example.org/datatype");

            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value, dataType);

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Empty(occurrence.Scope);
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value, occurrence.Value);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeStringScopeArray()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var value = "Occurrence";
            var dataType = topicMap.CreateLocator("http://www.w3.org/2001/XMLSchema#string");

            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value, theme1, theme2);

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Equal(2, occurrence.Scope.Count);
            Assert.True(occurrence.Scope.Contains(theme1));
            Assert.True(occurrence.Scope.Contains(theme2));
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value, occurrence.Value);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeURIScopeArray()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var value = topicMap.CreateLocator("http://www.example.org/");
            var dataType = topicMap.CreateLocator("http://www.w3.org/2001/XMLSchema#anyURI");

            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value, theme1, theme2);

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Equal(2, occurrence.Scope.Count);
            Assert.True(occurrence.Scope.Contains(theme1));
            Assert.True(occurrence.Scope.Contains(theme2));
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value.Reference, occurrence.Value);
            Assert.Equal(value, occurrence.LocatorValue);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeStringExplicitDatatypeScopeArray()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var value = "Occurrence";
            var dataType = topicMap.CreateLocator("http://www.example.org/datatype");

            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value, dataType, theme1, theme2);

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Equal(2, occurrence.Scope.Count);
            Assert.True(occurrence.Scope.Contains(theme1));
            Assert.True(occurrence.Scope.Contains(theme2));
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value, occurrence.Value);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeStringScopeCollection()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var value = "Occurrence";
            var dataType = topicMap.CreateLocator("http://www.w3.org/2001/XMLSchema#string");

            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value, new List<ITopic> {theme1, theme2});

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Equal(2, occurrence.Scope.Count);
            Assert.True(occurrence.Scope.Contains(theme1));
            Assert.True(occurrence.Scope.Contains(theme2));
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value, occurrence.Value);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeURIScopeCollection()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var value = topicMap.CreateLocator("http://www.example.org/");
            var dataType = topicMap.CreateLocator("http://www.w3.org/2001/XMLSchema#anyURI");

            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value, new List<ITopic> { theme1, theme2 });

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Equal(2, occurrence.Scope.Count);
            Assert.True(occurrence.Scope.Contains(theme1));
            Assert.True(occurrence.Scope.Contains(theme2));
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value.Reference, occurrence.Value);
            Assert.Equal(value, occurrence.LocatorValue);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_CreateOccurrenceWithTypeStringExplicitDatatypeScopeCollection()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var value = "Occurrence";
            var dataType = topicMap.CreateLocator("http://www.example.org/datatype");

            Assert.Empty(topic.Occurrences);

            var occurrence = topic.CreateOccurrence(type, value, dataType, new List<ITopic> { theme1, theme2 });

            Assert.Equal(1, topic.Occurrences.Count);
            Assert.True(topic.Occurrences.Contains(occurrence));
            Assert.Equal(2, occurrence.Scope.Count);
            Assert.True(occurrence.Scope.Contains(theme1));
            Assert.True(occurrence.Scope.Contains(theme2));
            Assert.Equal(type, occurrence.Type);
            Assert.Equal(value, occurrence.Value);
            Assert.Equal(dataType, occurrence.Datatype);
            Assert.Empty(occurrence.ItemIdentifiers);
        }

        [Fact]
        public void CreateOccurrence_UsingInvalidStringThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for string value is not allowed.", () => topic.CreateOccurrence(topicMap.CreateTopic(), (string)null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateOccurrence_UsingInvalidURIThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for URI locator is not allowed.", () => topic.CreateOccurrence(topicMap.CreateTopic(), (ILocator)null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateOccurrence_UsingInvalidDatatypeThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for datatype is not allowed.", () => topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence", (ILocator)null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateOccurrence_UsingInvalidTypeThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for type is not allowed.", () => topic.CreateOccurrence(null, "Occurrence"));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateOccurrence_UsingInvalidScopeArrayThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for scope array is not allowed.", () => topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence", (ITopic[])null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateOccurrence_UsingInvalidScopeCollectionThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for scope collection is not allowed.", () => topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence", (IList<ITopic>)null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateName_CreateNameWithType()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var value = "Name";

            Assert.Empty(topic.Names);

            var name = topic.CreateName(type, value);

            Assert.Equal(1, topic.Names.Count);
            Assert.True(topic.Names.Contains(name));
            Assert.Empty(name.Scope);
            Assert.Equal(type, name.Type);
            Assert.Equal(value, name.Value);
            Assert.Empty(name.ItemIdentifiers);
        }

        [Fact]
        public void CreateName_CreateNameWithDefaultType()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var value = "Name";
            var defaultType = topicMap.CreateLocator("http://psi.topicmaps.org/iso13250/model/topic-name");

            Assert.Empty(topic.Names);

            var name = topic.CreateName(value);

            Assert.Equal(1, topic.Names.Count);
            Assert.True(topic.Names.Contains(name));
            Assert.Empty(name.Scope);
            Assert.NotNull(name.Type);
            Assert.Equal(value, name.Value);
            Assert.Empty(name.ItemIdentifiers);
            Assert.True(name.Type.SubjectIdentifiers.Contains(defaultType));
        }

        [Fact]
        public void CreateName_CreateNameWithTypeScopeArray()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var value = "Name";

            Assert.Empty(topic.Names);

            var name = topic.CreateName(type, value, theme1, theme2);

            Assert.Equal(1, topic.Names.Count);
            Assert.True(topic.Names.Contains(name));
            Assert.Equal(2, name.Scope.Count);
            Assert.True(name.Scope.Contains(theme1));
            Assert.True(name.Scope.Contains(theme2));
            Assert.Equal(type, name.Type);
            Assert.Equal(value, name.Value);
            Assert.Empty(name.ItemIdentifiers);
        }

        [Fact]
        public void CreateName_CreateNameWithDefaultTypeScopeArray()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var value = "Name";
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var defaultType = topicMap.CreateLocator("http://psi.topicmaps.org/iso13250/model/topic-name");

            Assert.Empty(topic.Names);

            var name = topic.CreateName(value, theme1, theme2);

            Assert.Equal(1, topic.Names.Count);
            Assert.True(topic.Names.Contains(name));
            Assert.Equal(2, name.Scope.Count);
            Assert.True(name.Scope.Contains(theme1));
            Assert.True(name.Scope.Contains(theme2));
            Assert.NotNull(name.Type);
            Assert.Equal(value, name.Value);
            Assert.Empty(name.ItemIdentifiers);
            Assert.True(name.Type.SubjectIdentifiers.Contains(defaultType));
        }

        [Fact]
        public void CreateName_CreateNameWithTypeScopeCollection()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var type = topicMap.CreateTopic();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var value = "Name";

            Assert.Empty(topic.Names);

            var name = topic.CreateName(type, value, new List<ITopic> { theme1, theme2 });

            Assert.Equal(1, topic.Names.Count);
            Assert.True(topic.Names.Contains(name));
            Assert.Equal(2, name.Scope.Count);
            Assert.True(name.Scope.Contains(theme1));
            Assert.True(name.Scope.Contains(theme2));
            Assert.Equal(type, name.Type);
            Assert.Equal(value, name.Value);
            Assert.Empty(name.ItemIdentifiers);
        }

        [Fact]
        public void CreateName_CreateNameWithDefaultTypeScopeCollection()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var value = "Name";
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();
            var defaultType = topicMap.CreateLocator("http://psi.topicmaps.org/iso13250/model/topic-name");

            Assert.Empty(topic.Names);

            var name = topic.CreateName(value, new List<ITopic> { theme1, theme2 });

            Assert.Equal(1, topic.Names.Count);
            Assert.True(topic.Names.Contains(name));
            Assert.Equal(2, name.Scope.Count);
            Assert.True(name.Scope.Contains(theme1));
            Assert.True(name.Scope.Contains(theme2));
            Assert.NotNull(name.Type);
            Assert.Equal(value, name.Value);
            Assert.Empty(name.ItemIdentifiers);
            Assert.True(name.Type.SubjectIdentifiers.Contains(defaultType));
        }

        [Fact]
        public void CreateName_UsingInvalidStringThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for string value is not allowed.", () => topic.CreateName(topicMap.CreateTopic(), null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateName_UsingInvalidScopeArrayThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for scope array is not allowed.", () => topic.CreateName(topicMap.CreateTopic(), "Name", null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateName_UsingInvalidScopeCollectionThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for scope collection is not allowed.", () => topic.CreateName(topicMap.CreateTopic(), "Name", (IList<ITopic>)null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateName_UsingInvalidStringWithDefaultTypeThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for string value is not allowed.", () => topic.CreateName(null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateName_UsingInvalidScopeArrayWithDefaultTypeThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for scope array is not allowed.", () => topic.CreateName("Name", null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateName_UsingInvalidScopeCollectionWithDefaultTypeThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            var e = Assert.Throws<ModelConstraintException>("Using null for scope collection is not allowed.", () => topic.CreateName("Name", (IList<ITopic>)null));
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsPlayer()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();

            Assert.Equal(1, topicMap.Topics.Count);
            topic.Remove();
            Assert.Equal(0, topicMap.Topics.Count);

            topic = topicMap.CreateTopic();
            Assert.Equal(1, topicMap.Topics.Count);

            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var role = association.CreateRole(topicMap.CreateTopic(), topic);

            Assert.Equal(3, topicMap.Topics.Count);

            Assert.ThrowsDelegate d = () => topic.Remove();
            var e = Assert.Throws<TopicInUseException>("Removing a topic used as player is not allowed.", d);
            Assert.Equal(topic, e.Reporter);

            role.Player = topicMap.CreateTopic();

            Assert.Equal(4, topicMap.Topics.Count);
            topic.Remove();
            Assert.Equal(3, topicMap.Topics.Count);
        }

        #region Remove topic used as type
        private void RemoveTopicUsedAsType(ITyped typedConstruct)
        {
            var topicMap = typedConstruct.TopicMap;
            var topicToBeRemoved = topicMap.CreateTopic();

            Assert.Contains(topicToBeRemoved, topicMap.Topics);
            Assert.DoesNotThrow(() => topicToBeRemoved.Remove());
            Assert.DoesNotContain(topicToBeRemoved, topicMap.Topics);

            topicToBeRemoved = topicMap.CreateTopic();
            Assert.Contains(topicToBeRemoved, topicMap.Topics);
            typedConstruct.Type = topicToBeRemoved;
            Assert.ThrowsDelegate d = () => topicToBeRemoved.Remove();
            var e = Assert.Throws<TopicInUseException>("Removing a topic used as type is not allowed.", d);
            Assert.Equal(topicToBeRemoved, e.Reporter);
            Assert.Contains(topicToBeRemoved, topicMap.Topics);

            typedConstruct.Type = topicMap.CreateTopic();
            Assert.DoesNotThrow(() => topicToBeRemoved.Remove());
            Assert.DoesNotContain(topicToBeRemoved, topicMap.Topics);

        }

        [Fact]
        public void Remove_RemoveTopicUsedAsTypeForAssociation()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var typedConstruct = topicMap.CreateAssociation(topicMap.CreateTopic(), topicMap.CreateTopic());

            RemoveTopicUsedAsReifier(typedConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsTypeForOccurrence()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var typedConstruct = topic.CreateOccurrence(topicMap.CreateTopic(), "MyOccurrence", topicMap.CreateTopic());

            RemoveTopicUsedAsReifier(typedConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsTypeForRole()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var associationc = topicMap.CreateAssociation(topicMap.CreateTopic(), topicMap.CreateTopic());
            var typedConstruct = associationc.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic());

            RemoveTopicUsedAsReifier(typedConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsTypeForName()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var typedConstruct = topic.CreateName("MyName", topicMap.CreateTopic());

            RemoveTopicUsedAsReifier(typedConstruct);
        }
        #endregion

        #region Remove topic used as reifier
        private void RemoveTopicUsedAsReifier(IReifiable reifiableConstruct)
        {
            var topicMap = reifiableConstruct.TopicMap;
            var topicToBeRemoved = topicMap.CreateTopic();

            Assert.Contains(topicToBeRemoved, topicMap.Topics);
            Assert.DoesNotThrow(() => topicToBeRemoved.Remove());
            Assert.DoesNotContain(topicToBeRemoved, topicMap.Topics);

            topicToBeRemoved = topicMap.CreateTopic();
            Assert.Contains(topicToBeRemoved, topicMap.Topics);
            reifiableConstruct.Reifier = topicToBeRemoved;
            Assert.ThrowsDelegate d = () => topicToBeRemoved.Remove();
            var e = Assert.Throws<TopicInUseException>("Removing a topic used as reifier is not allowed.", d);
            Assert.Equal(topicToBeRemoved, e.Reporter);
            Assert.Contains(topicToBeRemoved, topicMap.Topics);

            reifiableConstruct.Reifier = null;
            Assert.DoesNotThrow(() => topicToBeRemoved.Remove());
            Assert.DoesNotContain(topicToBeRemoved, topicMap.Topics);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsReifierForAssociation()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var reifiableConstruct = topicMap.CreateAssociation(topicMap.CreateTopic(), topicMap.CreateTopic());

            RemoveTopicUsedAsReifier(reifiableConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsReifierForOccurrence()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var reifiableConstruct = topic.CreateOccurrence(topicMap.CreateTopic(), "MyOccurrence", topicMap.CreateTopic());

            RemoveTopicUsedAsReifier(reifiableConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsReifierForRole()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic(), topicMap.CreateTopic());
            var reifiableConstruct = association.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic());

            RemoveTopicUsedAsReifier(reifiableConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsReifierForName()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var reifiableConstruct = topic.CreateName("MyName", topicMap.CreateTopic());
			
            RemoveTopicUsedAsReifier(reifiableConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsReifierForTopicMap()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);

            RemoveTopicUsedAsReifier(topicMap);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsReifierForVariant()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("MyName", topicMap.CreateTopic());
            var reifiableConstruct = name.CreateVariant("MyVariant", topicMap.CreateTopic());

            RemoveTopicUsedAsReifier(reifiableConstruct);
        }
        #endregion

        #region Remove topic used as scope theme
        private void RemoveTopicUsedAsScopeTheme(IScoped scopedConstruct)
        {
            var topicMap = scopedConstruct.TopicMap;
            var topicToBeRemoved = topicMap.CreateTopic();

            Assert.Contains(topicToBeRemoved, topicMap.Topics);
            Assert.DoesNotThrow(() => topicToBeRemoved.Remove());
            Assert.DoesNotContain(topicToBeRemoved, topicMap.Topics);

            topicToBeRemoved = topicMap.CreateTopic();
            Assert.Contains(topicToBeRemoved, topicMap.Topics);
            scopedConstruct.AddTheme(topicToBeRemoved);
            Assert.ThrowsDelegate d = () => topicToBeRemoved.Remove();
            var e = Assert.Throws<TopicInUseException>("Removing a topic used as theme is not allowed.", d);
            Assert.Equal(topicToBeRemoved, e.Reporter);
            Assert.Contains(topicToBeRemoved, topicMap.Topics);

            scopedConstruct.RemoveTheme(topicToBeRemoved);
            Assert.DoesNotThrow(() => topicToBeRemoved.Remove());
            Assert.DoesNotContain(topicToBeRemoved, topicMap.Topics);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsScopeThemeForAssociation()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var scopedConstruct = topicMap.CreateAssociation(topicMap.CreateTopic(), topicMap.CreateTopic());

            RemoveTopicUsedAsScopeTheme(scopedConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsScopeThemeForOccurrence()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var scopedConstruct = topic.CreateOccurrence(topicMap.CreateTopic(),
                                                         "MyOccurrence",
                                                         topicMap.CreateTopic());

            RemoveTopicUsedAsScopeTheme(scopedConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsScopeThemeForVariant()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("MyName", topicMap.CreateTopic());
            var scopedConstruct = name.CreateVariant("MyVariant", topicMap.CreateTopic());

            RemoveTopicUsedAsScopeTheme(scopedConstruct);
        }

        [Fact]
        public void Remove_RemoveTopicUsedAsScopeThemeForName()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var scopedConstruct = topic.CreateName("MyName", topicMap.CreateTopic());

            RemoveTopicUsedAsScopeTheme(scopedConstruct);
        }
        #endregion

        #endregion
    }
}