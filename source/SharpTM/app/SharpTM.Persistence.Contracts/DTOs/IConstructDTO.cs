using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs
{
	public interface IConstructDTO
	{
		/// <summary>
		/// Gets or sets the list of <see cref="LocatorDTO"/> objects.
		/// </summary>
		/// <remarks>
		/// The <c>itemIdentity</c> element is used to assign an item identifier 
		/// to the topic map construct represented by its parent element.
		/// </remarks>
		/// <value>The list of <see cref="LocatorDTO"/> objects.</value>
		[XmlElement("itemIdentity")]
		List<LocatorDTO> ItemIdentities
		{
			get;
			set;
		}
	}
}