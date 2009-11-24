// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRole.cs">
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
//   Represents an
//   <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-assoc-role">association role item</a>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.Core
{
    /// <summary>
    /// Represents an 
    /// <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-assoc-role">association role item</a>.
    /// </summary>
    public interface IRole : IReifiable, ITyped
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.IAssociation"/> to which this role belongs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:TMAPI.Net.Core.IAssociation"/> to which this role belongs.
        /// </returns>
        new IAssociation Parent
        {
            get;
        }

        /// <summary>
        /// Gets or sets the <see cref="T:TMAPI.Net.Core.ITopic"/> playing this role.
        /// </summary>
        /// <exception cref="ModelConstraintException">
        /// If trying to set the role player to <c>null</c> or if the player belongs to another topic map.
        /// </exception>
        ITopic Player
        {
            get;
            set;
        }

        #endregion
    }
}