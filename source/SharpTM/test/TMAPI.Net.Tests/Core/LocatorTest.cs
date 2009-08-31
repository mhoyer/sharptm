using TMAPI.Net.Core;
using Xunit;

namespace TMAPI.Net.Tests.Core
{
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
			Assert.Equal(CORRECT_RESOLVED_URI, topicMapSystem.CreateLocator(CORRECT_URI).Resolve(RELATIVE_URI).ExternalForm);
		}

		[Fact]
		public void ShouldResolveRelativIri()
		{
			Assert.Equal(CORRECT_RESOLVED_IRI, topicMapSystem.CreateLocator(CORRECT_URI).Resolve(RELATIVE_URI).Reference);
		}

		[Fact]
		public void ShouldReturnReference()
		{
			Assert.NotNull(topicMapSystem.CreateLocator(CORRECT_URI).Reference);
		}

		[Fact]
		public void ShouldInitializeReferenceProperty()
		{
			Assert.Equal(CORRECT_URI, topicMapSystem.CreateLocator(CORRECT_URI).Reference);
		}

		[Fact]
		public void ShouldReturnIRIReference()
		{
			Assert.NotNull(topicMapSystem.CreateLocator(CORRECT_IRI).Reference);
		}

		[Fact]
		public void ShouldInitializeIRIReferenceProperty()
		{
			Assert.Equal(CORRECT_IRI, topicMapSystem.CreateLocator(CORRECT_IRI).Reference);
		}

		[Fact]
		public void ShouldReturnExternalForm()
		{
			Assert.Equal(CORRECT_URI_EXTERNAL_FORM, topicMapSystem.CreateLocator(CORRECT_URI).ExternalForm);
		}

		[Fact]
		public void ShouldReturnExternalIriForm()
		{
			Assert.Equal(CORRECT_IRI_EXTERNAL_FORM, topicMapSystem.CreateLocator(CORRECT_IRI).ExternalForm);
		}

		[Fact]
		public void ShouldThrowTMAPIExceptionOnRelativeURI()
		{
			Assert.Throws(typeof(TMAPIException), delegate { topicMapSystem.CreateLocator(RELATIVE_URI); });
		}

	}
}