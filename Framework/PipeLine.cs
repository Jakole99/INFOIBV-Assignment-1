using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using INFOIBV.Extensions;

namespace INFOIBV.Framework
{
    /// <summary>
    /// Pipeline for bitmap conversion using filters
    /// </summary>
    public sealed class PipeLine
    {
        private readonly Queue<Filter> _filters = new Queue<Filter>();

        /// <summary>
        /// Queue a filter to the pipeline
        /// </summary>
        public PipeLine AddFilter(Filter filter)
        {
            _filters.Enqueue(filter);
            return this;
        }

        /// <summary>
        /// Convert an <see cref="Bitmap"/> to single-channel byte array and apply all the filters
        /// </summary>
        /// <remarks>Run asynchronously because we are doing cpu bound computation</remarks>
        public async Task<Bitmap> Build(Bitmap image, IProgress<(string, int)> progress)
        {
            var singleChannel = image.ToSingleChannel();

            // Apply filters
            while (_filters.Count > 0)
            {
                var filter = _filters.Dequeue();
                singleChannel = await filter.ConvertParallel(singleChannel, progress);
            }

            return singleChannel.ToBitmap();
        }
    }
}