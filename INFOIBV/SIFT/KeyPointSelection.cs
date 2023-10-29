using Accessibility;
using MathNet.Numerics.LinearAlgebra;
using static INFOIBV.SIFT.SiftScaleSpace;

namespace INFOIBV.SIFT;

public static class KeyPointSelection
{
    #region Algorithm 7.3
    public static List<(int?, int?, int?, int?)> GetSiftFeatures(SiftScaleSpace.Parameters parameters)
    {
        var scaleSpace = SiftScaleSpace.Build(parameters);

        var keyPoints = GetKeyPoints(scaleSpace.DifferenceOfGaussiansOctaves, parameters.OctaveCount,
            parameters.ScaleSteps);

        return keyPoints;
    }

    private static List<(int?, int?, int?, int?)> GetKeyPoints(Image[][] dogSpace, int octaveCount, int scaleSteps)
    {
        List<(int?, int?, int?, int?)> keyPoints = new List<(int?, int?, int?, int?)>();

        for (var p = 0; p < octaveCount; p++)
        {
            for (var q = 0; q < scaleSteps; q++)
            {
                var extremas = FindExtrema(dogSpace, p, q);

                foreach (var extrema in extremas)
                {
                    var dk = RefineKeyPosition(dogSpace, extrema, scaleSteps);
                    if (dk != (null, null, null, null))
                        keyPoints.Add(dk);
                }
            }
        }

        return keyPoints;
    }

    private static List<(int, int, int, int)> FindExtrema(Image[][] dogSpace, int p, int q)
    {
        const double tMag = 0.01;
        var layer = dogSpace[p][q].Bytes;
        var m = layer.GetLength(0);
        var n = layer.GetLength(1);

        List<(int, int, int, int)> extremas = new List<(int, int, int, int)>();

        for (var u = 1; u < m-1; u++)
        {
            for (var v = 1; v < n-1; v++)
            {
                if (Math.Abs(layer[u, v]) > tMag)
                {
                    var k = (p, q, u, v);
                    var N = GetNeighborHood(dogSpace, p, 1, u, v);
                    
                    if (IsExtremum(N))
                        extremas.Add(k);
                }
            }
        }

        return extremas;
    }

    #endregion

    #region Algorithm 7.4

    private static (int?, int?, int?, int?) RefineKeyPosition(Image[][] dogSpace, (int, int, int, int) k, int scaleSteps)
    {
        const double reMax = 10.0;
        const int nRefine = 5;
        const double tPeak = 0.01;

        (int?, int?, int?, int?) dk = (null, null, null, null);

        var maxAlpha = Math.Pow((reMax + 1), 2) / reMax;
        var n = 1;
        var done = false;

        while (!done && n <= nRefine && IsInside(dogSpace, k, scaleSteps))
        {
            var (p, q, u, v) = k;
            var N = GetNeighborHood(dogSpace, p, q, u, v);
            var gradient = Gradient(N);
            var hessianMatrix = Hessian(N);

            if (hessianMatrix.Determinant() == 0)
                done = true;
            else
            {
                var d = -hessianMatrix.Inverse() * gradient;
                var dx = Math.Round(d[0]);
                var dy = Math.Round(d[1]);
                    
                if (dx < 0.5 || dy < 0.5)
                {
                    done = true;
                    var gradientT = TransposeVector(gradient);
                    var peakD = N[1, 1, 1] + 0.5 * gradientT * d;
                    var hessianMatrixXY = hessianMatrix.SubMatrix(0, 1, 0, 1);

                    if (Math.Abs(peakD) > tPeak && hessianMatrixXY.Determinant() > 0)
                    {
                        var dxx = hessianMatrixXY.At(0, 0);
                        var dyy = hessianMatrixXY.At(1, 1);

                        var alpha = (dxx + dyy * dxx + dyy) / hessianMatrixXY.Determinant();
                        if (alpha <= maxAlpha)
                            dk = (p, q, u + (int)dx, v + (int)dy);
                    }
                }
                else
                {
                    var du = Math.Min(1, Math.Max(-1, Math.Round(dx)));
                    var dv = Math.Min(1, Math.Max(-1, Math.Round(dy)));
                    k = (p, q, u + (int)du, v + (int)dv);
                }
            }

            n++;
        }

        return dk;
    }

    #endregion

    #region Algorithm 7.5
    private static bool IsInside(Image[][] dogSpace, (int, int, int, int) k, int scaleSteps)
    {
        var (p,q,u,v) = k;

        if (q < 1 || q > scaleSteps + 1)
            return false;

        var layer = dogSpace[p][q].Bytes;
        var m = layer.GetLength(0);
        var n = layer.GetLength(1);

        return 0 < u && u < m - 1 && 0 < v && v < n - 1;
    }

    private static byte[,,] GetNeighborHood(Image[][] dogSpace, int p, int q, int u, int v)
    {

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

    #region Helper Functions

    private static Vector<double> TransposeVector(Vector<double> vector)
    {
        var matrix = vector.ToColumnMatrix();
        var transposedMatrix = matrix.Transpose();
        var transposedVector = transposedMatrix.Column(0);

        return transposedVector;
    }
    #endregion

    #region Testing

    private static Bitmap DrawKeypoint(byte[,] input, List<(int?, int?, int?, int?)> keypoints)
    {
        var width = input.GetLength(0);
        var height = input.GetLength(1);

        var output = new Bitmap(width, height);
        var newColor = Color.FromArgb(255, 0, 160);

        foreach (var keypoint in keypoints)
        {
            //output.SetPixel(keypoint.Item1, keypoint.Item2, newColor);
        }

        throw new NotImplementedException();
    }

    #endregion
}
