// <copyright file="Datatypes.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

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
	}
}