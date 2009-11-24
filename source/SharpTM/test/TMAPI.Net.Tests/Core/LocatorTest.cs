// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocatorTest.cs">
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
//   Defines the LocatorTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xunit.Extensions;

namespace TMAPI.Net.UnitTests.Core
{
    using Net.Core;
    using Xunit;

    public class LocatorTest : TMAPITestCase
    {
        private const string CORRECT_URI = "http://www.example.org/test +me/";
        private const string CORRECT_URI_EXTERNAL_FORM = "http://www.example.org/test%20+me/";

        private const string CORRECT_IRI = "http://הצü.example.org/test+%20הצüß";
        private const string CORRECT_IRI_EXTERNAL_FORM = "http://הצü.example.org/test+%20%C3%A4%C3%B6%C3%BC%C3%9F";

        private const string RELATIVE_URI = "../test + you";
        private const string CORRECT_RESOLVED_URI = "http://www.example.org/test%20+%20you";
        private const string CORRECT_RESOLVED_IRI = "http://www.example.org/test + you";

        [Fact] 
        public void ShouldResolveRelativUri()
        {
            Assert.Equal(CORRECT_RESOLVED_URI, TopicMapSystem.CreateLocator(CORRECT_URI).Resolve(RELATIVE_URI).ExternalForm);
        }

        [Fact]
        public void ShouldResolveRelativIri()
        {
            Assert.Equal(CORRECT_RESOLVED_IRI, TopicMapSystem.CreateLocator(CORRECT_URI).Resolve(RELATIVE_URI).Reference);
        }

        [Fact]
        public void ShouldReturnReference()
        {
            Assert.NotNull(TopicMapSystem.CreateLocator(CORRECT_URI).Reference);
        }

        [Fact]
        public void ShouldInitializeReferenceProperty()
        {
            Assert.Equal(CORRECT_URI, TopicMapSystem.CreateLocator(CORRECT_URI).Reference);
        }

        [Fact]
        public void ShouldReturnIRIReference()
        {
            Assert.NotNull(TopicMapSystem.CreateLocator(CORRECT_IRI).Reference);
        }

        [Fact]
        public void ShouldInitializeIRIReferenceProperty()
        {
            Assert.Equal(CORRECT_IRI, TopicMapSystem.CreateLocator(CORRECT_IRI).Reference);
        }

        [Fact]
        public void ShouldReturnExternalForm()
        {
            Assert.Equal(CORRECT_URI_EXTERNAL_FORM, TopicMapSystem.CreateLocator(CORRECT_URI).ExternalForm);
        }

        [Fact]
        public void ShouldReturnExternalIriForm()
        {
            Assert.Equal(CORRECT_IRI_EXTERNAL_FORM, TopicMapSystem.CreateLocator(CORRECT_IRI).ExternalForm);
        }

        [Theory]
        [InlineData("")]
        [InlineData("#fragment")]
        public void IllegalLocatorAddressesShouldThrowMalformedIRIException(string reference)
        {
            // act
            Assert.ThrowsDelegate topicMap = () => TopicMap.CreateLocator(reference);
            Assert.ThrowsDelegate topicMapSystem = () => TopicMapSystem.CreateLocator(reference);

            // assert
            Assert.Throws<MalformedIRIException>(topicMap);
            Assert.Throws<MalformedIRIException>(topicMapSystem);
        }
    }
}