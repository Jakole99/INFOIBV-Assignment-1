using System;
using System.Drawing;
using INFOIBV.Extensions;
using INFOIBV.Filters;

namespace INFOIBV
{
    public class FilterMethods
    {
        /// <summary>
        /// Applies a specified filter
        /// </summary>
        /// <param name="filter">Type of filter</param>
        /// <param name="image">Three-channel image</param>
        /// <returns>One-channel image</returns>
        public static Bitmap ApplyFilter(FilterType filter, Bitmap image)
        {
            var singleChannel = image.ToSingleChannel();

            switch (filter)
            {
                case FilterType.GrayScale:
                    return singleChannel.ToBitmap();

                case FilterType.Invert:
                    return new InversionFilter().Convert(singleChannel).ToBitmap();

                case FilterType.AdjustContrast:
                    return new ContrastAdjustmentFilter().Convert(singleChannel).ToBitmap();

                case FilterType.Gaussian:
                    return new GaussianFilter(5, 1).Convert(singleChannel).ToBitmap();

                case FilterType.Median:
                    return new MedianFilter(5).Convert(singleChannel).ToBitmap();

                case FilterType.EdgeMagnitude:
                    return new EdgeMagnitudeFilter().Convert(singleChannel).ToBitmap();

                case FilterType.Threshold:
                    return new ThresholdFilter(128).Convert(singleChannel).ToBitmap();

                case FilterType.None:
                default:
                    throw new NotImplementedException();
            }
        }

        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 1 GO HERE ==============
        // ====================================================================


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 2 GO HERE ==============
        // ====================================================================


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 3 GO HERE ==============
        // ====================================================================
    }
}