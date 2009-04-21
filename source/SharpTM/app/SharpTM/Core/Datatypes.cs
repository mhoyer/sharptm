// <copyright file="Datatypes.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements a set of <c>datatypes</c> to be used in <see cref="DatatypeAware"/>.
	/// </summary>
	public static class Datatypes
	{
		/// <summary>
		/// Represents the URI string for anyURI <c>datatype</c>.
		/// </summary>
		public const string ANY_URI = "http://www.w3.org/2001/XMLSchema#anyURI";

		/// <summary>
		/// Represents the URI string for decimal <c>datatype</c>.
		/// </summary>
		public const string DECIMAL = "http://www.w3.org/2001/XMLSchema#decimal";

		/// <summary>
		/// Represents the URI string for float <c>datatype</c>.
		/// </summary>
		public const string FLOAT = "http://www.w3.org/2001/XMLSchema#float";

		/// <summary>
		/// Represents the URI string for int <c>datatype</c>.
		/// </summary>
		public const string INT = "http://www.w3.org/2001/XMLSchema#int";

		/// <summary>
		/// Represents the URI string for long <c>datatype</c>.
		/// </summary>
		public const string LONG = "http://www.w3.org/2001/XMLSchema#long";

		/// <summary>
		/// Represents the URI string for string <c>datatype</c>.
		/// </summary>
		public const string STRING = "http://www.w3.org/2001/XMLSchema#string";

		public static class Locators
		{
			/// <summary>
			/// Represents the locator for any URI <c>datatype</c>.
			/// </summary>
			public static readonly ILocator AnyUri = new Locator(ANY_URI);

			/// <summary>
			/// Represents the locator for decimal <c>datatype</c>.
			/// </summary>
			public static readonly ILocator Decimal = new Locator(DECIMAL);

			/// <summary>
			/// Represents the locator for float <c>datatype</c>.
			/// </summary>
			public static readonly ILocator Float = new Locator(FLOAT);

			/// <summary>
			/// Represents the locator for int <c>datatype</c>.
			/// </summary>
			public static readonly ILocator Int = new Locator(INT);

			/// <summary>
			/// Represents the locator for long <c>datatype</c>.
			/// </summary>
			public static readonly ILocator Long = new Locator(LONG);

			/// <summary>
			/// Represents the locator for string <c>datatype</c>.
			/// </summary>
			public static readonly ILocator String = new Locator(STRING);
		}
	}
}