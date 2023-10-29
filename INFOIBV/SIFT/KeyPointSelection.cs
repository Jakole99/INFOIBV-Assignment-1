using MathNet.Numerics.LinearAlgebra;

namespace INFOIBV.SIFT;

public static class KeyPointSelection
{
    public readonly struct KeyPoint
    {
        public int Octave { get; }

        public int ScaleStep { get; }

        public int U { get; }

        public int V { get; }

        public KeyPoint(int p, int q, int u, int v)
        {
            Octave = p;
            ScaleStep = q;
            U = u;
            V = v;
        }

        public static KeyPoint operator +(KeyPoint a, KeyPoint b)
        {
            var (p0, q0, u0, v0) = a;
            var (p1, q1, u1, v1) = b;
            return new(p0+p1, q0+q1, u0+u1, v0+v1);
        }

        public void Deconstruct(out int octave, out int scaleStep, out int u, out int v)
        {
            octave = Octave;
            scaleStep = ScaleStep;
            u = U;
            v = V;
        }
    }

    #region Algorithm 7.3
    public static List<KeyPoint> GetSiftFeatures(SiftScaleSpace.Parameters parameters)
    {
        var scaleSpace = SiftScaleSpace.Build(parameters);

        var keyPoints = GetKeyPoints(scaleSpace.DifferenceOfGaussiansOctaves, parameters.OctaveCount,
            parameters.ScaleSteps);

        return keyPoints;
    }

    private static List<KeyPoint> GetKeyPoints(Image[][] dogSpace, int octaveCount, int scaleSteps)
    {
        var keyPoints = new List<KeyPoint>();

        for (var p = 1; p < octaveCount; p++)
        {
            for (var q = 1; q < scaleSteps; q++)
            {
                var extrema = FindExtrema(dogSpace, p, q);

                foreach (var extreme in extrema)
                {
                    var kPrime = RefineKeyPosition(dogSpace, extreme, scaleSteps);
                    if (kPrime.HasValue)
                        keyPoints.Add(kPrime.Value);
                }
            }
        }

        return keyPoints;
    }

    private static List<KeyPoint> FindExtrema(Image[][] dogSpace, int p, int q)
    {
        const double tMag = 0.01;
        var layer = dogSpace[p][q].Bytes;
        var m = layer.GetLength(0);
        var n = layer.GetLength(1);

        var extrema = new List<KeyPoint>();

        for (var u = 1; u < m-1; u++)
        {
            for (var v = 1; v < n-1; v++)
            {
                if (!(layer[u, v] > tMag))
                    continue;

                var k = new KeyPoint(p, q, u, v);
                var neighborHood = GetNeighborHood(dogSpace, k);

                if (IsExtremum(neighborHood))
                    extrema.Add(k);
            }
        }

        return extrema;
    }

    #endregion

    #region Algorithm 7.4

    private static KeyPoint? RefineKeyPosition(Image[][] dogSpace, KeyPoint k, int scaleSteps)
    {
        const double reMax = 10.0;
        const int nRefine = 5;
        const double tPeak = 0.01;


        var maxAlpha = Math.Pow(reMax + 1, 2) / reMax;
        KeyPoint? kPrime = null;
        var n = 1;
        var done = false;

        while (!done && n <= nRefine && IsInside(dogSpace, k, scaleSteps))
        {
            var neighborHood = GetNeighborHood(dogSpace, k);
            var gradient = Gradient(neighborHood);
            var hessianMatrix = Hessian(neighborHood);

            if (hessianMatrix.Determinant() == 0)
            {
                 done = true;
            }
            else
            {
                var d = -hessianMatrix.Inverse() * gradient;
                var dx = d[0];
                var dy = d[1];

                if (Math.Abs(dx) < 0.5 || Math.Abs(dy) < 0.5)
                {
                    done = true;
                    var peakD = neighborHood[1, 1, 1] + 0.5 * gradient * d;
                    var hessianMatrix2D = hessianMatrix.SubMatrix(0, 1, 0, 1);

                    if (Math.Abs(peakD) > tPeak && hessianMatrix2D.Determinant() > 0)
                    {
                        var alpha = Math.Pow(hessianMatrix2D.Trace(), 2) / hessianMatrix2D.Determinant();
                        if (alpha <= maxAlpha)
                            kPrime = k + new KeyPoint(0, 0, (int)Math.Round(dx), (int)Math.Round(dy));
                    }
                }
                else
                {
                    var uPrime = (int)Math.Min(1, Math.Max(-1, Math.Round(dx)));
                    var vPrime = (int)Math.Min(1, Math.Max(-1, Math.Round(dy)));
                    k += new KeyPoint(0, 0, uPrime, vPrime);
                }
            }

            n++;
        }

        return kPrime;
    }

    #endregion

    #region Algorithm 7.5
    private static bool IsInside(Image[][] dogSpace, KeyPoint k, int scaleSteps)
    {
        var (p, q, u, v) = k;

        if (q < 1 || q > scaleSteps + 1)
            return false;

        var layer = dogSpace[p][q].Bytes;
        var m = layer.GetLength(0);
        var n = layer.GetLength(1);

        return 0 < u && u < m - 1 && 0 < v && v < n - 1;
    }

    private static byte[,,] GetNeighborHood(Image[][] dogSpace, KeyPoint keyPoint)
    {
        var (p, q, u, v) = keyPoint;
        var neighborhood = new byte[3, 3, 3];

        for (var i = -1; i < 2; i++)
        {
            for (var j = -1; j < 2; j++)
            {
                for (var k = -1; k < 2; k++)
                {
                    var du = u + i;
                    var dv = v + j;
                    var dq = q + k;

                    var layer = dogSpace[p][dq].Bytes;

                    neighborhood[i + 1, j + 1, k + 1] = layer[du, dv];
                }
            }
        }

        return neighborhood;
    }

    private static bool IsExtremum(byte[,,] neighborHood)
    {
        var c = neighborHood[1, 1, 1];

        var isMin = true;
        var isMax = true;

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                for (var k = 0; k < 3; k++)
                {
                    if ((i, j, k) == (1, 1, 1))
                        continue;

                    isMin = neighborHood[i, j, k] > c;
                    isMax = neighborHood[i, j, k] < c;
                }
            }
        }

        return isMin || isMax;
    }

    private static Vector<double> Gradient(byte[,,] n)
    {
        var dx = 0.5 * (n[2, 1, 1] - n[0, 1, 1]);
        var dy = 0.5 * (n[1, 2, 1] - n[1, 0, 1]);
        var dz = 0.5 * (n[1, 1, 2] - n[1, 1, 0]);
        return Vector<double>.Build.Dense(new[] { dx, dy, dz });
    }

    private static Matrix<double> Hessian(byte[,,] n)
    {
        var dxx = n[0, 1, 1] - 2 * n[1, 1, 1] + n[2, 1, 1];
        var dyy = n[1, 0, 1] - 2 * n[1, 1, 1] + n[1, 2, 1];
        var dss = n[1, 1, 0] - 2 * n[1, 1, 1] + n[1, 1, 2];
        var dxy = (n[2, 2, 1] - n[0, 2, 1] - n[2, 0, 1] + n[0, 0, 1]) / 4;
        var dxs = (n[2, 1, 2] - n[0, 1, 2] - n[2, 1, 0] + n[0, 1, 0]) / 4;
        var dys = (n[1, 2, 2] - n[1, 0, 2] - n[1, 2, 0] + n[1, 0, 0]) / 4;

        return Matrix<double>.Build.DenseOfArray(new double[,]
        {
            { dxx, dxy, dxs },
            { dxy, dyy, dys },
            { dxs, dys, dss }
        });
    }
    #endregion

    #region Testing

    public static Bitmap DrawKeypoint(byte[,] input)
    {
        var width = input.GetLength(0);
        var height = input.GetLength(1);

        var keyPoints = GetSiftFeatures(new()
        {
            Input = new(input)
        });

        var output = new Bitmap(width, height);
        var newColor = Color.FromArgb(255, 0, 160);

        foreach (var keypoint in keyPoints)
        {
            if (keypoint.U < 0 || keypoint.U >= width)
                continue;

            if (keypoint.V < 0 || keypoint.V >= height)
                continue;

            output.SetPixel(keypoint.U, keypoint.V, newColor);
        }

        return output;
    }

    #endregion
}
