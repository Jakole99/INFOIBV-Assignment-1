namespace INFOIBV.Framework
{
    /// <summary>
    /// Abstract definition of a filter
    /// </summary>
    public abstract class Filter
    {
        /// <summary>
        /// Progress of the filter
        /// </summary>
        public IReadonlyFilterProgress Progress => _progress;

        private readonly FilterProgress _progress = new FilterProgress();

        /// <summary>
        /// User friendly name of the filter
        /// </summary>
        public abstract string Identifier { get; }

        /// <summary>
        /// Execute a single conversion on a specific point using the input. 
        /// </summary>
        /// <param name="u">Horizontal index</param>
        /// <param name="v">Vertical index</param>
        /// <param name="input">Single-channel image</param>
        /// <returns>Resulting value</returns>
        protected abstract byte ExecuteStep(int u, int v, byte[,] input);

        /// <summary>
        /// Useful for pre-computation of values needed for every pixel
        /// </summary>
        protected virtual void BeforeExecute(byte[,] input) { }

        /// <summary>
        /// Convert an image to the filtered image
        /// </summary>
        /// <param name="input">Single-channel image</param>
        /// <returns>Filtered single-channel image</returns>
        public byte[,] Convert(byte[,] input)
        {
            _progress.Init(input.Length);

            var output = new byte[input.GetLength(0), input.GetLength(1)];
            
            BeforeExecute(input);
            
            for (var u = 0; u < input.GetLength(0); u++)
            {
                for (var v = 0; v < input.GetLength(1); v++)
                {
                    output[u, v] = ExecuteStep(u, v, input);
                    _progress.Step();
                }
            }

            return output;
        }

    }
}