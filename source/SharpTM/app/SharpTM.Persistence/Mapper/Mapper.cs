// <copyright file="Mapper.cs" company="Pixelplastic">
// Copyright (C) Alexander Groﬂ 2009. All rights reserved.
// </copyright>
// <author>Alexander Groﬂ, Marcel Hoyer</author>
// <email>agross AT therightstuff DOT de</email>

using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper
{
	public abstract class Mapper<TSource, TResult> : IMapper<TSource, TResult>
	{
		private readonly IList<IMapperStep<TSource, TResult>> mappingSteps = new List<IMapperStep<TSource, TResult>>();

		#region IMapper<TSource,TResult> Members
		public virtual TResult Map(TSource from, TResult to)
		{
			foreach (var mappingStep in mappingSteps)
			{
				mappingStep.Map(from, to);
			}

			return to;
		}

		public void Add(IMapperStep<TSource, TResult> mapperStep)
		{
			mappingSteps.Add(mapperStep);
		}
		#endregion
	}
}