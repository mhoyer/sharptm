// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVariant.cs">
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
//   Represents a
//   <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-variant">variant item</a>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Core
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a 
    /// <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-variant">variant item</a>.
    /// </summary>
    public interface IVariant : IDatatypeAware
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.IName"/> to which this variant belongs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:TMAPI.Net.Core.IName"/> to which this variant belongs.
        /// </returns>
        new IName Parent
        {
            get;
        }

        /// <summary>
        /// Gets the scope of this variant.
        /// </summary>
        /// <remarks>
        /// The returned scope is a true superset of the parent's scope.
        /// </remarks>
        /// <returns>
        /// An unmodifiable set of <see cref="T:TMAPI.Net.Core.ITopic"/>s which define the scope.
        /// </returns>
        new ReadOnlyCollection<ITopic> Scope
        {
            get;
        }

        #endregion
    }
}