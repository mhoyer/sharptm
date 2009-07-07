using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	public interface IScopedDTO
	{
		/// <summary>
		/// Gets or sets the scope.
		/// </summary>
		/// <value>The scope.</value>
		[XmlElement("scope")]
		ScopeDTO Scope
		{
			get;
			set;
		}
	}
}