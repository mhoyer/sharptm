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
		protected static TypeDTO knowsAboutType;
		protected static TypeDTO isProjectLeaderOfType;
		protected static TypeDTO personType;
		protected static TypeDTO projectType;
		protected static TopicDTO marcelHoyer;
		protected static TopicDTO lutzMaicher;
		protected static TopicDTO sharpTM;
		protected static AssociationDTO projectLeaderOfSharpTM;
		protected static AssociationDTO marcelKnowsAboutLutz;

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
			association.Type = associationType;
			association.Roles.Add(new RoleDTO() { Type = roleType1, TopicReference = role1.SubjectIdentifiers[0] });
			association.Roles.Add(new RoleDTO() { Type = roleType2, TopicReference = role2.SubjectIdentifiers[0] });

			return association;
		}

		Given the_person_topic = () => person = CreateTopic("http://sharptm.de/person");
		Given the_project_topic = () => project = CreateTopic("http://sharptm.de/project");
		Given the_knows_about_topic = () => knowsAbout = CreateTopic("http://sharptm.de/knowsAbout");
		Given the_project_leader_topic = () => isProjectLeaderOf = CreateTopic("http://sharptm.de/isProjectLeaderOf");

		Given the_knows_about_type = () => knowsAboutType = new TypeDTO() { TopicReference = knowsAbout.SubjectIdentifiers[0] };
		Given the_project_leader_type = () => isProjectLeaderOfType = new TypeDTO() { TopicReference = isProjectLeaderOf.SubjectIdentifiers[0] };
		Given the_person_type = () => personType = new TypeDTO() { TopicReference = person.SubjectIdentifiers[0] };
		Given the_project_type = () => projectType = new TypeDTO() { TopicReference = project.SubjectIdentifiers[0] };

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
					sharpTM = CreateTopic("http://sharptm.de");
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
	}
}