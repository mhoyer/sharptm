namespace TMAPI.Net.Core
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a 
    /// <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-topic-name">topic name item</a>.
    /// </summary>
    public interface IName : ITyped, IScoped, IReifiable
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/> to which this name belongs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:TMAPI.Net.Core.ITopic"/> to which this name belongs.
        /// </returns>
        new ITopic Parent
        {
            get;
        }

        /// <summary>
        /// Gets or sets a string representing the value of this name.
        /// </summary>
        /// <exception cref="ModelConstraintException">
        /// If trying to assign <c>null</c> as value.
        /// </exception>
        string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.IVariant"/>s defined for this name.
        /// The return value may be empty but must never be <c>null</c>.
        /// </summary>
        /// <returns>
        ///     An unmodifiable set of <see cref="T:TMAPI.Net.Core.IVariant"/>s.
        /// </returns>
        ReadOnlyCollection<IVariant> Variants
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
        /// string <paramref name="value"/> and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>.
        /// </summary>
        /// <param name="value">
        /// The string value.
        /// </param>
        /// <param name="scope">
        /// An array (length &gt;= 1) of themes.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="value"/> is <c>null</c>, or the scope of the variant would not be 
        /// a true superset of the name's scope.
        /// </exception>
        IVariant CreateVariant(string value, params ITopic[] scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
        /// string <paramref name="value"/> and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#string">xsd:string</a>.
        /// </summary>
        /// <param name="value">
        /// The string value.
        /// </param>
        /// <param name="scope">
        /// A collection (size &gt;= 1) of themes.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="value"/> is <c>null</c>, or the scope of the variant would not be 
        /// a true superset of the name's scope.
        /// </exception>
        IVariant CreateVariant(string value, IList<ITopic> scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
        /// IRI <paramref name="value"/> and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a>.
        /// </summary>
        /// <param name="value">
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
        /// </param>
        /// <param name="scope">
        /// An array (length &gt;= 1) of themes.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="value"/> is <c>null</c>, or the scope of the variant would not be 
        /// a true superset of the name's scope.
        /// </exception>
        IVariant CreateVariant(ILocator value, params ITopic[] scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
        /// IRI <paramref name="value"/> and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
        /// <a href="http://www.w3.org/TR/xmlschema-2/#anyURI">xsd:anyURI</a>.
        /// </summary>
        /// <param name="value">
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> which represents an IRI.
        /// </param>
        /// <param name="scope">
        /// A collection (size &gt;= 1) of themes.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="value"/> is <c>null</c>, or the scope of the variant would not be 
        /// a true superset of the name's scope.
        /// </exception>
        IVariant CreateVariant(ILocator value, IList<ITopic> scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
        /// string <paramref name="value"/>, <paramref name="datatype"/>, and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
        /// specified by <paramref name="datatype"/>.
        /// </summary>
        /// <param name="value">
        /// The string value.
        /// </param>
        /// <param name="datatype">
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> indicating the datatype of the <paramref name="value"/>.
        /// </param>
        /// <param name="scope">
        /// An array (length &gt;= 1) of themes.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>, or the scope of the variant would not be 
        /// a true superset of the name's scope.
        /// </exception>
        IVariant CreateVariant(string value, ILocator datatype, params ITopic[] scope);

        /// <summary>
        /// Creates a <see cref="T:TMAPI.Net.Core.IVariant"/> of this topic name with the specified 
        /// string <paramref name="value"/>, <paramref name="datatype"/>, and <paramref name="scope"/>.
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/> will have the datatype 
        /// specified by <paramref name="datatype"/>.
        /// </summary>
        /// <param name="value">
        /// The string value.
        /// </param>
        /// <param name="datatype">
        /// A <see cref="T:TMAPI.Net.Core.ILocator"/> indicating the datatype of the <paramref name="value"/>.
        /// </param>
        /// <param name="scope">
        /// A collection (size &gt;= 1) of themes.
        /// </param>
        /// <returns>
        /// The newly created <see cref="T:TMAPI.Net.Core.IVariant"/>.
        /// </returns>
        /// <exception cref="ModelConstraintException">
        /// If the <paramref name="value"/> or <paramref name="datatype"/> is <c>null</c>, or the scope of the variant would not be 
        /// a true superset of the name's scope.
        /// </exception>
        IVariant CreateVariant(string value, ILocator datatype, IList<ITopic> scope);

        #endregion
    }
}