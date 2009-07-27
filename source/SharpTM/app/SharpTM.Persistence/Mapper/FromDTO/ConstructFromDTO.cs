using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class ConstructFromDTO<TSourceType, TTargetType> : ClassMapper<TSourceType, TTargetType>
		where TSourceType : class, IConstructDTO, new()
		where TTargetType : class, IConstruct
	{
		protected ConstructFromDTO()
		{
			From(dto => dto.ItemIdentities)
				.To((construct, itemIdentifiers) =>
				    	{
				    		foreach (LocatorDTO dto in itemIdentifiers)
				    		{
				    			construct.AddItemIdentifier(
				    				construct.TopicMap.CreateLocator(dto.HRef));
				    		}
				    	});
		}
	}
}