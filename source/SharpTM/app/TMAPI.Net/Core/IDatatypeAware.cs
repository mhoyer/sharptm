// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatatypeAware.cs">
//  TMAPI.Net was created collectively by the membership of the tmapinet-discuss mailing list 
//  (https://lists.sourceforge.net/lists/listinfo/tmapinet-discuss) with support by the 
//  tmapi-discuss mailing list (http://lists.sourceforge.net/mailman/listinfo/tmapi-discuss),
//  and is hereby released into the public domain; and comes with NO WARRANTY.
//  
//  No one owns TMAPI.Net: you may use it freely in both commercial and
//  non-commercial applications, bundle it with your software
//  distribution, include it on a CD-ROM, list the source code in a
//  book, mirror the documentation at your own web site, or use it in
//  any other way you see fit.
// </copyright>
// <summary>
//   Common base interface for <see cref="T:TMAPI.Net.Core.IOccurrence" />s and <see cref="T:TMAPI.Net.Core.IVariant" />s.
//   Some convenience methods for a subset of <a href="http://www.w3.org/TR/xmlschema-2/">XML Schema Part 2: Datatypes</a>
//   are supported.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Core
{
    /// <summary>
    /// Common base interface for <see cref="T:TMAPI.Net.Core.IOccurrence"/>s and <see cref="T:TMAPI.Net.Core.IVariant"/>s.
    /// Some convenience methods for a subset of <a href="http://www.w3.org/TR/xmlschema-2/">XML Schema Part 2: Datatypes</a>
    /// are supported.
    /// </summary>
    public interface IDatatypeAware : IReifiable, IScoped
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ILocator"/> identifying the data type of the value. 
        /// </summary>
        /// <returns>
        /// The data type of this construct (never <c>null</c>).
        /// </returns>
        ILocator Datatype
        {
            get;
        }

        /// <summary>
        /// Gets or sets the <c>decimal</c> representation of the value.
        /// This method sets the data type implicitly to 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#decimal">xsd:decimal</a>.
        /// </summary>
        /// <exception cref="System.FormatException">
        /// If value isn't in <c>decimal</c> format or cannot converted into it.
        /// </exception>
        decimal DecimalValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>float</c> representation of the value.
        /// This method sets the data type implicitly to 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#float">xsd:float</a>.
        /// </summary>
        /// <exception cref="System.FormatException">
        /// If value isn't in <c>float</c> format or cannot converted into it.
        /// </exception>
        float FloatValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>int</c> representation of the value.
        /// This method sets the data type implicitly to 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#int">xsd:int</a>.
        /// </summary>
        /// <exception cref="System.FormatException">
        /// If value isn't in <c>int</c> format or cannot converted into it.
        /// </exception>
        int IntValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="T:TMAPI.Net.Core.ILocator"/> representation of the value.
        /// </summary>
        /// <exception cref="T:TMAPI.Net.Core.ModelConstraintException">
        /// In case the <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// If trying to access the property but value is <c>null</c>.
        /// </exception>
        ILocator LocatorValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>long</c> representation of the value.
        /// This method sets the data type implicitly to 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#long">xsd:long</a>.
        /// </summary>
        /// <exception cref="System.FormatException">
        /// If value isn't in <c>long</c> format or cannot converted into it.
        /// </exception>
        long LongValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lexical representation of the value.
        /// For the data type <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a> 
        /// the string itself is returned.
        /// For the data type <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a> 
        /// the <see cref="P:TMAPI.Net.Core.ILocator.Reference"/> is returned.
        /// This method sets the data type implicitly to
        /// <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>.
        /// </summary>
        /// <exception cref="ModelConstraintException">
        /// If value is <c>null</c>.
        /// </exception>
        string Value
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the string value and the data type.
        /// </summary>
        /// <param name="value">
        /// The string value.
        /// </param>
        /// <param name="datatype">
        /// The value's data type.
        /// </param>
        /// <exception cref="T:TMAPI.Net.Core.ModelConstraintException">
        /// In case the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// If value couldn't converted into format specified by <paramref name="datatype"/>.
        /// </exception>
        void SetValue(string value, ILocator datatype);

        #endregion
    }
}