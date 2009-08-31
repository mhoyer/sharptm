using System;
using TMAPI.Net.Core;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class AssociationTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        [Fact]
        public void TestAssociationParentRelationship()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);

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
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
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
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            Assert.Throws<ModelConstraintException>("Using null as role player is not allowed.", () => association.CreateRole(topicMap.CreateTopic(), null));
        }

        [Fact]
        public void CreateRole_UsingInvalidTypeThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            Assert.Throws<ModelConstraintException>("Using null as role type is not allowed.", () => association.CreateRole(null, topicMap.CreateTopic()));
        }

        [Fact]
        public void RoleTypes_TestDifferentRoleTypes()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
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
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
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
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

			Assert.Throws<ArgumentNullException>("Using null as role type is not allowed.", () => association.GetRolesByTopicType(null));
        }

        [Fact]
        public void GetRoles_TwoRolesTest()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
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