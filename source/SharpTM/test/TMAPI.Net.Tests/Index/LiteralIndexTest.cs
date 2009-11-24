// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LiteralIndexTest.cs">
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
//   Defines the LiteralIndexTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Index
{
    using System;
    using Net.Core;
    using Net.Index;
    using Xunit;

    public class LiteralIndexTest : TMAPITestCase
    {
        #region Constants
        private const string Value1 = "http://www.example.org/1";
        private const string Value2 = "http://www.example.org/2";
        #endregion
        
        #region Fields
        private static ILocator _xsdString;
        private static ILocator _xsdAnyURI;
        private static ILocator _datatype;
        #endregion

        #region Constructor
        public LiteralIndexTest()
        {
            _xsdString = TopicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#string");
            _xsdAnyURI = TopicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#anyURI");
            _datatype = TopicMap.CreateLocator("http://www.example.org/datatype");
        }
        #endregion

        #region Tests
        [Fact]
        public void GetNames()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(Value1));

            var name = CreateTopic().CreateName(Value1);
            UpdateIndex(index);

            Assert.Equal(1, index.GetNames(Value1).Count);
            Assert.True(index.GetNames(Value1).Contains(name));

            name.Value = Value2;
            UpdateIndex(index);

            Assert.Empty(index.GetNames(Value1));
            Assert.Equal(1, index.GetNames(Value2).Count);
            Assert.True(index.GetNames(Value2).Contains(name));

            name.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetNames(Value1));
            Assert.Empty(index.GetNames(Value2));
        }

        [Fact]
        public void GetNames_UsingInvalidStringThrowsException()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as string value is not allowed.", () => index.GetNames(null));
        }

        [Fact]
        public void GetOccurrences_String()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(Value1));
            Assert.Empty(index.GetOccurrences(Value1, _xsdString));

            var type = CreateTopic();
            var occurrence = CreateTopic().CreateOccurrence(type, Value1);
            UpdateIndex(index);

            Assert.Equal(1, index.GetOccurrences(Value1).Count);
            Assert.True(index.GetOccurrences(Value1).Contains(occurrence));
            Assert.Equal(1, index.GetOccurrences(Value1, _xsdString).Count);
            Assert.True(index.GetOccurrences(Value1, _xsdString).Contains(occurrence));

            occurrence.Value = Value2;
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(Value1));
            Assert.Empty(index.GetOccurrences(Value1, _xsdString));
            Assert.Equal(1, index.GetOccurrences(Value2).Count);
            Assert.True(index.GetOccurrences(Value2).Contains(occurrence));
            Assert.Equal(1, index.GetOccurrences(Value2, _xsdString).Count);
            Assert.True(index.GetOccurrences(Value2, _xsdString).Contains(occurrence));

            occurrence.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(Value1));
            Assert.Empty(index.GetOccurrences(Value1, _xsdString));
            Assert.Empty(index.GetOccurrences(Value2));
            Assert.Empty(index.GetOccurrences(Value2, _xsdString));
        }

        [Fact]
        public void GetOccurrences_URI()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();
            var locatorValue1 = TopicMap.CreateLocator("http://www.example.org/1");
            var locatorValue2 = TopicMap.CreateLocator("http://www.example.org/2");

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(locatorValue1));
            Assert.Empty(index.GetOccurrences(locatorValue1.Reference, _xsdAnyURI));

            var type = CreateTopic();
            var occurrence = CreateTopic().CreateOccurrence(type, locatorValue1);
            UpdateIndex(index);

            Assert.Equal(1, index.GetOccurrences(locatorValue1).Count);
            Assert.True(index.GetOccurrences(locatorValue1).Contains(occurrence));
            Assert.Equal(1, index.GetOccurrences(locatorValue1.Reference, _xsdAnyURI).Count);
            Assert.True(index.GetOccurrences(locatorValue1.Reference, _xsdAnyURI).Contains(occurrence));

            occurrence.LocatorValue = locatorValue2;
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(locatorValue1));
            Assert.Empty(index.GetOccurrences(locatorValue1.Reference, _xsdAnyURI));
            Assert.Equal(1, index.GetOccurrences(locatorValue2).Count);
            Assert.True(index.GetOccurrences(locatorValue2).Contains(occurrence));
            Assert.Equal(1, index.GetOccurrences(locatorValue2.Reference, _xsdAnyURI).Count);
            Assert.True(index.GetOccurrences(locatorValue2.Reference, _xsdAnyURI).Contains(occurrence));

            occurrence.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(locatorValue1));
            Assert.Empty(index.GetOccurrences(locatorValue1.Reference, _xsdAnyURI));
            Assert.Empty(index.GetOccurrences(locatorValue2));
            Assert.Empty(index.GetOccurrences(locatorValue2.Reference, _xsdAnyURI));
        }

        [Fact]
        public void GetOccurrences_URIExplicitDatatype()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(Value1));
            Assert.Empty(index.GetOccurrences(Value1, _datatype));

            var type = CreateTopic();
            var occurrence = CreateTopic().CreateOccurrence(type, Value1, _datatype);
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(Value1));
            Assert.Equal(1, index.GetOccurrences(Value1, _datatype).Count);
            Assert.True(index.GetOccurrences(Value1, _datatype).Contains(occurrence));

            occurrence.SetValue(Value2, _datatype);
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(Value1));
            Assert.Empty(index.GetOccurrences(Value1, _datatype));
            Assert.Empty(index.GetOccurrences(Value2));
            Assert.Equal(1, index.GetOccurrences(Value2, _datatype).Count);
            Assert.True(index.GetOccurrences(Value2, _datatype).Contains(occurrence));

            occurrence.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetOccurrences(Value2));
            Assert.Empty(index.GetOccurrences(Value2, _datatype));
        }

        [Fact]
        public void GetOccurrences_UsingInvalidStringThrowsException()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as string value is not allowed.", () => index.GetOccurrences((string)null));
        }

        [Fact]
        public void GetOccurrences_UsingInvalidURIThrowsException()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as locator is not allowed.", () => index.GetOccurrences((ILocator)null));
        }

        [Fact]
        public void GetOccurrences_UsingInvalidDatatypeThrowsException()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as datatype is not allowed.", () => index.GetOccurrences("Value", null));
        }

        [Fact]
        public void GetVariants_String()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(Value1));
            Assert.Empty(index.GetVariants(Value1, _xsdString));

            var theme = CreateTopic();
            var variant = CreateTopic().CreateName("Name").CreateVariant(Value1, theme);
            UpdateIndex(index);

            Assert.Equal(1, index.GetVariants(Value1).Count);
            Assert.True(index.GetVariants(Value1).Contains(variant));
            Assert.Equal(1, index.GetVariants(Value1, _xsdString).Count);
            Assert.True(index.GetVariants(Value1, _xsdString).Contains(variant));

            variant.Value = Value2;
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(Value1));
            Assert.Empty(index.GetVariants(Value1, _xsdString));
            Assert.Equal(1, index.GetVariants(Value2).Count);
            Assert.True(index.GetVariants(Value2).Contains(variant));
            Assert.Equal(1, index.GetVariants(Value2, _xsdString).Count);
            Assert.True(index.GetVariants(Value2, _xsdString).Contains(variant));

            variant.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(Value1));
            Assert.Empty(index.GetVariants(Value1, _xsdString));
            Assert.Empty(index.GetVariants(Value2));
            Assert.Empty(index.GetVariants(Value2, _xsdString));
        }

        [Fact]
        public void GetVariants_URI()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();
            var locatorValue1 = TopicMap.CreateLocator("http://www.example.org/1");
            var locatorValue2 = TopicMap.CreateLocator("http://www.example.org/2");

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(locatorValue1));
            Assert.Empty(index.GetVariants(locatorValue1.Reference, _xsdAnyURI));

            var theme = CreateTopic();
            var variant = CreateTopic().CreateName("Name").CreateVariant(locatorValue1, theme);
            UpdateIndex(index);

            Assert.Equal(1, index.GetVariants(locatorValue1).Count);
            Assert.True(index.GetVariants(locatorValue1).Contains(variant));
            Assert.Equal(1, index.GetVariants(locatorValue1.Reference, _xsdAnyURI).Count);
            Assert.True(index.GetVariants(locatorValue1.Reference, _xsdAnyURI).Contains(variant));

            variant.LocatorValue = locatorValue2;
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(locatorValue1));
            Assert.Empty(index.GetVariants(locatorValue1.Reference, _xsdAnyURI));
            Assert.Equal(1, index.GetVariants(locatorValue2).Count);
            Assert.True(index.GetVariants(locatorValue2).Contains(variant));
            Assert.Equal(1, index.GetVariants(locatorValue2.Reference, _xsdAnyURI).Count);
            Assert.True(index.GetVariants(locatorValue2.Reference, _xsdAnyURI).Contains(variant));

            variant.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(locatorValue1));
            Assert.Empty(index.GetVariants(locatorValue1.Reference, _xsdAnyURI));
            Assert.Empty(index.GetVariants(locatorValue2));
            Assert.Empty(index.GetVariants(locatorValue2.Reference, _xsdAnyURI));
        }

        [Fact]
        public void GetVariants_URIExplicitDatatype()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            index.Open();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(Value1));
            Assert.Empty(index.GetVariants(Value1, _datatype));

            var theme = CreateTopic();
            var variant = CreateTopic().CreateName("Name").CreateVariant(Value1, _datatype, theme);
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(Value1));
            Assert.Equal(1, index.GetVariants(Value1, _datatype).Count);
            Assert.True(index.GetVariants(Value1, _datatype).Contains(variant));

            variant.SetValue(Value2, _datatype);
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(Value1));
            Assert.Empty(index.GetVariants(Value1, _datatype));
            Assert.Empty(index.GetVariants(Value2));
            Assert.Equal(1, index.GetVariants(Value2, _datatype).Count);
            Assert.True(index.GetVariants(Value2, _datatype).Contains(variant));

            variant.Remove();
            UpdateIndex(index);

            Assert.Empty(index.GetVariants(Value2));
            Assert.Empty(index.GetVariants(Value2, _datatype));
        }

        [Fact]
        public void GetVariants_UsingInvalidStringThrowsException()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as string value is not allowed.", () => index.GetVariants((string)null));
        }

        [Fact]
        public void GetVariants_UsingInvalidURIThrowsException()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as locator value is not allowed.", () => index.GetVariants((ILocator)null));
        }

        [Fact]
        public void GetVariants_UsingInvalidDatatypeThrowsException()
        {
            var index = TopicMap.GetIndex<ILiteralIndex>();

            Assert.Throws<ArgumentNullException>("Using null as datatype is not allowed.", () => index.GetVariants("Value", null));
        }
        #endregion

        #region Methods
        private static void UpdateIndex(IIndex index)
        {
            if (!index.AutoUpdated)
            {
                index.Reindex();
            }
        }
        #endregion
    }
}