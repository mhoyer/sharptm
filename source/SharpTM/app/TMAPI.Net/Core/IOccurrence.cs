namespace TMAPI.Net.Core
{
    /// <summary>
    /// Represents an 
    /// <a href="http://www.isotopicmaps.org/sam/sam-model/#sect-occurrence">occurrence item</a>.
    /// </summary>
    public interface IOccurrence : IDatatypeAware, ITyped
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="T:TMAPI.Net.Core.ITopic"/> to which this occurrence belongs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:TMAPI.Net.Core.ITopic"/> to which this occurrence belongs.
        /// </returns>
        new ITopic Parent
        {
            get;
        }

        #endregion
    }
}