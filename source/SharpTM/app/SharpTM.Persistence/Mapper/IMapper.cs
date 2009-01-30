// <copyright file="IMapper.cs" company="Pixelplastic">
// Copyright (C) Alexander Gro� 2009. All rights reserved.
// </copyright>
// <author>Alexander Gro�</author>
// <email>agross AT therightstuff DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper
{
	public interface IMapper<TSource, TResult>
	{
		void Map(TSource from, TResult to);
		void Add(IMapperStep<TSource, TResult> mapperStep);
	}
}