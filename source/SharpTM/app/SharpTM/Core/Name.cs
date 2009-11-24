// <copyright file="Name.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pixelplastic.TopicMaps.SharpTM.Core.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IName"/> interface.
	/// </summary>
	public class Name : Construct, IName
	{
		internal NameData nameData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Name"/> class.
		/// </summary>
		/// <param name="parent">The parent topic for this instance.</param>
		/// <param name="nameType">Type for this name instance.</param>
		internal Name(ITopic parent, ITopic nameType)
			: this(new NameData(), parent, nameType)
		{}

		internal Name(NameData data, ITopic parent, ITopic nameType)
			: base(data, parent, parent.TopicMap)
		{
			if (data == null) throw new ArgumentNullException("data");

			nameData = data;

			if (nameType == null &&
				nameData.Type == null)
			{
				throw new ModelConstraintException(
					this,
					"The type of a name MUST NOT be null.",
					new ArgumentNullException("nameType"));
			}
			
			if (nameType != null) Type = nameType;
		}

		#region IName properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/> to which this name belongs.
		/// </summary>
		/// <returns>
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> to which this name belongs.
		/// </returns>
		public new ITopic Parent
		{
			get
			{
				return base.Parent as ITopic;
			}
			internal set
			{
				base.Parent = value;
			}
		}

		/// <summary>
		///     Gets or sets the reifier of this construct.
		/// </summary>
		/// <remarks>
		///     <list type="bullet">
		///         <item>If this construct is not reified <c>null</c> is returned.</item>
		///         <item>If the reifier is set to <c>null</c> an existing reifier should be removed.</item>
		///         <item>The reifier of this construct MUST NOT reify another information item.</item>
		///     </list>
		/// </remarks>
		public ITopic Reifier
		{
			get
			{
				return nameData.Reifier;
			}
			set
			{
				ReificationHelper.Reify(this, value as Topic);
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		///     An empty set represents the unconstrained scope.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
		/// </returns>
		public ReadOnlyCollection<ITopic> Scope
		{
			get { return nameData.Scope; }
		}

		/// <summary>
		///     Gets or sets the type of this construct.
		/// </summary>
		/// <exception cref="ModelConstraintException">
		///     If the type is <c>null</c>.
		/// </exception>
		/// <remarks>
		///     Any previous type is overridden.
		/// </remarks>
		public ITopic Type
		{
			get
			{
				return nameData.Type;
			}
			set
			{
				if (value == null) throw new ModelConstraintException(this, "Type MUST NOT be null.");
				nameData.Type = value;
			}
		}

		/// <summary>
		///     Gets or sets a string representing the value of this name.
		/// </summary>
		/// <exception cref="ModelConstraintException">
		///     If trying to assign <c>null</c> as value.
		/// </exception>
		public string Value
		{
			get
			{
				return nameData.Value;
			}
			set
			{
				if (value == null)
					throw new ModelConstraintException(this, "The value of a Name MUST NOT be null.");

				nameData.Value = value;
			}
		}

		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.IVariant"/>s defined for this name.
		///     The return value may be empty but must never be <c>null</c>.
		/// </summary>
		/// <returns>
		///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
		/// </returns>
		public ReadOnlyCollection<IVariant> Variants
		{
			get { return nameData.Variants; }
		}
		#endregion

		#region IName methods
		/// <summary>
		///     Adds a <see cref="T:TMAPI.Net.Core.ITopic"/> to the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be added to the scope.
		/// </param>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="theme"/> is <c>null</c>.
		/// </exception>
		public void AddTheme(ITopic theme)
		{
			ScopeHelper.AddTheme(this, nameData.Scope, theme);
		}

		/// <summary>
		///     Removes a <see cref="T:TMAPI.Net.Core.ITopic"/> from the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be removed from the scope.
		/// </param>
		public void RemoveTheme(ITopic theme)
		{
			ScopeHelper.RemoveTheme(nameData.Scope, theme);
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
		///     string <paramref name="value"/> and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the <c>datatype</c>
		///     <a href="http://www.w3.org/TR/xmlschema-2/#string"><c>xsd:string</c></a>.
		/// </summary>
		/// <param name="value">
		///     The string value.
		/// </param>
		/// <param name="scope">
		///     An array (length >= 1) of themes.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="value"/> is <c>null</c>, or the scope of the variant would not be 
		///     a true superset of the name's scope.
		/// </exception>
		public IVariant CreateVariant(string value, params ITopic[] scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The scope for a variant instance MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			return CreateVariant(value, new List<ITopic>(scope));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
		///     string <paramref name="value"/> and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>.
		/// </summary>
		/// <param name="value">
		///     The string value.
		/// </param>
		/// <param name="scope">
		///     A collection (size >= 1) of themes.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="value"/> is <c>null</c>, or the scope of the variant would not be 
		///     a true superset of the name's scope.
		/// </exception>
		public IVariant CreateVariant(string value, IList<ITopic> scope)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					this,
					"The value of a variant instance MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			Variant variant = CreateVariant(scope);
			variant.Value = value;

			return variant;
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
		///     IRI <paramref name="value"/> and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a>.
		/// </summary>
		/// <param name="value">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
		/// </param>
		/// <param name="scope">
		///     An array (length >= 1) of themes.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="value"/> is <c>null</c>, or the scope of the variant would not be 
		///     a true superset of the name's scope.
		/// </exception>
		public IVariant CreateVariant(ILocator value, params ITopic[] scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The scope for a variant instance MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			return CreateVariant(value, new List<ITopic>(scope));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
		///     IRI <paramref name="value"/> and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a>.
		/// </summary>
		/// <param name="value">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
		/// </param>
		/// <param name="scope">
		///     A collection (size >= 1) of themes.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="value"/> is <c>null</c>, or the scope of the variant would not be 
		///     a true superset of the name's scope.
		/// </exception>
		public IVariant CreateVariant(ILocator value, IList<ITopic> scope)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					this,
					"The value of a variant instance MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			Variant variant = CreateVariant(scope);
			variant.LocatorValue = value;

			return variant;
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
		///     string <paramref name="value"/>, <paramref name="datatype"/>, and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
		///     specified by <paramref name="datatype"/>.
		/// </summary>
		/// <param name="value">
		///     The string value.
		/// </param>
		/// <param name="datatype">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> indicating the datatype of the <paramref name="value"/>.
		/// </param>
		/// <param name="scope">
		///     An array (length >= 1) of themes.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>, or the scope of the variant would not be 
		///     a true superset of the name's scope.
		/// </exception>
		public IVariant CreateVariant(string value, ILocator datatype, params ITopic[] scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The scope for a variant instance MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			return CreateVariant(value, datatype, new List<ITopic>(scope));
		}

		/// <summary>
		///     Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
		///     string <paramref name="value"/>, <paramref name="datatype"/>, and <paramref name="scope"/>.
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
		///     specified by <paramref name="datatype"/>.
		/// </summary>
		/// <param name="value">
		///     The string value.
		/// </param>
		/// <param name="datatype">
		///     A <see cref="T:TMAPI.Net.Core.ILocator"/> indicating the datatype of the <paramref name="value"/>.
		/// </param>
		/// <param name="scope">
		///     A collection (size >= 1) of themes.
		/// </param>
		/// <returns>
		///     The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
		/// </returns>
		/// <exception cref="ModelConstraintException">
		///     If the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>, or the scope of the variant would not be 
		///     a true superset of the name's scope.
		/// </exception>
		public IVariant CreateVariant(string value, ILocator datatype, IList<ITopic> scope)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					this,
					"The value of a variant instance MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			if (datatype == null)
			{
				throw new ModelConstraintException(
					this,
					"The datatype locator of a variant instance MUST NOT be null.",
					new ArgumentNullException("datatype"));
			}

			Variant variant = CreateVariant(scope);
			variant.SetValue(value, datatype);

			return variant;
		}
		#endregion

		public override string ToString()
		{
			return String.Format("{0} \"{1}\"", Id, Value);
		}

		/// <summary>
		/// Adds a list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> to the scope.
		/// </summary>
		/// <param name="themes">The list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> that should be added to the scope.</param>
		public void AddThemes(IEnumerable<ITopic> themes)
		{
			ScopeHelper.AddThemes(this, nameData.Scope, themes);
		}

		/// <summary>
		/// Compares two <see cref="IName"/> instances using the TMDM rules.
		/// </summary>
		/// <param name="name">The name instance to be compared.</param>
		/// <returns>[true] if both instances are equal. otherwise [false].</returns>
		/// <remarks>
		/// Topic name items are equal if the values of their
		/// [value], [type], [scope], and [parent] properties are equal.
		/// </remarks>
		public bool Equals(IName name)
		{
			return Equals(name, false);
		}

		/// <summary>
		/// Compares two <see cref="IName"/> instances using the TMDM rules.
		/// </summary>
		/// <param name="name">The name instance to be compared.</param>
		/// <param name="ignoreParent">if set to <c>true</c> the parent <see cref="ITopic"/> will be ignored.</param>
		/// <returns>
		/// [true] if both instances are equal. otherwise [false].
		/// </returns>
		/// <remarks>
		/// Topic name items are equal if the values of their
		/// [value], [type], [scope], and [parent] properties are equal.
		/// </remarks>
		public bool Equals(IName name, bool ignoreParent)
		{
			if (name == this)
			{
				return true;
			}

			if (name == null ||
			    name.Value != Value ||
			    name.Scope.Count != Scope.Count ||
			    !name.Type.Equals(Type) ||
			    (!ignoreParent && !name.Parent.Equals(Parent)))
			{
				return false;
			}

			foreach (ITopic scope in Scope)
			{
				if (!name.Scope.Contains(scope))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Finds a variant that matches the <paramref name="pattern"/>.
		/// </summary>
		/// <param name="pattern">A pattern with set properties that should match.</param>
		/// <param name="ignoreParent">if set to <c>true</c> the <see cref="Parent"/> property will be ignored.</param>
		/// <returns>The found variant or null.</returns>
		public Variant FindVariant(IVariant pattern, bool ignoreParent)
		{
			foreach (Variant variant in Variants)
			{
				if (variant == pattern)
				{
					continue;
				}

				if (variant.Equals(pattern, ignoreParent))
				{
					return variant;
				}
			}

			return null;
		}

		/// <summary>
		/// Finds a variant that matches the <paramref name="pattern"/>.
		/// </summary>
		/// <param name="pattern">A pattern with set properties that should match.</param>
		/// <returns>The found variant or null.</returns>
		public Variant FindVariant(IVariant pattern)
		{
			return FindVariant(pattern, false);
		}

		internal void AddVariant(IVariant variant)
		{
			CheckSuperSet(variant.Scope);
			nameData.Variants.Add(variant);

			if (variant is Variant)
			{
				((Variant) variant).OnRemove += Variant_OnRemove;
			}
		}

		/// <summary>
		/// Determines whether the given <paramref name="scope"/> is a super set 
		/// of the <see cref="Scope"/> of this <see cref="IName"/> instance.
		/// </summary>
		/// <param name="scope">The scope to be checked.</param>
		/// <exception cref="ModelConstraintException">
		///     If the <see cref="scope" /> of the variant would not be 
		///     a true superset of the name's scope.
		/// </exception>
		void CheckSuperSet(IList<ITopic> scope)
		{
			bool variantScopeHasDifferentTheme = false;

			foreach (ITopic theme in scope)
			{
				if (!Scope.Contains(theme))
				{
					variantScopeHasDifferentTheme = true;
					break;
				}
			}

			if (!variantScopeHasDifferentTheme)
			{
				throw new ModelConstraintException(
					this,
					String.Format(
						"The scope of a variant MUST be a true super set of its parent name {0}.",
						this),
					new ArgumentException("scope"));
			}
		}

		/// <summary>
		/// Creates a simple variant.
		/// </summary>
		/// <param name="scope">A non empty list of scope themes.</param>
		/// <returns>The created instance of a Variant.</returns>
		/// <exception cref="ModelConstraintException">
		///     If the <see cref="scope" /> of the variant would not be 
		///     a true superset of the name's scope.
		/// </exception>
		Variant CreateVariant(IList<ITopic> scope)
		{
			if (scope == null)
			{
				throw new ModelConstraintException(
					this,
					"The scope for a variant instance MUST NOT be null.",
					new ArgumentNullException("scope"));
			}

			if (scope.Count <= 0)
			{
				throw new ModelConstraintException(
					this,
					"The array size of the scope for a variant instance MUST be >= 1.",
					new ArgumentException("scope"));
			}

			CheckSuperSet(scope);

			Variant variant = new Variant(this, TopicMap);
			variant.AddThemes(scope);
			AddVariant(variant);

			// HACK should be solved by delegates
			if (TopicMap is TopicMap)
			{
				((TopicMap) TopicMap).AddConstruct(variant);
			}

			return variant;
		}

		/// <summary>
		/// Handles the <see cref="Construct.OnRemove"/> event of a <see cref="Variant"/> construct.
		/// </summary>
		/// <param name="sender">The source <see cref="IVariant"/> of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Variant_OnRemove(object sender, EventArgs e)
		{
			if (sender is IVariant)
			{
				nameData.Variants.Remove((IVariant)sender);
			}
		}
	}
}