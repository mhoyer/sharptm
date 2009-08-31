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
        /// If the type is <c>null</c>.
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