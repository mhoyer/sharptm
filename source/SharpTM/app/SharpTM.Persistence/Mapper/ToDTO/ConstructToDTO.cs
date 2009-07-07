using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.ToDTO
{
	public class ConstructToDTO<TSourceType, TTargetType> : ClassMapper<TSourceType, TTargetType>
		where TSourceType : class, IConstruct
		where TTargetType : class, IConstructDTO, new()
	{
		public ConstructToDTO()
		{
			From(construct => construct.ItemIdentifiers)
				.To((dto, itemIdentifiers) =>
				    	{
				    		foreach (ILocator itemIdentifier in itemIdentifiers)
				    		{
				    			dto.ItemIdentities.Add(LocatorToDTO.Create(itemIdentifier));
				    		}
				    	}
				);
		}
	}
}