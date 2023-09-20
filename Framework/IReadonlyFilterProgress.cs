namespace INFOIBV.Framework
{
    /// <summary>
    /// Progress of the filter
    /// </summary>
    public interface IReadonlyFilterProgress
    {
        /// <summary>
        /// Total number of steps for the filter
        /// </summary>
        int TotalSteps { get; }
    
        /// <summary>
        /// Current step in the filter
        /// </summary>
        int CurrentStep { get; }
    
        /// <summary>
        /// Percentage of completion
        /// </summary>
        int Percentage { get; }
    }
}