// <copyright file="AssociationFromDTO.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2.Mapper.FromDTO
{
	/// <summary>
	/// Converts from <see cref="AssociationDTO"/> to <see cref="IAssociation"/>
	/// </summary>
	public class AssociationFromDTO : ConstructFromDTO<AssociationDTO, IAssociation>
	{
		public const string PSI_INSTANCE = "http://psi.topicmaps.org/iso13250/model/instance";
		public const string PSI_TYPE = "http://psi.topicmaps.org/iso13250/model/type";
		public const string PSI_TYPE_INSTANCE = "http://psi.topicmaps.org/iso13250/model/type-instance";

		static readonly AssociationFromDTO mapper = new AssociationFromDTO();

		AssociationFromDTO()
		{
			From(dto => dto.Roles)
				.To((association, roleDTOs)
				    =>
				    	{
				    		foreach (RoleDTO roleDTO in roleDTOs)
				    		{
				    			RoleFromDTO.Create(association, roleDTO);
				    		}
				    	});
		}

		public static IAssociation Create(ITopicMap parent, AssociationDTO source)
		{
			IAssociation target = parent.CreateAssociation(TypeFromDTO.FindOrCreate(parent, source.Type));
			mapper.Map(source, target);
			ReifiableFromDTO.Instance.Map(source, target);
			ScopeFromDTO.Instance.Map(source, target);

			return target;
		}

		public static IAssociation CreateTypeInstance(ITopic instance, LocatorDTO typeLocator)
		{
			ITopicMap topicMap = instance.TopicMap;

			IAssociation typeInstanceAssociation = topicMap.CreateAssociation(
				TopicFromDTO.FindOrCreateBySubjectIdentifier(topicMap, PSI_TYPE_INSTANCE));

			typeInstanceAssociation.CreateRole(
				TopicFromDTO.FindOrCreateBySubjectIdentifier(topicMap, PSI_INSTANCE),
				instance);

			typeInstanceAssociation.CreateRole(
				TopicFromDTO.FindOrCreateBySubjectIdentifier(topicMap, PSI_TYPE),
				TopicFromDTO.FindOrCreate(topicMap, typeLocator));

			return typeInstanceAssociation;
		}
	}
}