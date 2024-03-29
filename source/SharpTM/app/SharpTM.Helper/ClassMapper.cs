// <copyright file="ClassMapper.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;

namespace Pixelplastic.TopicMaps.SharpTM.Helper
{
	public class ClassMapper<TSourceType, TTargetType> : Mapper<TSourceType, TTargetType>
		where TSourceType : class
		where TTargetType : class
	{
		public override TTargetType Map(TSourceType from, TTargetType to)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}

			if (to == null)
			{
				throw new ArgumentNullException("to");
			}

			base.Map(from, to);

			return to;
		}

		protected From<TSourceResultType, TSourceType, TTargetType> From<TSourceResultType>(Func<TSourceResultType, TSourceType> fromFunc)
		{
			var step = new From<TSourceResultType, TSourceType, TTargetType>(fromFunc);
			Add(step);

			return step;
		}
	}
}