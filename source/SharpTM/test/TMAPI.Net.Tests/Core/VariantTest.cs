// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VariantTest.cs">
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
//   Defines the VariantTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Xunit;

    public class VariantTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        [Fact]
        public void TestVariantParentRelationship()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var parent = topic.CreateName("Name");

            Assert.Empty(parent.Variants);

            var variant = parent.CreateVariant("Variant", topicMap.CreateTopic());

            Assert.Equal(parent, variant.Parent);
            Assert.Equal(1, parent.Variants.Count);
            Assert.True(parent.Variants.Contains(variant));

            variant.Remove();

            Assert.Empty(parent.Variants);
        }

        [Fact]
        public void Scope_VariantScopeContainsNameScope()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var nameTheme1 = topicMap.CreateTopic();
            var name = topic.CreateName("Name", nameTheme1);

            Assert.Equal(1, name.Scope.Count);
            Assert.True(name.Scope.Contains(nameTheme1));

            var variantTheme = topicMap.CreateTopic();
            var variant = name.CreateVariant("Variant", variantTheme);

            Assert.NotNull(variant);
            Assert.Equal(2, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(nameTheme1));
            Assert.True(variant.Scope.Contains(variantTheme));

            var nameTheme2 = topicMap.CreateTopic();
            name.AddTheme(nameTheme2);

            Assert.Equal(2, name.Scope.Count);
            Assert.True(name.Scope.Contains(nameTheme1));
            Assert.True(name.Scope.Contains(nameTheme2));
            Assert.Equal(3, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(nameTheme1));
            Assert.True(variant.Scope.Contains(nameTheme2));
            Assert.True(variant.Scope.Contains(variantTheme));

            name.RemoveTheme(nameTheme2);

            Assert.Equal(1, name.Scope.Count);
            Assert.True(name.Scope.Contains(nameTheme1));
            Assert.Equal(2, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(nameTheme1));
            Assert.True(variant.Scope.Contains(variantTheme));

            name.RemoveTheme(nameTheme1);

            Assert.Empty(name.Scope);
            Assert.Equal(1, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(variantTheme));
        }

        [Fact]
        public void TestIfVariantThemeEqualToANameThemeStaysIfNameThemeIsRemoved()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var nameTheme = topicMap.CreateTopic();
            var variantTheme = topicMap.CreateTopic();
            var name = topicMap.CreateTopic().CreateName("Name", nameTheme);

            Assert.Equal(1, name.Scope.Count);
            Assert.True(name.Scope.Contains(nameTheme));

            var variant = name.CreateVariant("Variant", nameTheme, variantTheme);

            Assert.NotNull(variant);
            Assert.Equal(2, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(nameTheme));
            Assert.True(variant.Scope.Contains(variantTheme));

            name.RemoveTheme(nameTheme);

            Assert.Empty(name.Scope);
            Assert.Equal(2, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(nameTheme));
            Assert.True(variant.Scope.Contains(variantTheme));
        }

        [Fact]
        public void TestIfVariantThemeEqualToANameThemeStaysIfVariantThemeIsRemoved()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var nameTheme = topicMap.CreateTopic();
            var variantTheme = topicMap.CreateTopic();
            var name = topicMap.CreateTopic().CreateName("Name", nameTheme);

            Assert.Equal(1, name.Scope.Count);
            Assert.True(name.Scope.Contains(nameTheme));

            var variant = name.CreateVariant("Variant", nameTheme, variantTheme);

            Assert.NotNull(variant);
            Assert.Equal(2, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(nameTheme));
            Assert.True(variant.Scope.Contains(variantTheme));

            variant.RemoveTheme(nameTheme);

            Assert.Equal(2, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(nameTheme));
            Assert.True(variant.Scope.Contains(variantTheme));

            name.RemoveTheme(nameTheme);

            Assert.Empty(name.Scope);
            Assert.Equal(1, variant.Scope.Count);
            Assert.False(variant.Scope.Contains(nameTheme));
            Assert.True(variant.Scope.Contains(variantTheme));
        }
        #endregion
    }
}