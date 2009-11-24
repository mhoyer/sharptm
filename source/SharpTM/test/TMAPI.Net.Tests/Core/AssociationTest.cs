// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssociationTest.cs">
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
//   Defines the AssociationTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using System;
    using Net.Core;
    using Xunit;

    public class AssociationTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        [Fact]
        public void TestAssociationParentRelationship()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);

            Assert.Empty(topicMap.Associations);

            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            Assert.Equal(topicMap, association.Parent);
            Assert.Equal(1, topicMap.Associations.Count);
            Assert.True(topicMap.Associations.Contains(association));

            association.Remove();

            Assert.Empty(topicMap.Associations);
        }

        [Fact]
        public void CreateRole_TestRoleTypeAndPlayer()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var roleType = topicMap.CreateTopic();
            var player = topicMap.CreateTopic();

            Assert.Empty(association.Roles);
            Assert.Equal(0, player.RolesPlayed.Count);

            var role = association.CreateRole(roleType, player);

            Assert.Equal(roleType, role.Type);
            Assert.Equal(player, role.Player);
            Assert.Equal(1, player.RolesPlayed.Count);
            Assert.True(player.RolesPlayed.Contains(role));
        }

        [Fact]
        public void CreateRole_UsingInvalidPlayerThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            var e = Assert.Throws<ModelConstraintException>("Using null as role player is not allowed.", () => association.CreateRole(topicMap.CreateTopic(), null));
            Assert.Equal(association, e.Reporter);
        }

        [Fact]
        public void CreateRole_UsingInvalidTypeThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            var e = Assert.Throws<ModelConstraintException>("Using null as role type is not allowed.", () => association.CreateRole(null, topicMap.CreateTopic()));
            Assert.Equal(association, e.Reporter);
        }

        [Fact]
        public void RoleTypes_TestDifferentRoleTypes()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var type1 = topicMap.CreateTopic();
            var type2 = topicMap.CreateTopic();

            Assert.Empty(association.RoleTypes);

            var role1 = association.CreateRole(type1, topicMap.CreateTopic());

            Assert.Equal(1, association.RoleTypes.Count);
            Assert.True(association.RoleTypes.Contains(type1));

            var role2 = association.CreateRole(type2, topicMap.CreateTopic());

            Assert.Equal(2, association.RoleTypes.Count);
            Assert.True(association.RoleTypes.Contains(type1));
            Assert.True(association.RoleTypes.Contains(type2));

            var role3 = association.CreateRole(type2, topicMap.CreateTopic());

            Assert.Equal(2, association.RoleTypes.Count);
            Assert.True(association.RoleTypes.Contains(type1));
            Assert.True(association.RoleTypes.Contains(type2));

            role3.Remove();

            Assert.Equal(2, association.RoleTypes.Count);
            Assert.True(association.RoleTypes.Contains(type1));
            Assert.True(association.RoleTypes.Contains(type2));

            role2.Remove();

            Assert.Equal(1, association.RoleTypes.Count);
            Assert.True(association.RoleTypes.Contains(type1));
            Assert.False(association.RoleTypes.Contains(type2));

            role1.Remove();

            Assert.Empty(association.RoleTypes);
        }

        [Fact]
        public void GetRoles_TestRoleFilter()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var type1 = topicMap.CreateTopic();
            var type2 = topicMap.CreateTopic();
            var unusedType = topicMap.CreateTopic();

            Assert.Empty(association.GetRolesByTopicType(type1));
            Assert.Empty(association.GetRolesByTopicType(type2));
            Assert.Empty(association.GetRolesByTopicType(unusedType));

            var role1 = association.CreateRole(type1, topicMap.CreateTopic());

            Assert.Equal(1, association.GetRolesByTopicType(type1).Count);
            Assert.True(association.GetRolesByTopicType(type1).Contains(role1));
            Assert.Empty(association.GetRolesByTopicType(type2));
            Assert.Empty(association.GetRolesByTopicType(unusedType));

            var role2 = association.CreateRole(type2, topicMap.CreateTopic());

            Assert.Equal(1, association.GetRolesByTopicType(type2).Count);
            Assert.True(association.GetRolesByTopicType(type2).Contains(role2));

            var role3 = association.CreateRole(type2, topicMap.CreateTopic());

            Assert.Equal(2, association.GetRolesByTopicType(type2).Count);
            Assert.True(association.GetRolesByTopicType(type2).Contains(role2));
            Assert.True(association.GetRolesByTopicType(type2).Contains(role3));
            Assert.Empty(association.GetRolesByTopicType(unusedType));

            role3.Remove();

            Assert.Equal(1, association.GetRolesByTopicType(type2).Count);
            Assert.True(association.GetRolesByTopicType(type2).Contains(role2));

            role2.Remove();

            Assert.Empty(association.GetRolesByTopicType(type2));

            role1.Remove();

            Assert.Empty(association.GetRolesByTopicType(type1));
            Assert.Empty(association.GetRolesByTopicType(unusedType));
        }

        [Fact]
        public void GetRoles_UsingInvalidTypeThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            Assert.Throws<ArgumentNullException>("Using null as role type is not allowed.", () => association.GetRolesByTopicType(null));
        }

        [Fact]
        public void GetRoles_TwoRolesTest()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var type = topicMap.CreateTopic();
            
            var player1 = topicMap.CreateTopic();
            var player2 = topicMap.CreateTopic();

            var roletype1 = topicMap.CreateTopic();
            var roletype2 = topicMap.CreateTopic();
            
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            association.CreateRole(roletype1, player1);
            association.CreateRole(roletype2, player2);
        }
        #endregion
    }
}