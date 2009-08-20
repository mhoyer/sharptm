using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper.FromDTO
{
	public class VariantFromDTO : ConstructFromDTO<VariantDTO, IVariant>
	{
		static VariantFromDTO mapper = new VariantFromDTO();

		private VariantFromDTO()
		{
			From(dto => dto.Reifier)
				.To((variant, reifierId)
					=>
				{
					if (reifierId != null)
					{
						variant.Reifier = TopicFromDTO.FindOrCreate(
							variant.TopicMap,
							reifierId);
					}
				});
		}

		public static IVariant Create(IName parent, VariantDTO source)
		{
			if(source.Scope == null || source.Scope.TopicReferences.Count == 0)
			{
				throw new MappingException(string.Format("Unable to map the variant of name '{0}'. Missing at least one scope.", parent));
			}

			IVariant target = parent.CreateVariant(
				source.ResourceData.Text,
				ScopeFromDTO.Create(parent.TopicMap, source.Scope));

			return mapper.Map(source, target);
		}
	}
}