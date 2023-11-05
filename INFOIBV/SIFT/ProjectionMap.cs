using MathNet.Numerics.LinearAlgebra;

namespace INFOIBV.SIFT;

public class ProjectionMap
{
    private readonly Matrix<double> _matrix;

    public ProjectionMap(
        (int, int) a1, (int, int) a2, (int, int) a3, (int, int) a4,
        (int, int) b1, (int, int) b2, (int, int) b3, (int, int) b4)
    {
        var a = GetUnitProjectionMatrix(a1, a2, a3, a4);
        var b = GetUnitProjectionMatrix(b1, b2, b3, b4);

        _matrix = b * a.Inverse();
    }


    private static Matrix<double> GetUnitProjectionMatrix((int x, int y) p1, (int x, int y) p2, (int x, int y) p3,
        (int x, int y) p4)
    {
        var (x1, y1) = p1;
        var (x2, y2) = p2;
        var (x3, y3) = p3;
        var (x4, y4) = p4;

        // ReSharper disable InlineTemporaryVariable
        var s = (x2 - x3) * (y4 - y3) - (x4 - x3) * (y2 - y3);

        var a31Upper = (x1 - x2 + x3 - x4) * (y4 - y3) - (y1 - y2 + y3 - y4) * (x4 - x3);
        var a31 = (double)a31Upper / s;

        var a32Upper = (y1 - y2 + y3 - y4) * (x2 - x3) - (x1 - x2 + x3 - x4) * (y2 - y3);
        var a32 = (double)a32Upper / s;

        var a11 = x2 - x1 + a31 * x2;
        var a12 = x4 - x1 + a32 * x4;
        var a13 = x1;
        var a21 = y2 - y1 + a31 * y2;
        var a22 = y4 - y1 + a32 * y4;
        var a23 = y1;
        const int a33 = 1;

        return Matrix<double>.Build.DenseOfArray(new[,]
        {
            { a11, a12, a13 },
            { a21, a22, a23 },
            { a31, a32, a33 }
        });
        // ReSharper restore InlineTemporaryVariable
    }

    public (int x, int y) Project(int x, int y)
    {
        var vector = Vector<double>.Build.DenseOfArray(new double[] { x, y, 1 });

        var transformedVector = _matrix * vector;
        var homogenizedVector = 1 / transformedVector[2] * transformedVector;

        return ((int)Math.Round(homogenizedVector[0]), (int)Math.Round(homogenizedVector[1]));
    }
}