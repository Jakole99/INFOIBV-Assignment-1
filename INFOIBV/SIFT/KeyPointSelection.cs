﻿using INFOIBV.Filters;
using INFOIBV.Framework;
using MathNet.Numerics.LinearAlgebra;

namespace INFOIBV.SIFT;

public static class KeyPointSelection
{
    #region SIFT Constants

    // ReSharper disable InconsistentNaming
    // ReSharper disable IdentifierTypo

    // Scale space parameters
    private const int Q = 3;
    private const int P = 4;
    private const double sigma_s = 0.5;
    private const double sigma_0 = 1.6;
    private const double t_Extrm = 0.0;

    // Key point detection
    private const int n_Orient = 36;
    private const int n_Refine = 5;
    private const int n_Smooth = 2;
    private const double reMax = 10.0;
    private const double t_DomOr = 0.8;
    private const double t_Mag = 18.0; //0.01 <- Lower is infeasible
    private const double t_Peak = 11.0; //0.01 <- Lower is infeasible

    // Feature descriptor
    private const int n_Spat = 4;
    private const int n_Angl = 16;
    private const double s_Desc = 10.0;
    private const double s_Fscale = 512.0;
    private const double t_Fclip = 0.2;

    // Feature matching
    private const double rmMax = 0.8;

    // ReSharper restore IdentifierTypo
    // ReSharper restore InconsistentNaming

    #endregion

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
            return new(p0 + p1, q0 + q1, u0 + u1, v0 + v1);
        }

        public void Deconstruct(out int octave, out int scaleStep, out int u, out int v)
        {
            octave = Octave;
            scaleStep = ScaleStep;
            u = U;
            v = V;
        }
    }

    private readonly struct KeyDescriptor
    {
        public int X { get; }

        public int Y { get; }

        public double Sigma { get; }

        public double Theta { get; }

        public byte[] FeatureVector { get; }

        public KeyDescriptor(int x, int y, double sigma, double theta, byte[] featureVector)
        {
            X = x;
            Y = y;
            Sigma = sigma;
            Theta = theta;
            FeatureVector = featureVector;
        }

        public void Deconstruct(out int x, out int y, out double sigma, out double theta, out byte[] featureVector)
        {
            x = X;
            y = Y;
            sigma = Sigma;
            theta = Theta;
            featureVector = FeatureVector;
        }
    }

    private readonly struct DescriptorMatch
    {
        public KeyDescriptor S1 { get; }

        public KeyDescriptor S2 { get; }

        public double Distance { get; }

        public DescriptorMatch(KeyDescriptor s1, KeyDescriptor s2, double dist)
        {
            S1 = s1;
            S2 = s2;
            Distance = dist;
        }

        public void Deconstruct(out KeyDescriptor s1, out KeyDescriptor s2)
        {
            s1 = S1;
            s2 = S2;
        }
    }

    #region Algorithm 7.2

    public static ScaleSpace BuildSiftScaleSpace(Image input, bool visualize)
    {
        var gaussianOctaves = new Image[P][];
        var dogOctaves = new SImage[P][];

        var initialAbsScale = sigma_0 * Math.Pow(2, (float)-1 / Q);
        var initialRelScale = Math.Sqrt(initialAbsScale * initialAbsScale - sigma_s * sigma_s);
        var initialGaussian = ApplyGaussian(input, initialRelScale);

        gaussianOctaves[0] = MakeGaussianOctave(initialGaussian, Q, sigma_0);

        for (var p = 1; p < P; p++)
        {
            var newFirstGaussian = Decimate(gaussianOctaves[p - 1][Q]);
            gaussianOctaves[p] = MakeGaussianOctave(newFirstGaussian, Q, sigma_0);
        }

        for (var p = 0; p < P; p++)
        {
            dogOctaves[p] = MakeDogOctave(gaussianOctaves[p], Q);
        }

        return new()
        {
            GaussianOctaves = gaussianOctaves,
            DifferenceOfGaussiansOctaves = dogOctaves,
            DifferenceOfGaussiansOctavesByte = visualize ? MakeDogImages(dogOctaves) : null,
        };
    }

    public readonly struct ScaleSpace
    {
        public Image[][] GaussianOctaves { get; init; }

        public SImage[][] DifferenceOfGaussiansOctaves { get; init; }

        public Image[][]? DifferenceOfGaussiansOctavesByte { get; init; }
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

    private static SImage[] MakeDogOctave(Image[] gaussians, int scaleSteps)
    {
        var dogs = new SImage[scaleSteps + 2];

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
        var filter = new FilterCollection().AddGaussian(9, (float)width);
        return new(filter.Process(input.Bytes));
    }

    #endregion

    #region Algorithm 7.3

    private static List<KeyDescriptor> GetSiftFeatures(Image input)
    {
        var scaleSpace = BuildSiftScaleSpace(input, false);

        var keyPoints = GetKeyPoints(scaleSpace.DifferenceOfGaussiansOctaves);

        var descriptors = new List<KeyDescriptor>();

        foreach (var keyPoint in keyPoints)
        {
            var dominantOrientations = GetDominantOrientations(scaleSpace.GaussianOctaves, keyPoint);

            foreach (var dominantOrientation in dominantOrientations)
            {
                var descriptor = MakeSiftDescriptor(scaleSpace.GaussianOctaves, keyPoint, dominantOrientation);
                descriptors.Add(descriptor);
            }
        }

        return descriptors;
    }

    private static List<KeyPoint> GetKeyPoints(SImage[][] dogSpace)
    {
        var keyPoints = new List<KeyPoint>();

        for (var p = 0; p < P; p++) ////////
        {
            for (var q = 1; q < Q + 1; q++) //////////
            {
                var extrema = FindExtrema(dogSpace, p, q);

                foreach (var extreme in extrema)
                {
                    var kPrime = RefineKeyPosition(dogSpace, extreme);
                    if (kPrime.HasValue)
                        keyPoints.Add(kPrime.Value);
                }
            }
        }

        return keyPoints;
    }

    private static List<KeyPoint> FindExtrema(SImage[][] dogSpace, int p, int q)
    {
        var layer = dogSpace[p][q].Data;
        var m = layer.GetLength(0);
        var n = layer.GetLength(1);

        var extrema = new List<KeyPoint>();

        for (var u = 1; u < m - 1; u++)
        {
            for (var v = 1; v < n - 1; v++)
            {
                if (!(Math.Abs(layer[u, v]) > t_Mag))
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

    private static KeyPoint? RefineKeyPosition(SImage[][] dogSpace, KeyPoint k)
    {
        var maxAlpha = Math.Pow(reMax + 1, 2) / reMax;
        KeyPoint? kPrime = null;
        var n = 1;
        var done = false;

        while (!done && n <= n_Refine && IsInside(dogSpace, k))
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
                    var hessianMatrix2D = hessianMatrix.SubMatrix(0, 2, 0, 2);


                    if (Math.Abs(peakD) > t_Peak && hessianMatrix2D.Determinant() > 0)
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

    private static bool IsInside(SImage[][] dogSpace, KeyPoint k)
    {
        var (p, q, u, v) = k;

        if (q is < 1 or > Q + 1)
            return false;

        var layer = dogSpace[p][q].Data;
        var m = layer.GetLength(0);
        var n = layer.GetLength(1);

        return 0 < u && u < m - 1 && 0 < v && v < n - 1;
    }

    private static int[,,] GetNeighborHood(SImage[][] dogSpace, KeyPoint keyPoint)
    {
        var (p, q, u, v) = keyPoint;
        var neighborhood = new int[3, 3, 3];

        for (var i = -1; i < 2; i++)
        {
            for (var j = -1; j < 2; j++)
            {
                for (var k = -1; k < 2; k++)
                {
                    var du = u + i;
                    var dv = v + j;
                    var dq = q + k;

                    var layer = dogSpace[p][dq].Data;

                    neighborhood[i + 1, j + 1, k + 1] = layer[du, dv];
                }
            }
        }

        return neighborhood;
    }

    private static bool IsExtremum(int[,,] neighborHood)
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

                    isMin = neighborHood[i, j, k] > c + t_Extrm;
                    isMax = neighborHood[i, j, k] < c - t_Extrm;
                }
            }
        }

        return isMin || isMax;
    }

    private static Vector<double> Gradient(int[,,] n)
    {
        var dx = 0.5 * (n[2, 1, 1] - n[0, 1, 1]);
        var dy = 0.5 * (n[1, 2, 1] - n[1, 0, 1]);
        var dz = 0.5 * (n[1, 1, 2] - n[1, 1, 0]);
        return Vector<double>.Build.Dense(new[] { dx, dy, dz });
    }

    private static Matrix<double> Hessian(int[,,] n)
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

    #region Algorithm 7.6

    private static List<double> GetDominantOrientations(Image[][] gaussians, KeyPoint kPrime)
    {
        var h = GetOrientationHistogram(gaussians, kPrime);
        SmoothCircular(h, n_Smooth);
        return FindPeakOrientations(h);
    }

    private static void SmoothCircular(double[] h, int iterations)
    {
        const double h0 = 0.25;
        const double h1 = 0.5;
        const double h2 = 0.25;
        var n = h.Length;

        for (var i = 1; i < iterations + 1; i++)
        {
            var s = h[0];
            var p = h[n - 1];

            for (var j = 0; j < n - 1; j++)
            {
                var c = h[j];
                h[j] = h0 * p + h1 * h[j] + h2 * h[j + 1];
                p = c;
            }

            h[n - 1] = h0 * p + h1 * h[n - 1] + h2 * s;
        }
    }

    private static List<double> FindPeakOrientations(double[] h)
    {
        var n = h.Length;
        var peakOrientations = new List<double>();
        var hMax = h.Max();

        for (var k = 1; k < n - 1; k++) ////
        {
            var hc = h[k];

            if (!(hc > t_DomOr * hMax))
                continue;

            var hp = h[(k - 1) % n]; ////
            var hn = h[(k + 1) % n]; ////

            if (!(hc > hp) || !(hc > hn))
                continue;

            var kPrime = k + (hp - hn) / (2 * (hp - 2 * hc + hn));
            var theta = kPrime * (2 * Math.PI / n) % (2 * Math.PI);
            peakOrientations.Add(theta);
        }

        return peakOrientations;
    }

    #endregion

    #region Algorithm 7.7

    private static double[] GetOrientationHistogram(Image[][] gaussians, KeyPoint kPrime)
    {
        var (p, q, x, y) = kPrime;

        var gaussian = gaussians[p][q].Bytes;
        var m = gaussian.GetLength(0);
        var n = gaussian.GetLength(1);

        var h = new double[n_Orient + 2]; /////

        var sw = 1.5 * sigma_0 * Math.Pow(2, (double)q / Q);
        var rw = Math.Max(1, 2.5 * sw);
        var uMin = (int)Math.Max(Math.Floor(x - rw), 1);
        var uMax = (int)Math.Min(Math.Ceiling(x + rw), m - 2);
        var vMin = (int)Math.Max(Math.Floor(y - rw), 1);
        var vMax = (int)Math.Min(Math.Ceiling(y + rw), n - 2);

        for (var u = uMin; u <= uMax; u++)
        {
            for (var v = vMin; v <= vMax; v++)
            {
                var r2 = Math.Pow(u - x, 2) + Math.Pow(v - y, 2);
                if (r2 >= rw * rw)
                    continue;

                var (r, phi) = GetGradientPolar(gaussian, u, v); ////

                var wg = Math.Exp(-(Math.Pow(u - x, 2) + Math.Pow(v - y, 2)) / (2 * sw * sw));
                var z = r * wg;
                var kPhi = n_Orient * (phi / (2 * Math.PI));
                var alpha = kPhi - Math.Floor(kPhi);

                var k0 = (int)Math.Floor(kPhi) % n_Orient;
                var k0Indexed = k0 + n_Orient / 2; ////

                var k1 = (k0 + 1) % n_Orient;
                var k1Indexed = k1 + n_Orient / 2; ////

                h[k0Indexed] += (1 - alpha) * z;
                h[k1Indexed] += alpha * z;
            }
        }

        return h;
    }

    private static (double, double) GetGradientPolar(byte[,] gaussian, int u, int v)
    {
        var dx = 0.5 * (gaussian[u + 1, v] - gaussian[u - 1, v]);
        var dy = 0.5 * (gaussian[u, v + 1] - gaussian[u, v - 1]);

        var r = Math.Sqrt(dx * dx + dy * dy);
        var phi = Math.Atan2(dy, dx);

        if (phi < -Math.PI) //////
            phi += 2 * Math.PI;
        if (phi > Math.PI)
            phi -= 2 * Math.PI;

        return (r, phi);
    }

    #endregion

    #region Algorithm 7.8

    private static KeyDescriptor MakeSiftDescriptor(Image[][] gaussians, KeyPoint kPrime, double theta)
    {
        var (p, q, x, y) = kPrime;

        var gaussian = gaussians[p][q];
        var m = gaussian.Bytes.GetLength(0);
        var n = gaussian.Bytes.GetLength(1);

        var sq = sigma_0 * Math.Pow(2, (float)q / Q);
        var wd = s_Desc * sq;
        var sd = 0.25 * wd;
        var rd = 2.5 * sd;

        var uMin = Math.Max((int)Math.Floor(x - rd), 1);
        var uMax = Math.Min((int)Math.Ceiling(x + rd), m - 2);
        var vMin = Math.Max((int)Math.Floor(y - rd), 1);
        var vMax = Math.Min((int)Math.Ceiling(y + rd), n - 2);

        var h = new double[n_Spat, n_Spat, n_Angl];

        for (var u = uMin; u <= uMax; u++)
        {
            for (var v = vMin; v <= vMax; v++)
            {
                var r2 = Math.Pow(u - x, 2) + Math.Pow(v - y, 2);
                if (r2 >= rd * rd)
                    continue;

                var transformMatrix = Matrix<double>.Build.DenseOfArray(new[,]
                {
                    { Math.Cos(-theta), -Math.Sin(-theta) },
                    { Math.Sin(-theta), Math.Cos(-theta) }
                });

                var coords = Matrix<double>.Build.DenseOfArray(new double[,]
                {
                    { u - x },
                    { v - y }
                });

                var canonCoords = 1 / wd * transformMatrix * coords;
                var uPrime = Math.Clamp(canonCoords[0, 0], -0.5, 0.5);
                var vPrime = Math.Clamp(canonCoords[1, 0], -0.5, 0.5);

                var (r, phi) = GetGradientPolar(gaussian.Bytes, u, v);
                var phiPrime = (phi - theta) % (2 * Math.PI);
                if (phiPrime < 0) //////
                {
                    phiPrime += 2 * Math.PI;
                }

                var wg = Math.Exp(-r2 / (2 * sd * sd));
                var z = r * wg;
                UpdateGradientHistogram(h, uPrime, vPrime, phiPrime, z);
            }
        }

        var fSift = MakeSiftFeatureVector(h);
        var sigma = sigma_0 * Math.Pow(2, p + (double)q / Q);
        var twoP = (int)Math.Pow(2, p);

        return new(twoP * x, twoP * y, sigma, theta, fSift);
    }

    #endregion

    #region Algorithm 7.9

    private static void UpdateGradientHistogram(double[,,] h, double uPrime, double vPrime, double phiPrime, double z)
    {
        var iPrime = n_Spat * uPrime + 0.5 * (n_Spat - 1);
        var jPrime = n_Spat * vPrime + 0.5 * (n_Spat - 1);
        var kPrime = n_Angl * (phiPrime / (2 * Math.PI));

        var i = new int[2];
        i[0] = (int)Math.Floor(iPrime);
        i[1] = i[0] + 1;

        var j = new int[2];
        j[0] = (int)Math.Floor(jPrime);
        j[1] = j[0] + 1;

        var k = new int[2];
        k[0] = (int)(Math.Floor(kPrime) % n_Angl);
        k[1] = (k[0] + 1) % n_Angl;

        var alpha = new double[2];
        alpha[0] = i[1] - iPrime;
        alpha[1] = 1 - alpha[0];

        var beta = new double[2];
        beta[0] = j[1] - jPrime;
        beta[1] = 1 - beta[0];

        var gamma = new double[2];
        gamma[0] = Math.Floor(kPrime) + 1 - iPrime;
        gamma[1] = 1 - gamma[0];

        for (var a = 0; a < 2; a++)
        {
            if (i[a] < 0 || i[a] >= n_Spat)
                continue;

            for (var b = 0; b < 2; b++)
            {
                if (j[b] < 0 || j[b] >= n_Spat)
                    continue;

                for (var c = 0; c < 2; c++)
                {
                    h[i[a], j[b], k[c]] += z * alpha[a] * beta[b] * gamma[c];
                }
            }
        }
    }

    #endregion

    #region Algorithm 7.10

    private static byte[] MakeSiftFeatureVector(double[,,] h)
    {
        var f = new double[n_Spat * n_Spat * n_Angl];
        var m = 0;

        for (var i = 0; i < n_Spat; i++)
        {
            for (var j = 0; j < n_Spat; j++)
            {
                for (var k = 0; k < n_Angl; k++)
                {
                    f[m] = h[i, j, k];
                    m++;
                }
            }
        }

        Normalize(f);
        ClipPeaks(f, t_Fclip);
        Normalize(f);
        var fSift = MapToBytes(f, s_Fscale);
        return fSift;
    }

    private static void Normalize(IList<double> x)
    {
        var n = x.Count;
        var s = x.Sum();
        for (var i = 0; i < n; i++)
        {
            x[i] /= s;
        }
    }

    private static void ClipPeaks(IList<double> x, double xMax)
    {
        var n = x.Count;
        for (var i = 0; i < n; i++)
        {
            x[i] = Math.Min(x[i], xMax);
        }
    }

    private static byte[] MapToBytes(IList<double> x, double s)
    {
        var n = x.Count;
        var bytes = new byte[n];
        for (var i = 0; i < n; i++)
        {
            var a = Math.Round(s * x[i]);
            bytes[i] = (byte)Math.Min(a, 255);
        }

        return bytes;
    }

    #endregion

    #region Algorithm 7.11

    private static List<DescriptorMatch> MatchDescriptors(List<KeyDescriptor> sA, List<KeyDescriptor> sB)
    {
        var matchList = new List<DescriptorMatch>();

        foreach (var si in sA)
        {
            KeyDescriptor s1 = default!;
            var dr1 = Double.PositiveInfinity;

            KeyDescriptor? s2 = null;
            var dr2 = Double.PositiveInfinity;

            foreach (var sj in sB)
            {
                var d = Dist(si, sj);
                if (d < dr1)
                {
                    s2 = s1;
                    dr2 = dr1;
                    s1 = sj;
                    dr1 = d;
                }
                else
                {
                    if (d < dr2)
                    {
                        s2 = sj;
                        dr2 = d;
                    }
                }
            }

            if (s2 != null && dr1 / dr2 <= rmMax)
            {
                matchList.Add(new(si, s1, dr1));
            }
        }

        var orderedMatches = matchList.OrderBy(x => x.Distance).ToList();
        return orderedMatches;
    }

    private static double Dist(KeyDescriptor sa, KeyDescriptor sb)
    {
        var (_, _, _, _, fa) = sa;
        var (_, _, _, _, fb) = sb;

        var n = fa.Length;
        var sum = 0.0;

        for (var i = 0; i < n; i++)
        {
            sum += Math.Pow(fa[i] - fb[i], 2);
        }

        return Math.Sqrt(sum);
    }

    #endregion


    #region Testing

    public static Bitmap DrawFeatures(byte[,] input)
    {
        var width = input.GetLength(0);
        var height = input.GetLength(1);

        var keyDescriptors = GetSiftFeatures(new(input));

        var output = input.ToBitmap();
        var newColor = Color.FromArgb(255, 0, 160);

        foreach (var keyDescriptor in keyDescriptors)
        {
            var (x, y, scale, theta, _) = keyDescriptor;

            if (x < 0 || x >= width)
                continue;

            if (y < 0 || y >= height)
                continue;

            DrawKeyDescriptor(output, x, y, theta, scale);
            output.SetPixel(x, y, newColor);
        }

        return output;
    }

    public static (Bitmap, Bitmap) DrawMatchFeatures(byte[,] inputReference, byte[,] input,
        byte[,] processedInputReference, byte[,] processedInput)
    {
        const int amount = 4; //The amount of matches we want to check

        var keyDescriptorsReference = GetSiftFeatures(new(processedInputReference));
        var keyDescriptors = GetSiftFeatures(new(processedInput));

        //Test the matching with the same image.
        var matches = MatchDescriptors(keyDescriptorsReference, keyDescriptors);

        var topMatches = GetTopMatches(matches, amount);

        var outputReference = inputReference.ToBitmap();
        var output = input.ToBitmap();

        //A different main color for each match, for testing purposes
        var colors = GetColors(amount);
        var pen = new Pen(Color.FromArgb(255, 255, 255), 4);
        const int size = 10;
        using var gReference = Graphics.FromImage(outputReference);
        using var g = Graphics.FromImage(output);

        if (topMatches.Count <= 0)
            return (outputReference, output);

        for (var i = 0; i < topMatches.Count; i++)
        {
            var (s1, s2) = topMatches[i];
            pen.Color = colors[i];

            //Draw in the reference
            var (x1, y1, _, _, _) = s1;
            gReference.DrawEllipse(pen, x1, y1, size, size);

            //draw in the output
            var (x2, y2, _, _, _) = s2;
            g.DrawEllipse(pen, x2, y2, size, size);
        }

        return (outputReference, output);
    }


    /// <summary>
    /// Draw the bounding box of the Uno card around the detected card
    /// </summary>
    /// <remarks>
    /// Does not give you the correct border due to some unforeseen problem
    /// </remarks>
    public static Bitmap DrawBoundingBox(byte[,] processedReferenceImage, byte[,] processedInput, byte[,] input)
    {
        var keyDescriptorsReference = GetSiftFeatures(new(processedReferenceImage));
        var keyDescriptors = GetSiftFeatures(new(processedInput));

        var matches = MatchDescriptors(keyDescriptorsReference, keyDescriptors);

        var topMatches = GetTopMatches(matches, 4);

        if (topMatches.Count < 4)
            return input.ToBitmap();


        var projectionMap = GetProjectionMap(topMatches);

        var width = processedReferenceImage.GetLength(0);
        var height = processedReferenceImage.GetLength(1);
        var corners = new (int x, int y)[]
        {
            (0, 0),
            (0, height),
            (width, height),
            (width, 0)
        };

        var output = input.ToBitmap();
        var red = new Pen(Color.FromArgb(255, 255, 0, 0), 3);
        using var graphics = Graphics.FromImage(output);

        for (var i = 1; i < 4; i++)
        {
            var (cx0, cy0) = corners[i - 1];
            var (cx1, cy1) = corners[i];
            var (x0, y0) = projectionMap.Project(cx0, cy0);
            var (x1, y1) = projectionMap.Project(cx1, cy1);
            graphics.DrawLine(red, x0, y0, x1, y1);
        }

        return output;
    }

    #endregion

    #region HelperFunctions


    /// <summary>
    /// Converts <see cref="SImage"/> DoG images to <see cref="Image"/>
    /// </summary>
    private static Image[][] MakeDogImages(IReadOnlyList<SImage[]> dogOctaves)
    {
        var dogOctavesImage = new Image[P][];

        for (var p = 0; p < P; p++)
        {
            dogOctavesImage[p] = MakeDogOctaveImage(dogOctaves[p], Q);
        }

        return dogOctavesImage;
    }

    /// <summary>
    /// Creates an array of images from <see cref="SImage"/>
    /// </summary>
    private static Image[] MakeDogOctaveImage(IReadOnlyList<SImage> dogs, int scaleSteps)
    {
        var dogsImages = new Image[scaleSteps + 2];

        for (var q = 1; q < scaleSteps + 1; q++)
        {
            dogsImages[q] = SImage.Visualize(dogs[q]);
        }

        return dogsImages;
    }

    /// <summary>
    /// Draws a key descriptor on an existing <see cref="Bitmap"/>
    /// </summary>
    private static void DrawKeyDescriptor(Bitmap bitmap, int x, int y, double theta, double scale)
    {
        // Draw line to screen.
        using var graphics = Graphics.FromImage(bitmap);

        var length = scale * 2;
        var xE = x + (int)(length * Math.Cos(theta));
        var yE = y + (int)(length * Math.Sin(theta));

        var penLine = new Pen(Color.FromArgb(255, 0, 200, 255), 1);
        var penCircle = new Pen(Color.FromArgb(255, 0, 200, 100), 1);
        graphics.DrawEllipse(penCircle, (float)(x - length), (float)(y - length), (float)(2 * length),
            (float)(2 * length));
        graphics.DrawLine(penLine, x, y, xE, yE);
    }

    /// <summary>
    /// Translate the top four <see cref="DescriptorMatch"/> to a <see cref="ProjectionMap"/>
    /// </summary>
    private static ProjectionMap GetProjectionMap(IReadOnlyList<DescriptorMatch> matches)
    {
        var ps = new (int x, int y)[4];
        var qs = new (int x, int y)[4];
        for (var i = 0; i < 4; i++)
        {
            ps[i] = (matches[i].S1.X, matches[i].S1.Y);
            qs[i] = (matches[i].S2.X, matches[i].S2.Y);
        }

        return new(ps[0], ps[1], ps[2], ps[3], qs[0], qs[1], qs[2], qs[3]);
    }

    /// <summary>
    /// Get the coordinate distinct matches and take the top amount
    /// </summary>
    private static List<DescriptorMatch> GetTopMatches(IEnumerable<DescriptorMatch> orderedMatches, int amount)
    {
        return orderedMatches
            .DistinctBy(x => (x.S1.X, x.S1.Y))
            .DistinctBy(x => (x.S2.X, x.S2.Y))
            .Take(amount).ToList();
    }

    /// <summary>
    /// Cycle through red, green and blue for an amount of times
    /// </summary>
    private static Color[] GetColors(int amount)
    {
        var colors = new Color[amount];
        for (var i = 0; i < amount; i++)
        {
            var red = i % 3 == 0 ? 255 : 0;
            var green = (i + 1) % 3 == 0 ? 255 : 0;
            var blue = (i + 2) % 3 == 0 ? 255 : 0;

            var newColor = Color.FromArgb(red, green, blue);
            colors[i] = newColor;
        }

        return colors;
    }

    #endregion
}