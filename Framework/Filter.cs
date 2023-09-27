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
        /// Output Width
        /// </summary>
        protected int Width { get; private set; }

        /// <summary>
        /// Output Height
        /// </summary>
        protected int Height { get; private set; }

        /// <summary>
        /// Set size of the output
        /// </summary>
        protected void SetOutputDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Execute a single conversion on a specific point using the input. 
        /// </summary>
        /// <param name="u">Horizontal index</param>
        /// <param name="v">Vertical index</param>
        /// <param name="input">Single-channel image</param>
        /// <returns>Resulting value</returns>
        protected abstract byte ConvertPixel(int u, int v, byte[,] input);

        /// <summary>
        /// Useful for pre-computation of values needed for every pixel
        /// </summary>
        protected virtual Task BeforeConvert(byte[,] input)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Convert an image to the filtered image
        /// </summary>
        /// <param name="input">Single-channel image</param>
        /// <param name="progress">Progress tuple (Identifier, Percentage)</param>
        /// <returns>Filtered single-channel image</returns>
        public async Task<byte[,]> ConvertParallel(byte[,] input, IProgress<(string, int)> progress)
        {
            // Run the convert on another thread
            return await Task.Run(async () =>
            {
                Width = input.GetLength(0);
                Height = input.GetLength(1);

                await BeforeConvert(input);

                // Origin of small object heap size but is allowed
                var output = new byte[Width, Height];
                var current = 0;

                progress.Report((Identifier, Percentage(current)));

                Parallel.For(0, output.Length, i =>
                {
                    var u = i % Width;
                    var v = i / Width;
                    current++;

                    output[u, v] = ConvertPixel(u, v, input);

                    if (Percentage(current) > Percentage(current - 1))
                        progress.Report((Identifier, Percentage(current)));
                });

                return output;

                int Percentage(int value)
                {
                    return value * 100 / output.Length;
                }
            });
        }
    }
}