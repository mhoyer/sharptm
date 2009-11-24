// <copyright file="DatatypeAware.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Pixelplastic.TopicMaps.SharpTM.Core.DTOs;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IDatatypeAware"/> interface.
	/// </summary>
	public abstract class DatatypeAware : Construct, IDatatypeAware
	{
		internal DatatypeAwareData datatypeData;

		/// <summary>
		/// Initializes a new instance of the <see cref="DatatypeAware"/> class.
		/// </summary>
		/// <param name="data">The inner data object.</param>
		/// <param name="parent">The parent of this instance.</param>
		/// <param name="topicMap">The topic map this instance is part of.</param>
		protected DatatypeAware(DatatypeAwareData data, IConstruct parent, ITopicMap topicMap)
			: base(data, parent, topicMap)
		{
			datatypeData = data;
		}

		#region IDatatypeAware properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ILocator"/> identifying the data type of the value. 
		/// </summary>
		/// <returns>
		///     The data type of this construct (never <c>null</c>).
		/// </returns>
		public ILocator Datatype
		{
			get { return datatypeData.Datatype; }
		}

		/// <summary>
		///     Gets or sets the <c>decimal</c> representation of the value.
		///     This method sets the data type implicitly to 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#decimal">xsd:decimal</a>.
		/// </summary>
		/// <exception cref="System.FormatException">
		///     If value isn't in <c>decimal</c> format or cannot converted into it.
		/// </exception>
		public decimal DecimalValue
		{
			get
			{
				if (Datatype.Reference != Datatypes.DECIMAL)
				{
					return decimal.Parse(datatypeData.StringValue, CultureInfo.CreateSpecificCulture("en"));
				}

				return datatypeData.DecimalValue;
			}
			set
			{
				datatypeData.DecimalValue = value;
				datatypeData.StringValue = value.ToString();
				datatypeData.Datatype = new Locator(Datatypes.DECIMAL);
			}
		}

		/// <summary>
		///     Gets or sets the <c>float</c> representation of the value.
		///     This method sets the data type implicitly to 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#float">xsd:float</a>.
		/// </summary>
		/// <exception cref="System.FormatException">
		///     If value isn't in <c>float</c> format or cannot converted into it.
		/// </exception>
		public float FloatValue
		{
			get
			{
				if (Datatype.Reference != Datatypes.FLOAT)
				{
					return float.Parse(datatypeData.StringValue, CultureInfo.CreateSpecificCulture("en"));
				}

				return datatypeData.FloatValue;
			}
			set
			{
				datatypeData.FloatValue = value;
				datatypeData.StringValue = value.ToString("F", CultureInfo.CreateSpecificCulture("en"));
				datatypeData.Datatype = new Locator(Datatypes.FLOAT);
			}
		}

		/// <summary>
		///     Gets or sets the <c>int</c> representation of the value.
		///     This method sets the data type implicitly to 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#int">xsd:int</a>.
		/// </summary>
		/// <exception cref="System.FormatException">
		///     If value isn't in <c>int</c> format or cannot converted into it.
		/// </exception>
		public int IntValue
		{
			get
			{
				if (Datatype.Reference != Datatypes.INT)
				{
					return int.Parse(datatypeData.StringValue, CultureInfo.CreateSpecificCulture("en"));
				}

				return datatypeData.IntValue;
			}
			set
			{
				datatypeData.IntValue = value;
				datatypeData.StringValue = value.ToString();
				datatypeData.Datatype = new Locator(Datatypes.INT);
			}
		}

		/// <summary>
		///     Gets or sets the <see cref="T:TMAPI.Net.Core.ILocator"/> representation of the value.
		/// </summary>
		/// <exception cref="T:TMAPI.Net.Core.ModelConstraintException">
		///     In case the <paramref name="value"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="System.InvalidOperationException">
		///     If trying to access the property but value is <c>null</c>.
		/// </exception>
		public ILocator LocatorValue
		{
			get
			{
				if (datatypeData.LocatorValue == null)
					throw new InvalidOperationException(
						"Trying to access the LocalValue property, but it's null.");

				return datatypeData.LocatorValue;
			}
			set
			{
				if (value == null)
					throw new ModelConstraintException(this, "LocatorValue cannot be set to null.");

				datatypeData.LocatorValue = value;
				datatypeData.StringValue = value.Reference;
				datatypeData.Datatype = new Locator(Datatypes.ANY_URI);
			}
		}

		/// <summary>
		///     Gets or sets the <c>long</c> representation of the value.
		///     This method sets the data type implicitly to 
		///     <a href="http://www.w3.org/TR/xmlschema-2/#long">xsd:long</a>.
		/// </summary>
		/// <exception cref="System.FormatException">
		///     If value isn't in <c>long</c> format or cannot converted into it.
		/// </exception>
		public long LongValue
		{
			get
			{
				if (Datatype.Reference != Datatypes.LONG)
				{
					return long.Parse(datatypeData.StringValue);
				}

				return datatypeData.LongValue;
			}
			set
			{
				datatypeData.LongValue = value;
				datatypeData.StringValue = value.ToString();
				datatypeData.Datatype = new Locator(Datatypes.LONG);
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
				return datatypeData.Reifier;
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
			get
			{
				return datatypeData.Scope;
			}
		}

		/// <summary>
		///     Gets or sets the lexical representation of the value.
		///     For the data type <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a> 
		///     the string itself is returned.
		///     For the data type <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a> 
		///     the <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> is returned.
		///     This method sets the data type implicitly to
		///     <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>.
		/// </summary>
		/// <exception cref="ModelConstraintException">
		///     If value is <c>null</c>.
		/// </exception>
		public string Value
		{
			get
			{
				switch (Datatype.Reference)
				{
					case Datatypes.ANY_URI:
						return datatypeData.LocatorValue.Reference;
					default:
						return datatypeData.StringValue;
				}
			}
			set
			{
				if (value == null)
				{
					throw new ModelConstraintException(this, "The value MUST NOT be null.");
				}

				datatypeData.StringValue = value;

				if (Datatype == null)
				{
					datatypeData.Datatype = new Locator(Datatypes.STRING);
				}
			}
		}
		#endregion

		#region IDatatypeAware methods
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
			ScopeHelper.AddTheme(this, datatypeData.Scope, theme);
		}

		/// <summary>
		///     Removes a <see cref="T:TMAPI.Net.Core.ITopic"/> from the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be removed from the scope.
		/// </param>
		public void RemoveTheme(ITopic theme)
		{
			ScopeHelper.RemoveTheme(datatypeData.Scope, theme);
		}

		/// <summary>
		///     Sets the string value and the data type.
		/// </summary>
		/// <param name="value">
		///     The string value.
		/// </param>
		/// <param name="datatype">
		///     The value's data type.
		/// </param>
		/// <exception cref="T:TMAPI.Net.Core.ModelConstraintException">
		///     In case the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="System.FormatException">
		///     If value couldn't converted into format specified by <paramref name="datatype"/>.
		/// </exception>
		public void SetValue(string value, ILocator datatype)
		{
			if (value == null)
			{
				throw new ModelConstraintException(
					this, 
					"An DatatypeAware value MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			if (datatype == null)
			{
				throw new ModelConstraintException(
					this,
					"An DatatypeAware datatype MUST NOT be null.",
					new ArgumentNullException("datatype"));
			}

			switch (datatype.Reference)
			{
				case Datatypes.STRING:
					{
						Value = value;
						break;
					}
				case Datatypes.ANY_URI:
					{
						try
						{
							LocatorValue = new Locator(value);
						}
						catch (Exception ex)
						{
							throw new FormatException("Unable to convert string to Locator.", ex);
						}

						break;
					}
				case Datatypes.DECIMAL:
					{
						DecimalValue = decimal.Parse(value);
						break;
					}
				case Datatypes.FLOAT:
					{
						FloatValue = float.Parse(value);
						break;
					}
				case Datatypes.INT:
					{
						IntValue = int.Parse(value);
						break;
					}
				case Datatypes.LONG:
					{
						LongValue = long.Parse(value);
						break;
					}
				default:
					{
						Value = value;
						datatypeData.Datatype = datatype;
						break;
					}
			}
		}
		#endregion

		/// <summary>
		/// Adds a list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> to the scope.
		/// </summary>
		/// <param name="themes">The list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> that should be added to the scope.</param>
		public void AddThemes(IEnumerable<ITopic> themes)
		{
			ScopeHelper.AddThemes(this, datatypeData.Scope, themes);
		}
	}
}