using System;

namespace INFOIBV.Framework
{
    public class FilterProgress : IReadonlyFilterProgress
    {
        public int TotalSteps { get; private set; }
        public int CurrentStep { get; private set; }

        public int Percentage => (int)Math.Round((double)CurrentStep / TotalSteps * 100);

        /// <summary>
        /// Initialize progress for new operation
        /// </summary>
        /// <param name="total">Total number of steps</param>
        public void Init(int total)
        {
            TotalSteps = total;
            CurrentStep = 0;
        }
    
        /// <summary>
        /// Perform a filter step
        /// </summary>
        public void Step()
        {
            CurrentStep++;
        }
    }
}