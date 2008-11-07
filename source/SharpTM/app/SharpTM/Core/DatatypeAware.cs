using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IDatatypeAware"/> interface.
	/// </summary>
	public abstract class DatatypeAware : Construct, IDatatypeAware
	{
		#region fields
		private decimal decimalValue;
		private float floatValue;
		private int intValue;
		private ILocator locatorValue;
		private long longValue;
		private string stringValue;
		#endregion

		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="DatatypeAware"/> class.
		/// </summary>
		/// <param name="parent">The parent of this instance.</param>
		/// <param name="topicMap">The topic map this instance is part of.</param>
		protected DatatypeAware(IConstruct parent, ITopicMap topicMap)
			: base(parent, topicMap)
		{
		}
		#endregion

		#region IDatatypeAware properties
		/// <summary>
		///     Gets the <see cref="T:TMAPI.Net.Core.ILocator"/> identifying the data type of the value. 
		/// </summary>
		/// <returns>
		///     The data type of this construct (never <c>null</c>).
		/// </returns>
		public ILocator Datatype
		{
			get;
			private set;
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
					return decimal.Parse(stringValue);
				}

				return decimalValue;
			}
			set
			{
				decimalValue = value;
				stringValue = value.ToString();
				Datatype = new Locator(Datatypes.DECIMAL);
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
					return float.Parse(stringValue);
				}

				return floatValue;
			}
			set
			{
				floatValue = value;
				stringValue = value.ToString();
				Datatype = new Locator(Datatypes.FLOAT);
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
					return int.Parse(stringValue);
				}

				return intValue;
			}
			set
			{
				intValue = value;
				stringValue = value.ToString();
				Datatype = new Locator(Datatypes.INT);
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
				if (locatorValue == null)
				{
					throw new InvalidOperationException("Trying to access the LocalValue property, but it's null.");
				}

				return locatorValue;
			}
			set
			{
				if (value == null)
				{
					throw new ModelConstraintException("LocatorValue cannot be set to null.");
				}

				locatorValue = value;
				stringValue = value.Reference;
				Datatype = new Locator(Datatypes.ANY_URI);
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
					return long.Parse(stringValue);
				}

				return longValue;
			}
			set
			{
				longValue = value;
				stringValue = value.ToString();
				Datatype = new Locator(Datatypes.LONG);
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
		public new ITopic Reifier
		{
			get
			{
				return base.Reifier;
			}
			set
			{
				base.Reifier = value;
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
		public new ReadOnlyCollection<ITopic> Scope
		{
			get
			{
				return base.Scope;
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
						return locatorValue.Reference;
					default:
						return stringValue;
				}
			}
			set
			{
				if (value == null)
				{
					throw new ModelConstraintException("The value MUST NOT be null.");
				}

				stringValue = value;
				Datatype = new Locator(Datatypes.STRING);
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
		public new void AddTheme(ITopic theme)
		{
			base.AddTheme(theme);
		}

		/// <summary>
		///     Removes a <see cref="T:TMAPI.Net.Core.ITopic"/> from the scope.
		/// </summary>
		/// <param name="theme">
		///     The <see cref="T:TMAPI.Net.Core.ITopic"/> which should be removed from the scope.
		/// </param>
		public new void RemoveTheme(ITopic theme)
		{
			base.RemoveTheme(theme);
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
					"An DatatypeAware value MUST NOT be null.",
					new ArgumentNullException("value"));
			}

			if (datatype == null)
			{
				throw new ModelConstraintException(
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
						Datatype = datatype;
						break;
					}
			}
		}
		#endregion

		#region methods
		/// <summary>
		/// Adds a list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> to the scope.
		/// </summary>
		/// <param name="themes">The list of <see cref="T:TMAPI.Net.Core.ITopic">topics</see> that should be added to the scope.</param>
		public new void AddThemes(IEnumerable<ITopic> themes)
		{
			base.AddThemes(themes);
		}
		#endregion
	}
}