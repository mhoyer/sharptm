// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SameTopicMapTests.cs">
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
//   Checks the "same topic map" constraint.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using System.Collections.Generic;
    using Net.Core;
    using Xunit;

    /// <summary>
    /// Checks the "same topic map" constraint.
    /// </summary>
    public class SameTopicMapTests : TMAPITestCase
    {
        #region Fields

        private readonly ITopicMap _tm2;

        #endregion

        #region Constructors

        public SameTopicMapTests() {
            _tm2 = TopicMapSystem.CreateTopicMap("http://topicMap2");
        }

        #endregion

        #region Tests

        [Fact]
        public void CreateAssociation_IllegalType()
        {
            // action
            Assert.ThrowsDelegate ex = () => TopicMap.CreateAssociation(_tm2.CreateTopic());

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(TopicMap, e.Reporter);
        }

        [Fact]
        public void CreateAssociation_IllegalScopeArray()
        {
            // action
            Assert.ThrowsDelegate ex = () => TopicMap.CreateAssociation(CreateTopic(), CreateTopic(), _tm2.CreateTopic());

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(TopicMap, e.Reporter);
        }

        [Fact]
        public void CreateAssociation_IllegalScopeCollection()
        {
            // setup
            var scope = new List<ITopic>
                             {
                                 CreateTopic(),
                                 _tm2.CreateTopic()
                             };

            // action
            Assert.ThrowsDelegate ex = () => TopicMap.CreateAssociation(CreateTopic(), scope);

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(TopicMap, e.Reporter);
        }

        [Fact]
        public void CreateName_IllegalType()
        {
            // arrange
            var topic = CreateTopic();

            // action
            Assert.ThrowsDelegate ex = () => topic.CreateName(_tm2.CreateTopic(), "value");

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateName_IllegalScopeArray()
        {
            // arrange
            var topic = CreateTopic();

            // action
            Assert.ThrowsDelegate ex = () => topic.CreateName(CreateTopic(), "value", CreateTopic(), _tm2.CreateTopic());

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateName_IllegalScopeCollection()
        {
            // arrange
            var scope = new List<ITopic>
                             {
                                 CreateTopic(),
                                 _tm2.CreateTopic()
                             };

            var topic = CreateTopic();

            // action
            Assert.ThrowsDelegate ex = () => topic.CreateName(CreateTopic(), "value", scope);

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateOccurrence_IllegalType()
        {
            // arrange
            var topic = CreateTopic();

            // action
            Assert.ThrowsDelegate ex = () => topic.CreateOccurrence(_tm2.CreateTopic(), "value");

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateOccurrence_IllegalScopeArray()
        {
            // arrange
            var topic = CreateTopic();

            // action
            Assert.ThrowsDelegate ex = () => topic.CreateOccurrence(CreateTopic(), "value", CreateTopic(), _tm2.CreateTopic());

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateOccurrence_IllegalScopeCollection()
        {
            // arrange
            var scope = new List<ITopic>
                             {
                                 CreateTopic(),
                                 _tm2.CreateTopic()
                             };

            var topic = CreateTopic();

            // action
            Assert.ThrowsDelegate ex = () => topic.CreateOccurrence(CreateTopic(), "value", scope);

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(topic, e.Reporter);
        }

        [Fact]
        public void CreateRole_IllegalType()
        {
            // arrange
            var association = CreateAssociation();

            // action
            Assert.ThrowsDelegate ex = () => association.CreateRole(_tm2.CreateTopic(), CreateTopic());

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(association, e.Reporter);
        }

        [Fact]
        public void CreateRole_IllegalPlayer()
        {
            // arrange
            var association = CreateAssociation();

            // action
            Assert.ThrowsDelegate ex = () => association.CreateRole(CreateTopic(), _tm2.CreateTopic());

            // assert
            var e = Assert.Throws<ModelConstraintException>("Expected a model constraint violation.", ex);
            Assert.Equal(association, e.Reporter);
        }

        private void AddIllegalTheme(IScoped scoped)
        {
            // action
            Assert.ThrowsDelegate ex = () => scoped.AddTheme(_tm2.CreateTopic());

            // assert
            var e = Assert.Throws<ModelConstraintException>("Adding a theme from another topic map shouldn't be allowed.", ex);
            Assert.Equal(scoped, e.Reporter);
        }

        [Fact]
        public void Association_AddIllegalTheme()
        {
            AddIllegalTheme(CreateAssociation());
        }

        [Fact]
        public void Occurrence_AddIllegalTheme()
        {
            AddIllegalTheme(CreateOccurrence());
        }

        [Fact]
        public void Name_AddIllegalTheme()
        {
            AddIllegalTheme(CreateName());
        }

        [Fact]
        public void Variant_AddIllegalTheme()
        {
            AddIllegalTheme(CreateVariant());
        }

        private void SetIllegalType(ITyped typed)
        {
            // action
            Assert.ThrowsDelegate ex = () => typed.Type = _tm2.CreateTopic();

            // assert
            var e = Assert.Throws<ModelConstraintException>("Setting the type to a topic from another topic map shouldn't be allowed.", ex);
            Assert.Equal(typed, e.Reporter);
        }

        [Fact]
        public void Association_SetIllegalType()
        {
            SetIllegalType(CreateAssociation());
        }

        [Fact]
        public void Role_SetIllegalType()
        {
            SetIllegalType(CreateRole());
        }

        [Fact]
        public void Occurrence_SetIllegalType()
        {
            SetIllegalType(CreateOccurrence());
        }

        [Fact]
        public void Name_SetIllegalType()
        {
            SetIllegalType(CreateName());
        }

        [Fact]
        public void Role_SetIllegalPlayer()
        {
            // arrange
            var role = CreateRole();

            // action
            Assert.ThrowsDelegate ex = () => role.Player = _tm2.CreateTopic();

            // assert
            var e = Assert.Throws<ModelConstraintException>("Setting the player to a topic of another topic map shouldn't be allowed.", ex);
            Assert.Equal(role, e.Reporter);
        }

        private void SetIllegalReifier(IReifiable reifiable)
        {
            // action
            Assert.ThrowsDelegate ex = () => reifiable.Reifier = _tm2.CreateTopic();

            // assert
            var e = Assert.Throws<ModelConstraintException>("Setting the reifier to a topic of another topic map shouldn't be allowed.", ex);
            Assert.Equal(reifiable, e.Reporter);
        }

        [Fact]
        public void TopicMap_SetIllegalReifier()
        {
            SetIllegalReifier(TopicMap);
        }

        [Fact]
        public void Association_SetIllegalReifier()
        {
            SetIllegalReifier(CreateAssociation());
        }

        [Fact]
        public void Role_SetIllegalReifier()
        {
            SetIllegalReifier(CreateRole());
        }

        [Fact]
        public void Occurrence_SetIllegalReifier()
        {
            SetIllegalReifier(CreateOccurrence());
        }

        [Fact]
        public void Name_SetIllegalReifier()
        {
            SetIllegalReifier(CreateName());
        }

        [Fact]
        public void Variant_SetIllegalReifier()
        {
            SetIllegalReifier(CreateVariant());
        }

        [Fact]
        public void Topic_AddIllegalType()
        {
            // arrange
            var topic = CreateTopic();

            // action
            Assert.ThrowsDelegate ex = () => topic.AddType(_tm2.CreateTopic());

            // assert
            var e = Assert.Throws<ModelConstraintException>("The type is not from the same topic map. Disallowed.", ex);
            Assert.Equal(topic, e.Reporter);
        }

        #endregion
    }
}
