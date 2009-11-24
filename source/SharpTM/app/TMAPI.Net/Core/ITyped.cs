// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITyped.cs">
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
//   Indicates that a Topic Maps construct is typed.
//   <see cref="T:TMAPI.Net.Core.IAssociation" />s, <see cref="T:TMAPI.Net.Core.IRole" />s,
//   <see cref="T:TMAPI.Net.Core.IOccurrence" />s, and <see cref="T:TMAPI.Net.Core.IName" />s are typed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Core
{
    /// <summary>
    /// Indicates that a Topic Maps construct is typed.
    /// <see cref="T:TMAPI.Net.Core.IAssociation"/>s, <see cref="T:TMAPI.Net.Core.IRole"/>s, 
    /// <see cref="T:TMAPI.Net.Core.IOccurrence"/>s, and <see cref="T:TMAPI.Net.Core.IName"/>s are typed.
    /// </summary>
    public interface ITyped : IConstruct
    {
        #region Properties

        /// <summary>
        /// Gets or sets the type of this construct.
        /// </summary>
        /// <exception cref="ModelConstraintException">
        /// If the type is <c>null</c> or if the type belongs to another topic map.
        /// </exception>
        /// <remarks>
        /// Any previous type is overridden.
        /// </remarks>
        ITopic Type
        {
            get;
            set;
        }

        #endregion
    }
}