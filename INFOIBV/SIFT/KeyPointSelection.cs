using MathNet.Numerics.LinearAlgebra;

namespace INFOIBV.SIFT;

public static class KeyPointSelection
{
    public static void GetSiftFeatures(SiftScaleSpace.Parameters parameters)
    {
        var scaleSpace = SiftScaleSpace.Build(parameters);
    }

    private static void GetKeyPoints(Image[][] dogs, int octaveCount, int scaleSteps)
    {
        for (var p = 0; p < octaveCount; p++)
        {
            for (var q = 0; q < scaleSteps; q++)
            {
            }
        }
    }

    private static void FindExtrema(Image[] dogSpace, int p, int q)
    {

    }

    #region Algorithm 7.4

    private static Bitmap RefineKeyPosition(Image[][] dogSpace, int p, int q, int u, int v)
    {
        var maxAlpha = 0;
        // TODO: BRUH
        var n = 1;

        throw new NotImplementedException();
    }

    #endregion

    #region Algorithm 7.5
    private static bool IsInside(Image[][] dogSpace, int p, int q, int u, int v, int scaleSteps)
    {
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
}
