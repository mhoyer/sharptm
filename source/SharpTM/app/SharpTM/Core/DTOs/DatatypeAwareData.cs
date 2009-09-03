// <copyright file="DatatypeAwareData.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core.DTOs
{
	public abstract class DatatypeAwareData : ConstructData
	{
		public ReadOnlyCollectionWithLimitedAccess<ITopic> Scope { get; set; }
		public ITopic Reifier { get; set; }
		public ILocator Datatype { get; set; }
		public string Value;
		public decimal DecimalValue;
		public float FloatValue;
		public int IntValue;
		public ILocator LocatorValue;
		public long LongValue;
		public string StringValue;

        public DatatypeAwareData()
		{
			Scope = new ReadOnlyCollectionWithLimitedAccess<ITopic>();
		}
	}
}