// <copyright file="Features.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Reflection;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	public class Features
	{
		public const string AutomaticMerging = "http://tmapi.org/features/automerge/";
		public const string TopicMapsModelFeatures = "http://tmapi.org/features/model/";
		public const string MergingSupportFeatures = "http://tmapi.org/features/merge/";
		public const string LocatorAddressNotationFeatures = "http://tmapi.org/features/notation/";
		public const string ReadOnlySystem = "http://tmapi.org/features/readOnly/";

		public static string MapToName(string featureUrl)
		{
			if (featureUrl == null)
			{
				throw new ArgumentNullException("featureUrl");
			}

			var fields = typeof(Features).GetFields(BindingFlags.Public | BindingFlags.Static);
			
			foreach (FieldInfo info in fields)
			{
				if (info.IsLiteral && 
					info.FieldType == typeof(string) && 
					((string) info.GetValue(null)).StartsWith(featureUrl))
				{
					return info.Name;
				}
			}

			throw new FeatureNotRecognizedException(
				String.Format("Unable to find feature by URI {0}", featureUrl));
		}
	}
}