using System;
using System.Threading.Tasks;

namespace INFOIBV.Framework
{
    /// <summary>
    /// Abstract definition of a filter
    /// </summary>
    public abstract class Filter
    {
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

        /// <inheritdoc cref="BeforeExecute"/>
        protected virtual Task BeforeExecuteAsync(byte[,] input)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Convert an image to the filtered image
        /// </summary>
        /// <param name="input">Single-channel image</param>
        /// <returns>Filtered single-channel image</returns>
        [Obsolete("Use ConvertParallel instead")]
        public byte[,] Convert(byte[,] input)
        {
            var width = input.GetLength(0);
            var height = input.GetLength(1);

            var output = new byte[width, height];

            BeforeExecute(input);

            for (var u = 0; u < width; u++)
            {
                for (var v = 0; v < height; v++)
                {
                    output[u, v] = ExecuteStep(u, v, input);
                }
            }

            return output;
        }

        public async Task<byte[,]> ConvertParallel(byte[,] input, IProgress<(string, int)> progress)
        {
            // Run the convert on another thread
            return await Task.Run(async () =>
            {
                await BeforeExecuteAsync(input);

                var width = input.GetLength(0);
                var height = input.GetLength(1);
                var output = new byte[width, height];
                var current = 0;

                progress.Report((Identifier, Percentage(current)));

                Parallel.For(0, input.Length, i =>
                {
                    var u = i % width;
                    var v = i / width;
                    current++;

                    output[u, v] = ExecuteStep(u, v, input);

                    if (Percentage(current) > Percentage(current - 1))
                        progress.Report((Identifier, Percentage(current)));
                });

                return output;

                int Percentage(int value)
                {
                    return value * 100 / input.Length;
                }
            });
        }
    }
}