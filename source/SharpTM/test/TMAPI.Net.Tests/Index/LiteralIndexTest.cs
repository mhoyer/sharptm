using System;
using TMAPI.Net.Core;
using TMAPI.Net.Index;
using Xunit;

namespace TMAPI.Net.Tests.Index
{
    public class LiteralIndexTest : TMAPITestCase
    {
        #region Static Constants
        public static readonly string TestTM1 = "mem://localhost/testm1";
        public static ILocator _xsdString;
        public static ILocator _xsdAnyURI;
        #endregion

        #region Constructor
        public LiteralIndexTest()
        {
            _xsdString = topicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#string");
            _xsdAnyURI = topicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#anyURI");
        }
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
        public void GetNames()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();
            var value1 = "Value1";
            var value2 = "Value2";

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(value1));

            var name = topicMap.CreateTopic().CreateName(value1);
            UpdateIndex(index);

            Assert.Equal(1, index.GetNames(value1).Count);
            Assert.True(index.GetNames(value1).Contains(name));

            name.Value = value2;
            UpdateIndex(index);

            Assert.Empty(index.GetNames(value1));
            Assert.Equal(1, index.GetNames(value2).Count);
            Assert.True(index.GetNames(value2).Contains(name));

            name.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(value1));
            Assert.Empty(index.GetNames(value2));
        }

        [Fact]
        public void GetNames_UsingInvalidStringThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as string value is not allowed.", () => index.GetNames(null));
        }

        [Fact]
        public void GetOccurrences_String()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();
            var value1 = "Value1";
            var value2 = "Value2";

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Empty(index.GetOccurrences(value1, _xsdString));

            var type = topicMap.CreateTopic();
            var occurrence = topicMap.CreateTopic().CreateOccurrence(type, value1);
            UpdateIndex(index);

            Assert.Equal(1, index.GetOccurrences(value1).Count);
            Assert.True(index.GetOccurrences(value1).Contains(occurrence));
            Assert.Equal(1, index.GetOccurrences(value1, _xsdString).Count);
            Assert.True(index.GetOccurrences(value1, _xsdString).Contains(occurrence));

            occurrence.Value = value2;
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Empty(index.GetOccurrences(value1, _xsdString));
            Assert.Equal(1, index.GetOccurrences(value2).Count);
            Assert.True(index.GetOccurrences(value2).Contains(occurrence));
            Assert.Equal(1, index.GetOccurrences(value2, _xsdString).Count);
            Assert.True(index.GetOccurrences(value2, _xsdString).Contains(occurrence));

            occurrence.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Empty(index.GetOccurrences(value1, _xsdString));
            Assert.Empty(index.GetOccurrences(value2));
            Assert.Empty(index.GetOccurrences(value2, _xsdString));
        }

        [Fact]
        public void GetOccurrences_URI()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();
            var value1 = topicMap.CreateLocator("http://www.example.org/1");
            var value2 = topicMap.CreateLocator("http://www.example.org/2");

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Empty(index.GetOccurrences(value1.Reference, _xsdAnyURI));

            var type = topicMap.CreateTopic();
            var occurrence = topicMap.CreateTopic().CreateOccurrence(type, value1);
            UpdateIndex(index);

            Assert.Equal(1, index.GetOccurrences(value1).Count);
            Assert.True(index.GetOccurrences(value1).Contains(occurrence));
            Assert.Equal(1, index.GetOccurrences(value1.Reference, _xsdAnyURI).Count);
            Assert.True(index.GetOccurrences(value1.Reference, _xsdAnyURI).Contains(occurrence));

            occurrence.LocatorValue = value2;
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Empty(index.GetOccurrences(value1.Reference, _xsdAnyURI));
            Assert.Equal(1, index.GetOccurrences(value2).Count);
            Assert.True(index.GetOccurrences(value2).Contains(occurrence));
            Assert.Equal(1, index.GetOccurrences(value2.Reference, _xsdAnyURI).Count);
            Assert.True(index.GetOccurrences(value2.Reference, _xsdAnyURI).Contains(occurrence));

            occurrence.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Empty(index.GetOccurrences(value1.Reference, _xsdAnyURI));
            Assert.Empty(index.GetOccurrences(value2));
            Assert.Empty(index.GetOccurrences(value2.Reference, _xsdAnyURI));
        }

        [Fact]
        public void GetOccurrences_URIExplicitDatatype()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();
            var value1 = "http://www.example.org/1";
            var value2 = "http://www.example.org/2";
            var datatype = topicMap.CreateLocator("http://www.example.org/datatype");

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Empty(index.GetOccurrences(value1, datatype));

            var type = topicMap.CreateTopic();
            var occurrence = topicMap.CreateTopic().CreateOccurrence(type, value1, datatype);
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Equal(1, index.GetOccurrences(value1, datatype).Count);
            Assert.True(index.GetOccurrences(value1, datatype).Contains(occurrence));

            occurrence.Value = value2;
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value1));
            Assert.Empty(index.GetOccurrences(value1, datatype));
            Assert.Empty(index.GetOccurrences(value2));
            Assert.Equal(1, index.GetOccurrences(value2, datatype).Count);
            Assert.True(index.GetOccurrences(value2, datatype).Contains(occurrence));

            occurrence.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(value2));
            Assert.Empty(index.GetOccurrences(value2, datatype));
        }

        [Fact]
        public void GetOccurrences_UsingInvalidStringThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as string value is not allowed.", () => index.GetOccurrences((string)null));
        }

        [Fact]
        public void GetOccurrences_UsingInvalidURIThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as locator is not allowed.", () => index.GetOccurrences((ILocator)null));
        }

        [Fact]
        public void GetOccurrences_UsingInvalidDatatypeThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as datatype is not allowed.", () => index.GetOccurrences("Value", null));
        }

        [Fact]
        public void GetVariants_String()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();
            var value1 = "Value1";
            var value2 = "Value2";

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Empty(index.GetVariants(value1, _xsdString));

            var theme = topicMap.CreateTopic();
            var variant = topicMap.CreateTopic().CreateName("Name").CreateVariant(value1, theme);
            UpdateIndex(index);

            Assert.Equal(1, index.GetVariants(value1).Count);
            Assert.True(index.GetVariants(value1).Contains(variant));
            Assert.Equal(1, index.GetVariants(value1, _xsdString).Count);
            Assert.True(index.GetVariants(value1, _xsdString).Contains(variant));

            variant.Value = value2;
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Empty(index.GetVariants(value1, _xsdString));
            Assert.Equal(1, index.GetVariants(value2).Count);
            Assert.True(index.GetVariants(value2).Contains(variant));
            Assert.Equal(1, index.GetVariants(value2, _xsdString).Count);
            Assert.True(index.GetVariants(value2, _xsdString).Contains(variant));

            variant.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Empty(index.GetVariants(value1, _xsdString));
            Assert.Empty(index.GetVariants(value2));
            Assert.Empty(index.GetVariants(value2, _xsdString));
        }

        [Fact]
        public void GetVariants_URI()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();
            var value1 = topicMap.CreateLocator("http://www.example.org/1");
            var value2 = topicMap.CreateLocator("http://www.example.org/2");

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Empty(index.GetVariants(value1.Reference, _xsdAnyURI));

            var theme = topicMap.CreateTopic();
            var variant = topicMap.CreateTopic().CreateName("Name").CreateVariant(value1, theme);
            UpdateIndex(index);

            Assert.Equal(1, index.GetVariants(value1).Count);
            Assert.True(index.GetVariants(value1).Contains(variant));
            Assert.Equal(1, index.GetVariants(value1.Reference, _xsdAnyURI).Count);
            Assert.True(index.GetVariants(value1.Reference, _xsdAnyURI).Contains(variant));

            variant.LocatorValue = value2;
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Empty(index.GetVariants(value1.Reference, _xsdAnyURI));
            Assert.Equal(1, index.GetVariants(value2).Count);
            Assert.True(index.GetVariants(value2).Contains(variant));
            Assert.Equal(1, index.GetVariants(value2.Reference, _xsdAnyURI).Count);
            Assert.True(index.GetVariants(value2.Reference, _xsdAnyURI).Contains(variant));

            variant.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Empty(index.GetVariants(value1.Reference, _xsdAnyURI));
            Assert.Empty(index.GetVariants(value2));
            Assert.Empty(index.GetVariants(value2.Reference, _xsdAnyURI));
        }

        [Fact]
        public void GetVariants_URIExplicitDatatype()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();
            var value1 = "http://www.example.org/1";
            var value2 = "http://www.example.org/2";
            var datatype = topicMap.CreateLocator("http://www.example.org/datatype");

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Empty(index.GetVariants(value1, datatype));

            var theme = topicMap.CreateTopic();
            var variant = topicMap.CreateTopic().CreateName("Name").CreateVariant(value1, datatype, theme);
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Equal(1, index.GetVariants(value1, datatype).Count);
            Assert.True(index.GetVariants(value1, datatype).Contains(variant));

            variant.Value = value2;
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value1));
            Assert.Empty(index.GetVariants(value1, datatype));
            Assert.Empty(index.GetVariants(value2));
            Assert.Equal(1, index.GetVariants(value2, datatype).Count);
            Assert.True(index.GetVariants(value2, datatype).Contains(variant));

            variant.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(value2));
            Assert.Empty(index.GetVariants(value2, datatype));
        }

        [Fact]
        public void GetVariants_UsingInvalidStringThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as string value is not allowed.", () => index.GetVariants((string)null));
        }

        [Fact]
        public void GetVariants_UsingInvalidURIThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as locator value is not allowed.", () => index.GetVariants((ILocator)null));
        }

        [Fact]
        public void GetVariants_UsingInvalidDatatypeThrowsException()
        {
            var topicMap = topicMapSystem.CreateTopicMap(TestTM1);
            var index = (ILiteralIndex)topicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as datatype is not allowed.", () => index.GetVariants("Value", null));
        }
        #endregion
    }
}