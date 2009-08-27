// <copyright file="IConnector.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts
{
	public interface IRepository<TTMAPIEntity> where TTMAPIEntity : IConstruct
	{
		TTMAPIEntity Load(object id);
		void Save(TTMAPIEntity tmapiEntity);
	}
}