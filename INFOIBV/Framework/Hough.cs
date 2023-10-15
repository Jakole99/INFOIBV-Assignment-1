namespace INFOIBV.Framework;

public readonly struct Hough
{
    private readonly int _thetaSteps;

    public List<Tuple<double, double>> TupleSetRandTheta { get; }
    public Hough(byte[,] input, int steps)
    {
        _thetaSteps = steps;
        TupleSetRandTheta = CreateHough(input, steps);
    }


    private static List<Tuple<double,double>> CreateHough(byte[,] input, int thetaSteps)
    {
        var tupleList = new List<Tuple<double, double>>();

        var thetaSet = CreateThetaSet(thetaSteps);

        var width = input.GetLength(0);
        var height = input.GetLength(1);


        for (var u = 0; u < width; u++)
        {
            for (var v = 0; v < height; v++)
            {
                if (input[u, v] == 0)
                    continue;


                foreach (var theta in thetaSet)
                {
                    var r = (u * Math.Cos(theta) + v * Math.Sin(theta));
                    var tuple = new Tuple<double, double>(r, theta);

                    tupleList.Add(tuple);
                }

            }
        }

        return tupleList;
    }

    public Bitmap GetBitmap()
    {
        //TA's recommend around 300 steps for theta and 400 for r.
        var maxR = TupleSetRandTheta.MaxBy(x => x.Item1).Item1;
        var minR = TupleSetRandTheta.MinBy(x => x.Item1).Item1;
        var rHeight = maxR + Math.Abs(minR);
        var thetaWidth = _thetaSteps;

        var width = _thetaSteps;
        var height = 800;

        var widthPerPixel = width / Math.PI;
        var heightPerPixel = height / rHeight;


        byte[,] output = new byte[width+1, height+1];

        foreach (var (u, v) in TupleSetRandTheta)
        {
            //r can be minus
            if (output[(int)(v*widthPerPixel), (int)((u + Math.Abs(minR))*heightPerPixel)] < 255)
                output[(int)(v*widthPerPixel), (int)((u + Math.Abs(minR))*heightPerPixel)] += 1;
        }

        return output.ToBitmap();

    }



    public static double[] CreateThetaSet(int steps)
    {
        double[] thetaSteps = new double[steps+1];
        var thetaMax = Math.PI;

        double step = thetaMax / steps;

        for (int i = 0; i <= steps; i++)
        {
            thetaSteps[i] = step * i;
        }

        return thetaSteps;
    }

}

