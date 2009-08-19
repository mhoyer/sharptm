using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public abstract class With_FilledTopicMapDTOandOccurrences : With_FilledTopicMapDTO
	{
		protected static ResourceDataDTO marcelHoyerAbstractResource;
		protected static OccurrenceDTO marcelHoyerAbstract;
		protected static OccurrenceDTO marcelHoyerImage;

		Given the_Marcel_Hoyer_image_reference_occurrence =
			() =>
				{
					marcelHoyerImage = TestHelper.CreateOccurrence(imageType, new LocatorDTO() { HRef = "http://pixelplastic.de/images/_ChannelImage.jpg" });
					marcelHoyer.Occurrences.Add(marcelHoyerImage);
				};

		Given the_Marcel_Hoyer_abstract_occurrence =
			() =>
				{
					marcelHoyerAbstractResource = new ResourceDataDTO();
					marcelHoyerAbstractResource.Text = "This is some information about Marcel Hoyer.";
					marcelHoyerAbstractResource.Datatype = ""; // == http://www.w3.org/2001/XMLSchema#string

					marcelHoyerAbstract = TestHelper.CreateOccurrence(abstractType, marcelHoyerAbstractResource);
					marcelHoyer.Occurrences.Add(marcelHoyerAbstract);
				};
	}
}