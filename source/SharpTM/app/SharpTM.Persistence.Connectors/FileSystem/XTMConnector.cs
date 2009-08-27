// <copyright file="XTMConnector.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Pixelplastic.TopicMaps.SharpTM.Persistence.DTOs;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Connectors
{
	public class XTMConnector : Connector<TopicMapDTO>
	{
		public XTMConnector()
		{
			SerializeAction =
				(Stream xtmStream) =>
					{
						XmlSerializer xs = new XmlSerializer(typeof(TopicMapDTO));
						TopicMapDTO topicMapDTO = (TopicMapDTO) xs.Deserialize(XmlReader.Create(xtmStream));

						return topicMapDTO;
					};
		}
	}
}