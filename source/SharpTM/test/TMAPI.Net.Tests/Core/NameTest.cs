using System.Collections.Generic;
using TMAPI.Net.Core;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
    public class NameTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        public static readonly string TestLocator1 = TestTM1 + "/locator1";
        public static readonly string TestLocator2 = TestTM1 + "/locator2";
        #endregion

        #region Tests
        [Fact]
        public void TestNameParentRelationship()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();

            Assert.Empty(parent.Names);

            var name = parent.CreateName("Name");

            Assert.Equal(parent, name.Parent);
            Assert.Equal(1, parent.Names.Count);
            Assert.True(parent.Names.Contains(name));

            name.Remove();

            Assert.Empty(parent.Names);
        }

        [Fact]
        public void Value_SettingDiffrentValues()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");
            var value1 = "A name.";
            var value2 = "Another name.";

            name.Value = value1;

            Assert.Equal(value1, name.Value);

            name.Value = value2;

            Assert.Equal(value2, name.Value);
            Assert.Throws<ModelConstraintException>("Using null as string value for name is not allowed.", () => name.Value = null);
            Assert.Equal(value2, name.Value);
        }

        [Fact]
        public void CreateVariant_CreateVariantWithStringScope()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");
            var theme = topicMap.CreateTopic();
            var xsdString = topicMap.CreateLocator("http://www.w3.org/2001/XMLSchema#string");
            var variant = name.CreateVariant("Variant", theme);

            Assert.Equal("Variant", variant.Value);
            Assert.Equal(xsdString, variant.Datatype);
            Assert.Equal(1, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(theme));
        }

        [Fact]
        public void CreateVariant_CreateVariantWithURIScope()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");
            var theme = topicMap.CreateTopic();
            var xsdAnyURI = topicMap.CreateLocator("http://www.w3.org/2001/XMLSchema#anyURI");
            var value = topicMap.CreateLocator("http://www.example.org");
            var variant = name.CreateVariant(value, theme);

            Assert.Equal(value.Reference, variant.Value);
            Assert.Equal(value, variant.LocatorValue);
            Assert.Equal(xsdAnyURI, variant.Datatype);
            Assert.Equal(1, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(theme));
        }

        [Fact]
        public void CreateVariant_CreateVariantWithStringExplicitDataTypeScope()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");
            var theme = topicMap.CreateTopic();
            var dataType = topicMap.CreateLocator("http://www.example.org/datatype");
            var variant = name.CreateVariant("Variant", dataType, theme);

            Assert.Equal("Variant", variant.Value);
            Assert.Equal(dataType, variant.Datatype);
            Assert.Equal(1, variant.Scope.Count);
            Assert.True(variant.Scope.Contains(theme));
        }

        [Fact]
        public void CreateVariant_UsingInvalidStringThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");
            var theme = topicMap.CreateTopic();

            Assert.Throws<ModelConstraintException>("Using null as string value for variants is not allowed.", () => name.CreateVariant((string)null, theme));
        }

        [Fact]
        public void CreateVariant_UsingInvalidURIThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");
            var theme = topicMap.CreateTopic();

            Assert.Throws<ModelConstraintException>("Using null as string value for variants is not allowed.", () => name.CreateVariant((ILocator)null, theme));
        }

        [Fact]
        public void CreateVariant_UsingInvalidDataTypeThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");
            var theme = topicMap.CreateTopic();

            Assert.Throws<ModelConstraintException>("Using null as string value for variants is not allowed.", () => name.CreateVariant("Variant", (ILocator)null, theme));
        }

        [Fact]
        public void CreateVariant_UsingParentScopeThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");
            var theme = topicMap.CreateTopic();
            name.AddTheme(theme);

            Assert.Equal(1, name.Scope.Count);
            Assert.True(name.Scope.Contains(theme));

            Assert.Throws<ModelConstraintException>("The variant would be in the same scope as the parent.", () => name.CreateVariant("Variant", theme));
        }

        [Fact]
        public void CreateVariant_UsingEmptyScopeThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");

            Assert.Throws<ModelConstraintException>("Creation of a variant with empty scope is not allowed.", () => name.CreateVariant("Variant", new List<ITopic>()));
        }

        [Fact]
        public void CreateVariant_UsingNullScopeThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var parent = topicMap.CreateTopic();
            var name = parent.CreateName("Name");

            Assert.Throws<ModelConstraintException>("Creation of a variant with a null scope is not allowed.", () => name.CreateVariant("Variant", (ITopic[])null));
        }
        #endregion
    }
}