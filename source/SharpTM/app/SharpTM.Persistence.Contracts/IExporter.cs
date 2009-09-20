// <copyright file="IExporter.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts
{
	public interface IExporter<TTMAPIEntity> where TTMAPIEntity : IConstruct
	{
		void Export(TTMAPIEntity tmapiEntity);
	}
}