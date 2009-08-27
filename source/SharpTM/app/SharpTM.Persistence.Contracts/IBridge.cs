// <copyright file="IBridge.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts
{
	public interface IBridge<T1, T2>
	{
		T1 Map(T2 source);
		T2 Map(T1 source);
	}
}