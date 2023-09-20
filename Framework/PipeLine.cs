using System;
using System.Collections.Generic;
using System.Drawing;
using INFOIBV.Extensions;

namespace INFOIBV.Framework
{
    /// <summary>
    /// Pipeline for bitmap conversion using filters
    /// </summary>
    public sealed class PipeLine
    {
        private readonly Queue<Func<byte[,], byte[,]>> _filters = new Queue<Func<byte[,], byte[,]>>();

        /// <summary>
        /// Queue a filter to the pipeline
        /// </summary>
        /// <returns></returns>
        public PipeLine AddFilter(Func<byte[,], byte[,]> filter)
        {
            _filters.Enqueue(filter);
            return this;
        }

        /// <summary>
        /// Grayscale a bitmap and apply all the filters then convert it back to a bitmap
        /// </summary>
        /// <returns>Filtered bitmap</returns>
        public Bitmap Build(Bitmap image)
        {
            var singleChannel = image.ToSingleChannel();

            // Apply filters
            while (_filters.Count > 0)
            {
                var filter =_filters.Dequeue();
                singleChannel = filter(singleChannel);
            }

            return singleChannel.ToBitmap();
        }
    }
}