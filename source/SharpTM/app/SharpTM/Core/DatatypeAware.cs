using System.Collections.ObjectModel;
using TMAPI.Net.Core;

namespace Pixelplastic.TopicMaps.SharpTM.Core
{
	/// <summary>
	/// Implements the <see cref="IDatatypeAware"/> interface.
	/// </summary>
	public class DatatypeAware : Construct, IDatatypeAware
	{
		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="DatatypeAware"/> class.
		/// </summary>
		/// <param name="parent">The parent of this instance.</param>
		/// <param name="topicMap">The topic map this instance is part of.</param>
		public DatatypeAware(IConstruct parent, ITopicMap topicMap)
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
			get
			{
				throw new System.NotImplementedException();
			}
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
				throw new System.NotImplementedException();
			}
			set
			{
				throw new System.NotImplementedException();
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
				throw new System.NotImplementedException();
			}
			set
			{
				throw new System.NotImplementedException();
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
				throw new System.NotImplementedException();
			}
			set
			{
				throw new System.NotImplementedException();
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
				throw new System.NotImplementedException();
			}
			set
			{
				throw new System.NotImplementedException();
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
				throw new System.NotImplementedException();
			}
			set
			{
				throw new System.NotImplementedException();
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
				throw new System.NotImplementedException();
			}
			set
			{
				throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
		}
		#endregion
	}
}