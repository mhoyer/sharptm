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
        /// <exception cref="ModelConstraintException">If the specified <tt>reifier</tt> reifies another construct.</exception>
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