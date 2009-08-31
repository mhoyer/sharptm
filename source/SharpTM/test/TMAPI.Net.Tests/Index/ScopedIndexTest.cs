using System.Collections.Generic;
using TMAPI.Net.Core;
using TMAPI.Net.Index;
using Xunit;

namespace TMAPI.Net.Tests.Index
{
    public class ScopedIndexTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        #endregion

        #region Tests
        private static void UpdateIndex(IIndex index)
        {
            if (!index.AutoUpdated)
            {
                index.Reindex();
            }
        }

        [Fact]
        public void TestAssociation()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (IScopedIndex)topicMap.GetIndex<IScopedIndex>();
            var theme = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetAssociations(null));
            Assert.Empty(index.GetAssociations(theme));
            Assert.Empty(index.AssociationThemes);

            var scopedAssociation = topicMap.CreateAssociation(topicMap.CreateTopic());
            UpdateIndex(index);

            Assert.Empty(scopedAssociation.Scope);
            Assert.Equal(1, index.GetAssociations(null).Count);
            Assert.True(index.GetAssociations(null).Contains(scopedAssociation));
            Assert.False(index.AssociationThemes.Contains(theme));

            scopedAssociation.AddTheme(theme);
            UpdateIndex(index);

            Assert.Empty(index.GetAssociations(null));
            Assert.Equal(1, index.AssociationThemes.Count);
            Assert.True(index.AssociationThemes.Contains(theme));
            Assert.Equal(1, index.GetAssociations(theme).Count);
            Assert.True(index.GetAssociations(theme).Contains(scopedAssociation));

            scopedAssociation.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetAssociations(null));
            Assert.Empty(index.GetAssociations(theme));
        }

        [Fact]
        public void TestOccurrence()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (IScopedIndex)topicMap.GetIndex<IScopedIndex>();
            var theme = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(null));
            Assert.Empty(index.GetOccurrences(theme));
            Assert.Empty(index.OccurrenceThemes);

            var scopedOccurrence = topicMap.CreateTopic().CreateOccurrence(topicMap.CreateTopic(), "Occurrence");
            UpdateIndex(index);

            Assert.Empty(scopedOccurrence.Scope);
            Assert.Equal(1, index.GetOccurrences(null).Count);
            Assert.True(index.GetOccurrences(null).Contains(scopedOccurrence));
            Assert.False(index.OccurrenceThemes.Contains(theme));

            scopedOccurrence.AddTheme(theme);
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(null));
            Assert.Equal(1, index.OccurrenceThemes.Count);
            Assert.True(index.OccurrenceThemes.Contains(theme));
            Assert.Equal(1, index.GetOccurrences(theme).Count);
            Assert.True(index.GetOccurrences(theme).Contains(scopedOccurrence));

            scopedOccurrence.Remove();
            UpdateIndex(index);

            Assert.Empty(index.OccurrenceThemes);
            Assert.Empty(index.GetOccurrences(theme));
        }

        [Fact]
        public void TestName()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (IScopedIndex)topicMap.GetIndex<IScopedIndex>();
            var theme = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(null));
            Assert.Empty(index.GetNames(theme));
            Assert.Empty(index.NameThemes);

            var scopedName = topicMap.CreateTopic().CreateName("Name");
            UpdateIndex(index);

            Assert.Empty(scopedName.Scope);
            Assert.Equal(1, index.GetNames(null).Count);
            Assert.True(index.GetNames(null).Contains(scopedName));
            Assert.False(index.NameThemes.Contains(theme));

            scopedName.AddTheme(theme);
            UpdateIndex(index);

            Assert.Empty(index.GetNames(null));
            Assert.Equal(1, index.NameThemes.Count);
            Assert.True(index. NameThemes.Contains(theme));
            Assert.Equal(1, index.GetNames(theme).Count);
            Assert.True(index.GetNames(theme).Contains(scopedName));

            scopedName.Remove();
            UpdateIndex(index);

            Assert.Empty(index.NameThemes);
            Assert.Empty(index.GetNames(theme));
        }

        [Fact]
        public void TestName_NameCreatedWithScopeCollection()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (IScopedIndex)topicMap.GetIndex<IScopedIndex>();
            var theme = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(null));
            Assert.Empty(index.GetNames(theme));
            Assert.Empty(index.NameThemes);

            var scopedName = topicMap.CreateTopic().CreateName("Name", new List<ITopic> { theme });
            UpdateIndex(index);

            Assert.Equal(1, scopedName.Scope.Count);
            Assert.Empty(index.GetNames(null));
            Assert.Equal(1, index.NameThemes.Count);
            Assert.True(index.NameThemes.Contains(theme));
            Assert.Equal(1, index.GetNames(theme).Count);
            Assert.True(index.GetNames(theme).Contains(scopedName));

            scopedName.Remove();
            UpdateIndex(index);

            Assert.Empty(index.NameThemes);
            Assert.Empty(index.GetNames(theme));
            Assert.Empty(index.GetNames(null));
        }

        [Fact]
        public void TestVariant()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (IScopedIndex)topicMap.GetIndex<IScopedIndex>();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(theme1));
            Assert.Empty(index.VariantThemes);

            var parent = topicMap.CreateTopic().CreateName("Name");
            var scopedVariant = parent.CreateVariant("Variant", theme1);
            UpdateIndex(index);

            Assert.Empty(parent.Scope);
            Assert.Equal(1, scopedVariant.Scope.Count);

            Assert.Equal(1, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme1));
            Assert.Equal(1, index.GetVariants(theme1).Count);
            Assert.True(index.GetVariants(theme1).Contains(scopedVariant));

            parent.AddTheme(theme2);
            UpdateIndex(index);

            Assert.Equal(1, parent.Scope.Count);
            Assert.Equal(2, scopedVariant.Scope.Count);
            Assert.Equal(2, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme1));
            Assert.True(index.VariantThemes.Contains(theme2));
            Assert.Equal(1, index.GetVariants(theme1).Count);
            Assert.True(index.GetVariants(theme1).Contains(scopedVariant));
            Assert.Equal(1, index.GetVariants(theme2).Count);
            Assert.True(index.GetVariants(theme2).Contains(scopedVariant));

            parent.RemoveTheme(theme2);
            UpdateIndex(index);

            Assert.Empty(parent.Scope);
            Assert.Equal(1, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme1));
            Assert.Equal(1, index.GetVariants(theme1).Count);
            Assert.True(index.GetVariants(theme1).Contains(scopedVariant));
            Assert.Empty(index.GetVariants(theme2));

            scopedVariant.AddTheme(theme2);
            UpdateIndex(index);

            Assert.Equal(2, scopedVariant.Scope.Count);
            Assert.Equal(2, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme1));
            Assert.True(index.VariantThemes.Contains(theme2));
            Assert.Equal(1, index.GetVariants(theme1).Count);
            Assert.True(index.GetVariants(theme1).Contains(scopedVariant));
            Assert.Equal(1, index.GetVariants(theme2).Count);
            Assert.True(index.GetVariants(theme2).Contains(scopedVariant));

            parent.AddTheme(theme2);
            UpdateIndex(index);

            Assert.Equal(2, scopedVariant.Scope.Count);
            Assert.Equal(2, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme1));
            Assert.True(index.VariantThemes.Contains(theme2));
            Assert.Equal(1, index.GetVariants(theme1).Count);
            Assert.True(index.GetVariants(theme1).Contains(scopedVariant));
            Assert.Equal(1, index.GetVariants(theme2).Count);
            Assert.True(index.GetVariants(theme2).Contains(scopedVariant));

            parent.RemoveTheme(theme2);
            UpdateIndex(index);

            Assert.Equal(2, scopedVariant.Scope.Count);
            Assert.Equal(2, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme1));
            Assert.True(index.VariantThemes.Contains(theme2));
            Assert.Equal(1, index.GetVariants(theme1).Count);
            Assert.True(index.GetVariants(theme1).Contains(scopedVariant));
            Assert.Equal(1, index.GetVariants(theme2).Count);
            Assert.True(index.GetVariants(theme2).Contains(scopedVariant));

            scopedVariant.RemoveTheme(theme2);
            UpdateIndex(index);

            Assert.Equal(1, scopedVariant.Scope.Count);
            Assert.Equal(1, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme1));
            Assert.Equal(1, index.GetVariants(theme1).Count);
            Assert.True(index.GetVariants(theme1).Contains(scopedVariant));
        }

        [Fact]
        public void TestVariant_VariantCreatedWithScopedParent()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (IScopedIndex)topicMap.GetIndex<IScopedIndex>();
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(theme1));
            Assert.Empty(index.GetVariants(theme2));
            Assert.Empty(index.VariantThemes);

            var parent = topicMap.CreateTopic().CreateName("Name", theme1);
            var scopedVariant = parent.CreateVariant("Variant", theme2);
            UpdateIndex(index);

            Assert.Equal(1, parent.Scope.Count);
            Assert.Equal(2, scopedVariant.Scope.Count);
            Assert.Equal(2, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme1));
            Assert.True(index.VariantThemes.Contains(theme2));
            Assert.Equal(1, index.GetVariants(theme1).Count);
            Assert.True(index.GetVariants(theme1).Contains(scopedVariant));
            Assert.Equal(1, index.GetVariants(theme2).Count);
            Assert.True(index.GetVariants(theme2).Contains(scopedVariant));

            parent.RemoveTheme(theme1);
            UpdateIndex(index);

            Assert.Empty(parent.Scope);
            Assert.Equal(1, index.VariantThemes.Count);
            Assert.True(index.VariantThemes.Contains(theme2));
            Assert.Empty(index.GetVariants(theme1));
            Assert.Equal(1, index.GetVariants(theme2).Count);
            Assert.True(index.GetVariants(theme2).Contains(scopedVariant));
        }
        #endregion
    }
}