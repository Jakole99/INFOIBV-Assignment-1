using INFOIBV.Filters;
using INFOIBV.Framework;

namespace INFOIBV.SIFT;

public static class SiftScaleSpace
{
    public readonly struct Image
    {
        public byte[,] Bytes { get; init; }

        public Image(byte[,] bytes)
        {
            Bytes = bytes;
        }

        public static Image operator -(Image a, Image b)
        {
            var width = a.Bytes.GetLength(0);
            var height = a.Bytes.GetLength(1);

            if (width != b.Bytes.GetLength(0) || height != b.Bytes.GetLength(1))
                throw new InvalidOperationException("Images are not the same size");

            var difference = new int[width, height];
            var min = Int32.MaxValue;
            var max = Int32.MinValue;

            for (var v = 0; v < height; v++)
            {
                for (var u = 0; u < width; u++)
                {
                    difference[u, v] = a.Bytes[u, v] - b.Bytes[u, v];
                    min = Math.Min(min, difference[u, v]);
                    max = Math.Max(max, difference[u, v]);
                }
            }

            var output = new byte[width, height];

            if (max - min == 0)
            {
                for (var v = 0; v < height; v++)
                {
                    for (var u = 0; u < width; u++)
                    {
                        output[u, v] = (byte)difference[u, v];
                    }
                }

                return new(output);
            }

            for (var v = 0; v < height; v++)
            {
                for (var u = 0; u < width; u++)
                {
                    output[u, v] = (byte)((difference[u, v] - min) * Byte.MaxValue / (max - min));
                }
            }

            return new(output);
        }
    }

    public readonly struct Parameters
    {
        public required Image Input { get; init; }

        public double SamplingScale { get; init; } = 0.5;

        public double ReferenceScale { get; init; } = 1.6;

        /// <remarks>
        /// P
        /// </remarks>
        public int OctaveCount { get; init; } = 4;

        /// <remarks>
        /// Q
        /// </remarks>
        public int ScaleSteps { get; init; } = 3;

        public Parameters()
        {
        }
    }

    public static Output Build(Parameters parameters)
    {
        var gaussianOctaves = new Image[parameters.OctaveCount][];
        var dogOctaves = new Image[parameters.OctaveCount][];

        var initialAbsScale = parameters.ReferenceScale * Math.Pow(2, (float)-1 / parameters.ScaleSteps);
        var initialRelScale = Math.Sqrt(initialAbsScale * initialAbsScale - parameters.SamplingScale * parameters.SamplingScale);
        var initialGaussian = ApplyGaussian(parameters.Input, initialRelScale);

        gaussianOctaves[0] = MakeGaussianOctave(initialGaussian, parameters.ScaleSteps, parameters.ReferenceScale);

        for (var p = 1; p < parameters.OctaveCount; p++)
        {
            var newFirstGaussian = Decimate(gaussianOctaves[p - 1][parameters.ScaleSteps]);
            gaussianOctaves[p] = MakeGaussianOctave(newFirstGaussian, parameters.ScaleSteps, parameters.ReferenceScale);
        }

        for (var p = 0; p < parameters.OctaveCount; p++)
        {
            dogOctaves[p] = MakeDogOctave(gaussianOctaves[p], parameters.ScaleSteps);
        }

        return new()
        {
            GaussianOctaves = gaussianOctaves,
            DifferenceOfGaussiansOctaves = dogOctaves
        };
    }

    public readonly struct Output
    {
        public Image[][] GaussianOctaves { get; init; }

        public Image[][] DifferenceOfGaussiansOctaves { get; init; }
    }

    private static Image[] MakeGaussianOctave(Image input, int scaleSteps, double referenceScale)
    {
        var gaussians = new Image[scaleSteps + 3];
        gaussians[0] = input;

        for (var q = 0; q < scaleSteps + 2; q++)
        {
            var relScale = referenceScale *
                           Math.Sqrt(Math.Pow(2, 2.0 * q / scaleSteps) - Math.Pow(2, -2.0 / scaleSteps));
            gaussians[q + 1] = ApplyGaussian(input, relScale);
        }

        return gaussians;
    }

    private static Image[] MakeDogOctave(Image[] gaussians, int scaleSteps)
    {
        var dogs = new Image[scaleSteps + 2];

        for (var q = -1; q < scaleSteps + 1; q++)
        {
            dogs[q + 1] = gaussians[q + 2] - gaussians[q + 1];
        }

        return dogs;
    }

    private static Image Decimate(Image input)
    {
        var width = input.Bytes.GetLength(0);
        var height = input.Bytes.GetLength(1);

        var newWidth = width / 2;
        var newHeight = height / 2;

        var output = new byte[newWidth, newHeight];

        for (var v = 0; v < newHeight; v++)
        {
            for (var u = 0; u < newWidth; u++)
            {
                output[u, v] = input.Bytes[2 * u, 2 * v];
            }
        }

        return new(output);
    }

    private static Image ApplyGaussian(Image input, double width)
    {
        // TODO: Variable kernel size formula?
        var filter = new FilterCollection().AddGaussian(9, (float)width);
        return new(filter.Process(input.Bytes));
    }
}