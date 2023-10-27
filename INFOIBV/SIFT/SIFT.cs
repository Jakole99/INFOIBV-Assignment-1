using INFOIBV.Filters;

namespace INFOIBV.Framework;

public class SIFT
{
    //Typical setting
    //omegaS, samplingScale = 0.5;
    //omega0, reference scale of the first octave = 1.6
    //P, number of octaves = 4
    //Q, number of scale steps = 3
    private int P;
    private double omega0;
    private int Q;

    public (List<byte[][,]>,List<byte[][,]>) BuildSiftScaleSpace(byte[,] input, double samplingScale, double referenceScale, int octavesNum,
        int scaleSteps)
    {
        P = octavesNum;
        omega0 = referenceScale;
        Q = octavesNum;
        //To determine local maxima along the scale dimension over
        //a full octave, two additional DoG levels, Dp,−1 and Dp,Q, and two additional
        //Gaussian scale levels, Gp,−1 and Gp, Q+1, are required in each octave

        //absolute scale value of m = -1, with m being the linear scale index.
        var omegaAbs = referenceScale * Math.Pow(2, (-1 / scaleSteps));

        //relative scale value of m = -1
        var omegaRel = Math.Sqrt(omegaAbs * omegaAbs - samplingScale * samplingScale);

        //apply Gaussian with the omegaRel as width
        var G = ApplyGaussian(input, omegaRel);

        var G0 = MakeGaussianOctave(G);

        List<byte[][,]> gaussianScaleSpace = new List<byte[][,]>();
        List<byte[][,]> DoGScaleSpace = new List<byte[][,]>();

        gaussianScaleSpace.Add(G0);

        for (var p = 1; p <= P - 1; p++)
        {
            var GLastList = gaussianScaleSpace.Last();
            var GLast = Decimate(GLastList.Last());
            var GLastScaled = MakeGaussianOctave(GLast);
            gaussianScaleSpace.Add(GLastScaled);
        }

        foreach (var octave in gaussianScaleSpace)
        {
            var D = MakeDogGOctave(octave);
            DoGScaleSpace.Add(D);
        }

        return (gaussianScaleSpace, DoGScaleSpace);

    }

    private byte[,] ApplyGaussian(byte[,] input, double width)
    {
        var filter = new FilterCollection().AddGaussian(9, (float)width);
        return filter.Process(input);
    }

    private byte[,] ApplyContrast(int[,] input)
    {
        var width = input.GetLength(0);
        var height = input.GetLength(1);

        var highest = input.Cast<int>().Max();
        var lowest = input.Cast<int>().Min();

        var output = new byte[width,height];
        for (var v = 0; v < input.GetLength(1); v++)
        {
            for (var u = 0; u < input.GetLength(0); u++)
            {   
                output[u, v] = (byte)(Byte.MinValue + (input[u, v] - lowest) * (Byte.MaxValue - Byte.MinValue) / (highest - lowest));
            }
        }

        return output;
    }

    private byte[][,] MakeGaussianOctave(byte[,] G)
    {
        var gaussianOctaves = new byte[Q + 2][,];
        for (var I = 0; I <= Q + 1; I++)
        {
            //decimated scales
            var omegaI = omega0 * Math.Sqrt(Math.Pow(2, (2 * (I+1) / (double)Q)) - 1);
            var Gpq = ApplyGaussian(G, omegaI);
            gaussianOctaves[I] = Gpq;
        }

        return gaussianOctaves;
    }

    private byte[,] Decimate(byte[,] Gin)
    {
        var M = Gin.GetLength(0);
        var N = Gin.GetLength(1);

        var M_ = M / 2;
        var N_ = N / 2;

        var Gout = new byte[M_, N_];

        for (var v = 0; v < Gout.GetLength(1); v++)
        {
            for (var u = 0; u < Gout.GetLength(0); u++)
            {
                Gout[u, v] = Gin[2 * u, 2 * v];
            }
        }

        return Gout;
    }

    private byte[][,] MakeDogGOctave(byte[][,] octave) 
    {
        var DogOctaves = new byte[Q + 1][,];
        for (var q = 0; q < Q + 1; q++)
        {
            var G = octave[q];
            var G1 = octave[q+1];
            var D = ImageDif(G1,G);
            DogOctaves[q] = D;

        }
        return DogOctaves;
    }

    private byte[,] ImageDif(byte[,] input1, byte[,] input2)
    {
        //Check if both images are same size
        if (input1.GetLength(0) != input2.GetLength(0) || input1.GetLength(1) != input2.GetLength(1))
            throw new Exception("Not the same sizes");

        var width = input1.GetLength(0);
        var height = input1.GetLength(1);

        var output = new int[width, height];

        for (var v = 0; v < height; v++)
        {
            for (var u = 0; u < width; u++)
            {
                var tmp = input1[u, v] - input2[u, v];
                output[u, v] = (sbyte)(tmp);
            }
        }

        //We need to contrastAdjust the inputs, otherwise we will get only very high or low values.
        return ApplyContrast(output);
    }

    private byte[,] AbsDoG(byte[,] input)
    {
        var filter = new FilterCollection().AddThresholdFilter(80);
        return filter.Process(input);
    }
}