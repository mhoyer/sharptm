// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleTest.cs">
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
//   Defines the RoleTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Net.Core;
    using Xunit;

    public class RoleTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        [Fact]
        public void TestRoleTypeChanged()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
			
            var initialType = topicMap.CreateTopic();
            var modifiedType = topicMap.CreateTopic();

            var role = association.CreateRole(initialType, topicMap.CreateTopic());

            Assert.Equal(initialType, role.Type);
            Assert.Contains(initialType, association.RoleTypes);
            Assert.DoesNotContain(modifiedType, association.RoleTypes);

            role.Type = modifiedType;

            Assert.Equal(modifiedType, role.Type);
            Assert.Contains(modifiedType, association.RoleTypes);
            Assert.DoesNotContain(initialType, association.RoleTypes);
        }

        #region Tests
        [Fact]
        public void TestRoleParentRelationship()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateAssociation(topicMap.CreateTopic());

            Assert.Empty(parent.Roles);

            var role = parent.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic());

            Assert.Equal(parent, role.Parent);
            Assert.Equal(1, parent.Roles.Count);
            Assert.True(parent.Roles.Contains(role));

            role.Remove();

            Assert.Empty(parent.Roles);
        }

        [Fact]
        public void Player_GetAndSetRolePlayer()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());

            Assert.Empty(association.Roles);

            var roleType = topicMap.CreateTopic();
            var player = topicMap.CreateTopic();
            var role = association.CreateRole(roleType, player);

            Assert.Equal(roleType, role.Type);
            Assert.Equal(player, role.Player);
            Assert.True(player.RolesPlayed.Contains(role));

            var player2 = topicMap.CreateTopic();

            role.Player = player2;

            Assert.Equal(player2, role.Player);
            Assert.True(player2.RolesPlayed.Contains(role));
            Assert.Empty(player.RolesPlayed);

            role.Player = player;

            Assert.Equal(player, role.Player);
        }

        [Fact]
        public void Player_UsingInvalidPlayerThrowsException()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var association = topicMap.CreateAssociation(topicMap.CreateTopic());
            var role = association.CreateRole(topicMap.CreateTopic(), topicMap.CreateTopic());

            var e = Assert.Throws<ModelConstraintException>("Using null as player for a role is not allowed.", () => role.Player = null);
            Assert.Equal(role, e.Reporter);
        }
        #endregion
    }
}