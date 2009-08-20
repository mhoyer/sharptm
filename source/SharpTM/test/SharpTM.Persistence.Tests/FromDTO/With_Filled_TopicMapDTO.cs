// <copyright file="With_Filled_TopicMapDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using Xunit.BDDExtension;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.FromDTO
{
	public abstract class With_Filled_TopicMapDTO : With_TopicMapDTO
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

		Given the_person_topic = () => person = topicMapDTO.CreateTopic(TestHelper.CreateLocatorString("person"));
		Given the_project_topic = () => project = topicMapDTO.CreateTopic(TestHelper.CreateLocatorString("project"));
		Given the_knows_about_topic = () => knowsAbout = topicMapDTO.CreateTopic(TestHelper.CreateLocatorString("knowsAbout"));
		Given the_project_leader_topic = () => isProjectLeaderOf = topicMapDTO.CreateTopic(TestHelper.CreateLocatorString("isProjectLeaderOf"));

		Given the_knows_about_type = () => knowsAboutType = new TypeDTO() { TopicReference = knowsAbout.SubjectIdentifiers[0] };
		Given the_project_leader_type = () => isProjectLeaderOfType = new TypeDTO() { TopicReference = isProjectLeaderOf.SubjectIdentifiers[0] };
		Given the_person_type = () => personType = new TypeDTO() { TopicReference = person.SubjectIdentifiers[0] };
		Given the_project_type = () => projectType = new TypeDTO() { TopicReference = project.SubjectIdentifiers[0] };

		Given the_image_topic = () => image = topicMapDTO.CreateTopic(TestHelper.CreateLocatorString("image"));
		Given the_image_type = () => imageType = new TypeDTO() { TopicReference = image.SubjectIdentifiers[0]};

		Given the_abstract_topic = () => abstractTopic = topicMapDTO.CreateTopic(TestHelper.CreateLocatorString("abstract"));
		Given the_abstract_type = () => abstractType = new TypeDTO() { TopicReference = abstractTopic.SubjectIdentifiers[0]};
        
		Given the_Marcel_Hoyer_topic =
			() =>
				{
					marcelHoyer = topicMapDTO.CreateTopic("http://www.topicmapslab.de/people/Marcel_Hoyer", "http://www.marcelhoyer.de");
					marcelHoyer.InstanceOf.TopicReferences.Add(personType.TopicReference);
				};

		Given the_Lutz_Maicher_topic =
			() =>
				{
					lutzMaicher = topicMapDTO.CreateTopic("http://www.topicmapslab.de/people/Lutz_Maicher");
					lutzMaicher.InstanceOf.TopicReferences.Add(personType.TopicReference);
				};

		Given the_SharpTM_topic =
			() =>
				{
					sharpTM = topicMapDTO.CreateTopic(TestHelper.CreateLocatorString("this"));
					sharpTM.InstanceOf.TopicReferences.Add(projectType.TopicReference);
				};
		Given the_SharpTM_project_leader_association =
			() =>
				{
					projectLeaderOfSharpTM = TestHelper.CreateAssociation(isProjectLeaderOfType, personType, marcelHoyer, projectType, sharpTM);
					topicMapDTO.Associations.Add(projectLeaderOfSharpTM);
				};

		Given the_Marcel_knows_about_Lutz_association =
			() =>
				{
					marcelKnowsAboutLutz = TestHelper.CreateAssociation(knowsAboutType, personType, marcelHoyer, personType, lutzMaicher);
					topicMapDTO.Associations.Add(marcelKnowsAboutLutz);
				};
	}
}