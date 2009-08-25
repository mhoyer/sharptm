// <copyright file="IMapper.cs" company="Pixelplastic">
// Copyright (C) Alexander Groﬂ 2009. All rights reserved.
// </copyright>
// <author>Alexander Groﬂ</author>
// <email>agross AT therightstuff DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts
{
	public interface IMapper<TSource, TResult>
	{
		TResult Map(TSource from, TResult to);
		void Add(IMapperStep<TSource, TResult> mapperStep);
	}
}