using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// An imutable representation of any IRI addressing scheme.
	/// </summary>
	/// <remarks>
	/// In this representation, an address (reference) is defined using a string and the 
	/// reference string is specified in a particular notation. For example, the majority 
	/// of network addresses are specified using the "URI" notation, and so the reference 
	/// string must conform to this notation specification.
	/// </remarks>
	public class Locator : ILocator
	{
		#region ILocator properties
		/// <summary>
		/// Gets the external form of the IRI.
		/// Any special character will be escaped using the escaping conventions of
		/// <a href="http://www.ietf.org/rfc/rfc3987.txt">RFC 3987</a>.
		/// A string representation of this locator suitable for output or passing to 
		/// APIs which will parse the locator anew.
		/// </summary>
		public string ExternalForm
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets a lexical representation of the IRI.
		/// </summary>
		public string Reference
		{
			get;
			private set;
		}
		#endregion

		#region ILocator methods
		/// <summary>
		/// Resolves the <paramref name="reference"/> against this <see cref="ILocator">locator</see>.
		/// The returned <c>Locator</c> represents an absolute IRI.
		/// </summary>
		/// <param name="reference">
		/// The reference which should be resolved against this locator.
		/// </param>
		/// <returns>
		/// A locator representing an absolute IRI.
		/// </returns>
		public ILocator Resolve(string reference)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Locator"/> class.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="notation">The notation.</param>
		public Locator(string address, string notation)
		{
			Reference = address;
			Notation = notation;
		}
		#endregion

	}
}