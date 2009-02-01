// <copyright file="Locator.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
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
		/// <summary>
		/// Represents the current <see cref="Uri"/> of this <see cref="Locator"/>.
		/// </summary>
		private readonly Uri reference;

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
		/// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Locator"/>.
		/// </summary>
		/// <param name="locator">The <see cref="object"/> to compare with the current <see cref="Locator"/>.</param>
		/// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="Locator"/> otherwise, false</returns>
		public override bool Equals(object locator)
		{
			if (locator != null && locator is ILocator)
			{
				return Equals(locator as ILocator);
			}

			return base.Equals(locator);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="Locator"/>.
		/// </returns>
		public override int GetHashCode()
		{
			return (reference != null ? reference.GetHashCode() : 0);
		}
		#endregion

		/// <summary>
		/// Determines whether the specified <see cref="ILocator"/> is equal to the current <see cref="Locator"/>.
		/// </summary>
		/// <param name="locator">The <see cref="ILocator"/> to compare with the current <see cref="Locator"/>.</param>
		/// <returns>true if the specified <see cref="ILocator"/> is equal to the current <see cref="Locator"/> otherwise, false</returns>
		public bool Equals(ILocator locator)
		{
			if (locator.Reference == Reference)
			{
				return true;
			}

			return false;
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

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			return Reference;
		}
	}
}