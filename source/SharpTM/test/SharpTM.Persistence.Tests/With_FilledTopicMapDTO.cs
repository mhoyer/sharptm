using System;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests
{
	public class With_FilledTopicMapDTO : With_TopicMapDTO
	{
		protected static TopicDTO knowsAbout;
		protected static TopicDTO person;
		protected static TopicDTO project;
		protected static TopicDTO isProjectLeaderOf;
		protected static TopicDTO image;
		protected static TopicDTO abstractTopic;
		
		protected static TypeDTO knowsAboutType;
		protected static TypeDTO isProjectLeaderOfType;
		protected static TypeDTO personType;
		protected static TypeDTO projectType;
		protected static TypeDTO imageType;
		protected static TypeDTO abstractType;

		protected static TopicDTO marcelHoyer;
		protected static TopicDTO lutzMaicher;
		protected static TopicDTO sharpTM;

		protected static AssociationDTO projectLeaderOfSharpTM;
		protected static AssociationDTO marcelKnowsAboutLutz;

		protected static ResourceDataDTO marcelHoyerAbstractResource;
		protected static OccurrenceDTO marcelHoyerAbstract;
		protected static OccurrenceDTO marcelHoyerImage;

		protected static LocatorDTO CreateLocator(string relativeUri)
		{
			return new LocatorDTO() { HRef = CreateLocatorString(relativeUri) };
		}

		protected static string CreateLocatorString(string relativeUri)
		{
			return String.Format("http://sharptm.de/{0}", relativeUri);
		}

		protected static TopicDTO CreateTopic(params string[] subjectIdentifiers)
		{
			var topic = new TopicDTO();
			topic.InstanceOf = new InstanceOfDTO();

			foreach (string identifier in subjectIdentifiers)
			{
				topic.SubjectIdentifiers.Add(new LocatorDTO() { HRef = identifier });
			}

			topicMapDTO.Topics.Add(topic);

			return topic;
		}

		protected static AssociationDTO CreateAssociation(TypeDTO associationType,
		                                                TypeDTO roleType1, TopicDTO role1,
		                                                TypeDTO roleType2, TopicDTO role2)
		{
			var association = new AssociationDTO();

			var associationLocator = new LocatorDTO();
			associationLocator.HRef = associationType.TopicReference.HRef + association.GetHashCode();

			association.ItemIdentities.Add(associationLocator);
			association.Type = associationType;
			association.Roles.Add(new RoleDTO() { Type = roleType1, TopicReference = role1.SubjectIdentifiers[0] });
			association.Roles.Add(new RoleDTO() { Type = roleType2, TopicReference = role2.SubjectIdentifiers[0] });

			return association;
		}

		static OccurrenceDTO CreateOccurrence(TypeDTO occurrenceType)
		{
			OccurrenceDTO occurrenceDTO = new OccurrenceDTO();
			occurrenceDTO.Type = occurrenceType;

			return occurrenceDTO;
		}

		protected static OccurrenceDTO CreateOccurrence(TypeDTO occurrenceType, LocatorDTO resourceReference)
		{
			OccurrenceDTO occurrenceDTO = CreateOccurrence(occurrenceType);
			occurrenceDTO.ResourceReference = resourceReference;
			
			return occurrenceDTO;
		}

		protected static OccurrenceDTO CreateOccurrence(TypeDTO occurrenceType, ResourceDataDTO resourceData)
		{
			OccurrenceDTO occurrenceDTO = CreateOccurrence(occurrenceType);
			occurrenceDTO.ResourceData = resourceData;
			
			return occurrenceDTO;
		}

		Given the_person_topic = () => person = CreateTopic(CreateLocatorString("person"));
		Given the_project_topic = () => project = CreateTopic(CreateLocatorString("project"));
		Given the_knows_about_topic = () => knowsAbout = CreateTopic(CreateLocatorString("knowsAbout"));
		Given the_project_leader_topic = () => isProjectLeaderOf = CreateTopic(CreateLocatorString("isProjectLeaderOf"));

		Given the_knows_about_type = () => knowsAboutType = new TypeDTO() { TopicReference = knowsAbout.SubjectIdentifiers[0] };
		Given the_project_leader_type = () => isProjectLeaderOfType = new TypeDTO() { TopicReference = isProjectLeaderOf.SubjectIdentifiers[0] };
		Given the_person_type = () => personType = new TypeDTO() { TopicReference = person.SubjectIdentifiers[0] };
		Given the_project_type = () => projectType = new TypeDTO() { TopicReference = project.SubjectIdentifiers[0] };

		Given the_image_topic = () => image = CreateTopic(CreateLocatorString("image"));
		Given the_image_type = () => imageType = new TypeDTO() { TopicReference = image.SubjectIdentifiers[0]};

		Given the_abstract_topic = () => abstractTopic = CreateTopic(CreateLocatorString("abstract"));
		Given the_abstract_type = () => abstractType = new TypeDTO() { TopicReference = abstractTopic.SubjectIdentifiers[0]};
        
		Given the_Marcel_Hoyer_topic =
			() =>
				{
					marcelHoyer = CreateTopic("http://www.topicmapslab.de/people/Marcel_Hoyer", "http://www.marcelhoyer.de");
					marcelHoyer.InstanceOf.TopicReferences.Add(personType.TopicReference);
				};

		Given the_Lutz_Maicher_topic =
			() =>
				{
					lutzMaicher = CreateTopic("http://www.topicmapslab.de/people/Lutz_Maicher");
					lutzMaicher.InstanceOf.TopicReferences.Add(personType.TopicReference);
				};

		Given the_SharpTM_topic =
			() =>
				{
					sharpTM = CreateTopic(CreateLocatorString("this"));
					sharpTM.InstanceOf.TopicReferences.Add(projectType.TopicReference);
				};
		Given the_SharpTM_project_leader_association =
			() =>
				{
					projectLeaderOfSharpTM = CreateAssociation(isProjectLeaderOfType, personType, marcelHoyer, projectType, sharpTM);
					topicMapDTO.Associations.Add(projectLeaderOfSharpTM);
				};

		Given the_Marcel_knows_about_Lutz_association =
			() =>
				{
					marcelKnowsAboutLutz = CreateAssociation(knowsAboutType, personType, marcelHoyer, personType, lutzMaicher);
					topicMapDTO.Associations.Add(marcelKnowsAboutLutz);
				};

		Given the_Marcel_Hoyer_image_reference_occurrence =
			() =>
				{
					marcelHoyerImage = CreateOccurrence(imageType, new LocatorDTO() { HRef = "http://pixelplastic.de/images/_ChannelImage.jpg" });
					marcelHoyer.Occurrences.Add(marcelHoyerImage);
				};

		Given the_Marcel_Hoyer_abstract_occurrence =
			() =>
				{
					marcelHoyerAbstractResource = new ResourceDataDTO();
					marcelHoyerAbstractResource.Text = "This is some information about Marcel Hoyer.";
					marcelHoyerAbstractResource.Datatype = ""; // == http://www.w3.org/2001/XMLSchema#string

					marcelHoyerAbstract = CreateOccurrence(abstractType, marcelHoyerAbstractResource);
					marcelHoyer.Occurrences.Add(marcelHoyerAbstract);
				};
	}
}