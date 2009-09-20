// <copyright file="XTMConnector.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;

namespace Pixelplastic.TopicMaps.SharpTM.DataInterchange.XTM2
{
	public class XTMConnector : FileConnector<TopicMapDTO>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="XTMConnector"/> class using default <see cref="XmlReader"/> for XTM access.
		/// </summary>
		public XTMConnector()
			: this ((xtmStream) => XmlReader.Create(xtmStream))
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="XTMConnector"/> class.
		/// </summary>
		/// <param name="xmlReaderCreationAction">The XML reader creation action.</param>
		public XTMConnector(Func<XmlReader, Stream> xmlReaderCreationAction)
		{
			SerializeAction =
				(Stream xtmStream) =>
					{
						XmlReader xr = xmlReaderCreationAction.Invoke(xtmStream);
						XmlSerializer xs = new XmlSerializer(typeof(TopicMapDTO));
						TopicMapDTO topicMapDTO = (TopicMapDTO)xs.Deserialize(xr);
						return topicMapDTO;
					};
		}
	}
}