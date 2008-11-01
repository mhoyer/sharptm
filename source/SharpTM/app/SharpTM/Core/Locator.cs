// <copyright file="Locator.cs" company="Pixelplastic">
// Copyright (©) Marcel Hoyer 2008. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// An immutable representation of an IRI.
	/// </summary>
	public class Locator : ILocator
	{
		#region readonly & static fields
		/// <summary>
		/// Represents the current <see cref="Uri"/> of this <see cref="Locator"/>.
		/// </summary>
		private readonly Uri reference;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Locator"/> class using a <see cref="string"/>.
		/// </summary>
		/// <param name="absoluteReference">The absolute reference.</param>
		public Locator(string absoluteReference)
		{
			if (absoluteReference == null)
			{
				throw new TMAPIException("Referenced IRI cannot be null.", 
					new ArgumentNullException("absoluteReference"));
			}

			try
			{
				reference = new Uri(absoluteReference, UriKind.Absolute);
			}
			catch (UriFormatException ex)
			{
				throw new TMAPIException("Referenced IRI should be a valid absolute IRI.", ex);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Locator"/> class.
		/// </summary>
		/// <param name="absoluteReference">The absolute reference.</param>
		public Locator(Uri absoluteReference)
		{
			if (!absoluteReference.IsAbsoluteUri)
			{
				throw new TMAPIException("Referenced IRI should be a valid absolute IRI.");
			}

			reference = absoluteReference;
		}
		#endregion

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
			get
			{
				return reference.AbsoluteUri;
			}
		}

		/// <summary>
		/// Gets a lexical representation of the IRI.
		/// </summary>
		public string Reference
		{
			get
			{
				return reference.OriginalString;
			}
		}
		#endregion

		#region ILocator methods
		/// <summary>
		/// Resolves the <paramref name="relativeReference"/> against this <see cref="ILocator">locator</see>.
		/// The returned <c>Locator</c> represents an absolute IRI.
		/// </summary>
		/// <param name="relativeReference">
		/// The reference which should be resolved against this locator.
		/// </param>
		/// <returns>
		/// A locator representing an absolute IRI.
		/// </returns>
		public ILocator Resolve(string relativeReference)
		{
			return Resolve(new Uri(relativeReference, UriKind.Relative));
		}

		/// <summary>
		/// Resolves the <paramref name="relativeReference"/> against this <see cref="ILocator">locator</see>.
		/// The returned <c>Locator</c> represents an absolute IRI.
		/// </summary>
		/// <param name="relativeReference">
		/// The reference which should be resolved against this locator.
		/// </param>
		/// <returns>
		/// A locator representing an absolute IRI.
		/// </returns>
		public ILocator Resolve(Uri relativeReference)
		{
			return new Locator(new Uri(reference, relativeReference));
		}

		#endregion
	}
}
