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
        /// If trying to set the role player to <c>null</c>.
        /// </exception>
        ITopic Player
        {
            get;
            set;
        }

        #endregion
    }
}