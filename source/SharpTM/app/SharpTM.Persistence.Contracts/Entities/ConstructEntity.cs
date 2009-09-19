// <copyright file="ConstructEntity.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;

namespace Pixelplastic.TopicMaps.SharpTM.Persistence.Contracts.Entities
{
	public class ConstructEntity
	{
		string _id;
		List<string> _itemIdentifiers;

		public virtual string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual List<string> ItemIdentifiers
		{
			get { return _itemIdentifiers;}
			set { _itemIdentifiers = value; }
		}

		public ConstructEntity()
		{
			_id = Guid.NewGuid().ToString();
			_itemIdentifiers = new List<string>();
		}
	}
}