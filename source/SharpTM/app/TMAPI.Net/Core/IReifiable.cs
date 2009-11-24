// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReifiable.cs">
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
//   Indicates that a <see cref="T:TMAPI.Net.Core.IConstruct" /> is reifiable.
//   Every Topic Maps construct that is not a <see cref="T:TMAPI.Net.Core.ITopic" /> is reifiable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Core
{
    /// <summary>
    /// Indicates that a <see cref="T:TMAPI.Net.Core.IConstruct"/> is reifiable.
    /// Every Topic Maps construct that is not a <see cref="T:TMAPI.Net.Core.ITopic"/> is reifiable.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Reifiable", Justification = "Its the naming convention of TMAPI.")]
    public interface IReifiable : IConstruct
    {
        #region Properties

        /// <summary>
        /// Gets or sets the reifier of this construct.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        ///     <item>If this construct is not reified <c>null</c> is returned.</item>
        ///     <item>If the reifier is set to <c>null</c> an existing reifier should be removed.</item>
        ///     <item>The reifier of this construct MUST NOT reify another information item.</item>
        /// </list>
        /// </remarks>
        /// <exception cref="ModelConstraintException">
        /// If the specified <tt>reifier</tt> reifies another construct or reifier belongs to another topic map.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
                "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Reifier",
                Justification = "Its the naming convention of TMAPI.")]
        ITopic Reifier
        {
            get;
            set;
        }

        #endregion
    }
}