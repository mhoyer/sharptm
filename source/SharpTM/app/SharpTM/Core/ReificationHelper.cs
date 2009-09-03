// <copyright file="ReificationHelper.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	public class ReificationHelper
	{
		private static void Do(IReifiable reifiable, Topic newReifier)
		{
			if (reifiable == null) throw new ArgumentNullException("reifiable");
			if (reifiable.Reifier == newReifier) return;

			if (newReifier != null)
			{
				if (newReifier.Reified != null)
				{
					throw new ModelConstraintException(
						"The specified reifier already reifies another construct.");
				}
			}

			Topic oldReifier = reifiable.Reifier as Topic;

			// update old reifier.Reified
			if (oldReifier != null)
			{
				oldReifier.Reified = null;
			}

			// update new reifier.Reified
			if (newReifier != null)
			{
				newReifier.Reified = reifiable;
			}
		}

		public static void Reify(DatatypeAware reifiable, Topic newReifier)
		{
			Do(reifiable, newReifier);
			reifiable.reifier = newReifier;
		}

		public static void Reify(Association reifiable, Topic newReifier)
		{
			Do(reifiable, newReifier);
			reifiable.reifier = newReifier;
		}

		public static void Reify(TopicMap reifiable, Topic newReifier)
		{
			Do(reifiable, newReifier);
			reifiable.reifier = newReifier;
		}

		public static void Reify(Role reifiable, Topic newReifier)
		{
			Do(reifiable, newReifier);
			reifiable.reifier = newReifier;
		}

		public static void Reify(Name reifiable, Topic newReifier)
		{
			Do(reifiable, newReifier);
			reifiable.reifier = newReifier;
		}
	}
}