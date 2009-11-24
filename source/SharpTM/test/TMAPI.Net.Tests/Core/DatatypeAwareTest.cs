// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatatypeAwareTest.cs">
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
//   Defines the DatatypeAwareTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using System;
    using Net.Core;
    using Xunit;

    public class DatatypeAwareTest : TMAPITestCase
    {
        #region Static Fields
        public static readonly string TestTM1 = "mem://localhost/testm1";
        public static ILocator _xsdString;
        public static ILocator _xsdAnyURI;
        public static ILocator _xsdInt;
        public static ILocator _xsdLong;
        public static ILocator _xsdFloat;
        public static ILocator _xsdDecimal;
        #endregion

        #region Constructors
        public DatatypeAwareTest()
        {
            _xsdString = TopicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#string");
            _xsdAnyURI = TopicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#anyURI");
            _xsdInt = TopicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#int");
            _xsdLong = TopicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#long");
            _xsdFloat = TopicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#float");
            _xsdDecimal = TopicMapSystem.CreateLocator("http://www.w3.org/2001/XMLSchema#decimal");
        }
        #endregion

        #region Tests
        [Fact]
        public void TestOccurrence()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var occurrence = topic.CreateOccurrence(topicMap.CreateTopic(), "Occurrence");

            TestStringValue(occurrence);
            TestStringValueExplicit(occurrence);
            TestURI(occurrence);
            TestURIExplicit(occurrence);
            TestDecimal(occurrence);
            TestDecimalExplicit(occurrence);
            TestFloat(occurrence);
            TestInt(occurrence);
            TestLong(occurrence);
            TestUserDefinedDatatype(occurrence);

            SetValue_UsingInvalidDatatypeThrowsException(occurrence);
            Value_UsingInvalidStringThrowsException(occurrence);
            Value_UsingInvalidStringWithExplicitDatatypeThrowsException(occurrence);
            LocatorValue_UsingInvalidLocatorThrowsException(occurrence);
        }

        [Fact]
        public void TestVariant()
        {
            var topicMap = TopicMapSystem.CreateTopicMap(TestTM1);
            var topic = topicMap.CreateTopic();
            var name = topic.CreateName("Name");
            var variant = name.CreateVariant("Variant", topicMap.CreateTopic());

            TestStringValue(variant);
            TestStringValueExplicit(variant);
            TestURI(variant);
            TestURIExplicit(variant);
            TestDecimal(variant);
            TestDecimalExplicit(variant);
            TestFloat(variant);
            TestInt(variant);
            TestLong(variant);
            TestUserDefinedDatatype(variant);

            SetValue_UsingInvalidDatatypeThrowsException(variant);
            Value_UsingInvalidStringThrowsException(variant);
            Value_UsingInvalidStringWithExplicitDatatypeThrowsException(variant);
            LocatorValue_UsingInvalidLocatorThrowsException(variant);
        }

        private static void TestStringValue(IDatatypeAware datatypeAware)
        {
            var value = "A string.";
            datatypeAware.Value = value;

            Assert.Equal(value, datatypeAware.Value);
            Assert.Equal(_xsdString, datatypeAware.Datatype);

            AssertFailInt(datatypeAware);
            AssertFailLong(datatypeAware);
            AssertFailFloat(datatypeAware);
            AssertFailDecimal(datatypeAware);
            AssertFailLocator(datatypeAware);
        }

        private static void TestStringValueExplicit(IDatatypeAware datatypeAware)
        {
            var value = "A string.";
            datatypeAware.SetValue(value, _xsdString);

            Assert.Equal(value, datatypeAware.Value);
            Assert.Equal(_xsdString, datatypeAware.Datatype);

            AssertFailInt(datatypeAware);
            AssertFailLong(datatypeAware);
            AssertFailFloat(datatypeAware);
            AssertFailDecimal(datatypeAware);
            AssertFailLocator(datatypeAware);
        }

        private void TestURI(IDatatypeAware datatypeAware)
        {
            var iri = "http://www.example.org/";
            var value = TopicMapSystem.CreateLocator(iri);
            datatypeAware.LocatorValue = value;

            Assert.Equal(iri, datatypeAware.Value);
            Assert.Equal(value, datatypeAware.LocatorValue);
            Assert.Equal(_xsdAnyURI, datatypeAware.Datatype);

            AssertFailInt(datatypeAware);
            AssertFailLong(datatypeAware);
            AssertFailFloat(datatypeAware);
            AssertFailDecimal(datatypeAware);
        }

        private void TestURIExplicit(IDatatypeAware datatypeAware)
        {
            var iri = "http://www.example.org/";
            var value = TopicMapSystem.CreateLocator(iri);
            datatypeAware.SetValue(iri, _xsdAnyURI);

            Assert.Equal(iri, datatypeAware.Value);
            Assert.Equal(value, datatypeAware.LocatorValue);
            Assert.Equal(_xsdAnyURI, datatypeAware.Datatype);

            AssertFailInt(datatypeAware);
            AssertFailLong(datatypeAware);
            AssertFailFloat(datatypeAware);
            AssertFailDecimal(datatypeAware);
        }

        private static void TestInt(IDatatypeAware datatypeAware)
        {
            var value = 2008;
            var stringValue = "2008";
            datatypeAware.IntValue = value;

            Assert.Equal(stringValue, datatypeAware.Value);
            Assert.Equal(_xsdInt, datatypeAware.Datatype);
            Assert.Equal(2008M, datatypeAware.DecimalValue);
            Assert.Equal(2008L, datatypeAware.LongValue);
            Assert.Equal(value, datatypeAware.IntValue);
            Assert.Equal(2008.0F, datatypeAware.FloatValue);
        }

        private static void TestLong(IDatatypeAware datatypeAware)
        {
            var value = 2008L;
            var stringValue = "2008";
            datatypeAware.LongValue = value;

            Assert.Equal(stringValue, datatypeAware.Value);
            Assert.Equal(_xsdLong, datatypeAware.Datatype);
            Assert.Equal(2008M, datatypeAware.DecimalValue);
            Assert.Equal(value, datatypeAware.LongValue);
            Assert.Equal(2008, datatypeAware.IntValue);
            Assert.Equal(2008.0F, datatypeAware.FloatValue);
        }

        private static void TestFloat(IDatatypeAware datatypeAware)
        {
            var value = 2008.0F;
            var stringValue = "2008.00";
            datatypeAware.FloatValue = value;

            Assert.Equal(stringValue, datatypeAware.Value);
            Assert.Equal(_xsdFloat, datatypeAware.Datatype);
            Assert.Equal(2008M, datatypeAware.DecimalValue);
            Assert.Equal(value, datatypeAware.FloatValue);

            AssertFailLong(datatypeAware);
            AssertFailInt(datatypeAware);
        }

        private static void TestDecimal(IDatatypeAware datatypeAware)
        {
            var value = 2008M;
            datatypeAware.DecimalValue = value;

            Assert.Equal(value.ToString(), datatypeAware.Value);
            Assert.Equal(_xsdDecimal, datatypeAware.Datatype);
            Assert.Equal(value, datatypeAware.DecimalValue);
            Assert.Equal(2008L, datatypeAware.LongValue);
            Assert.Equal(2008, datatypeAware.IntValue);
            Assert.Equal(2008.0F, datatypeAware.FloatValue);
        }

        private static void TestDecimalExplicit(IDatatypeAware datatypeAware)
        {
            var value = 2008M;
            datatypeAware.SetValue(value.ToString(), _xsdDecimal);

            Assert.Equal(value.ToString(), datatypeAware.Value);
            Assert.Equal(_xsdDecimal, datatypeAware.Datatype);
            Assert.Equal(value, datatypeAware.DecimalValue);
            Assert.Equal(2008L, datatypeAware.LongValue);
            Assert.Equal(2008, datatypeAware.IntValue);
            Assert.Equal(2008.0F, datatypeAware.FloatValue);
        }

        private void TestUserDefinedDatatype(IDatatypeAware datatypeAware)
        {
            var datatype = TopicMapSystem.CreateLocator("http://www.example.org/datatype");
            var value = "Value";
            datatypeAware.SetValue(value, datatype);

            Assert.Equal(value, datatypeAware.Value);
            Assert.Equal(datatype, datatypeAware.Datatype);

            AssertFailDecimal(datatypeAware);
            AssertFailFloat(datatypeAware);
            AssertFailInt(datatypeAware);
            AssertFailLong(datatypeAware);
        }

        private static void SetValue_UsingInvalidDatatypeThrowsException(IDatatypeAware datatypeAware)
        {
            var e = Assert.Throws<ModelConstraintException>("Using null as value datatytpe is not allowed.", () => datatypeAware.SetValue("Value", null));
            Assert.Equal(datatypeAware, e.Reporter);
        }

        private static void Value_UsingInvalidStringThrowsException(IDatatypeAware datatypeAware)
        {
            var e = Assert.Throws<ModelConstraintException>("Using null as string value is not allowed.", () => datatypeAware.Value = null);
            Assert.Equal(datatypeAware, e.Reporter);
        }

        private static void Value_UsingInvalidStringWithExplicitDatatypeThrowsException(IDatatypeAware datatypeAware)
        {
            var e = Assert.Throws<ModelConstraintException>("Using null as string value is not allowed.", () => datatypeAware.SetValue(null, _xsdString));
            Assert.Equal(datatypeAware, e.Reporter);
        }

        private static void LocatorValue_UsingInvalidLocatorThrowsException(IDatatypeAware datatypeAware)
        {
            var e = Assert.Throws<ModelConstraintException>("Using null as locator value is not allowed.", () => datatypeAware.LocatorValue = null);
            Assert.Equal(datatypeAware, e.Reporter);
        }

        private static void AssertFailLocator(IDatatypeAware datatypeAware)
        {
            Assert.Throws<InvalidOperationException>("Expected a failure for converting the value to Locator.", () => GetLocatorValue(datatypeAware));
        }

        private static ILocator GetLocatorValue(IDatatypeAware datatypeAware)
        {
            return datatypeAware.LocatorValue;
        }

        private static void AssertFailDecimal(IDatatypeAware datatypeAware)
        {
            Assert.Throws<FormatException>("Expected a failure for converting the value to 'decimal'.", () => GetDecimalValue(datatypeAware));
        }

        private static decimal GetDecimalValue(IDatatypeAware datatypeAware)
        {
            return datatypeAware.DecimalValue;
        }

        private static void AssertFailFloat(IDatatypeAware datatypeAware)
        {
            Assert.Throws<FormatException>("Expected a failure for converting the value to 'float'.", () => GetFloatValue(datatypeAware));
        }

        private static float GetFloatValue(IDatatypeAware datatypeAware)
        {
            return datatypeAware.FloatValue;
        }

        private static void AssertFailLong(IDatatypeAware datatypeAware)
        {
            Assert.Throws<FormatException>("Expected a failure for converting the value to 'long'.", () => GetLongValue(datatypeAware));
        }

        private static long GetLongValue(IDatatypeAware datatypeAware)
        {
            return datatypeAware.LongValue;
        }

        private static void AssertFailInt(IDatatypeAware datatypeAware)
        {
            Assert.Throws<FormatException>("Expected a failure for converting the value to 'int'.", () => GetIntValue(datatypeAware));
        }

        private static int GetIntValue(IDatatypeAware datatypeAware)
        {
            return datatypeAware.IntValue;
        }
        #endregion
    }
}