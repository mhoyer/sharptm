// <copyright file="IMapperStep.cs" company="Pixelplastic">
// Copyright (C) Alexander Groﬂ 2009. All rights reserved.
// </copyright>
// <author>Alexander Groﬂ</author>
// <email>agross AT therightstuff DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts
{
	public interface IMapperStep<TSource, TResult>
	{
		void Map(TSource source, TResult result);
	}
}