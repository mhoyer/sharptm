// <copyright file="From.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Mapper
{
	public delegate TResult Func<TResult, TParameter>(TParameter parameter);
	public delegate void Action<T1, T2>(T1 param1, T2 param2);

	public class From<TSourceResultType, TSourceType, TTargetType> :
		IMapperStep<TSourceType, TTargetType>
	{
		private Func<TSourceResultType, TSourceType> from;
		private Action<TTargetType, TSourceResultType> to;

		public From(Func<TSourceResultType, TSourceType> fromFunc)
		{
			from = fromFunc;
		}

		public void To(Action<TTargetType, TSourceResultType> toFunc)
		{
			to = toFunc;
		}

		public void Map(TSourceType source, TTargetType target)
		{
			to.Invoke(target, from.Invoke(source));
		}
	}
}