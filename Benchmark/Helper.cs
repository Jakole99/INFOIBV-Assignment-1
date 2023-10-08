namespace Benchmark;

public static class Helper
{
    public static byte[] Create1dArray(int n)
    {
        var array = new byte[n * n];
        new Random(42).NextBytes(array);

        return array;
    }

    public static byte[,] ConvertTo2dArray(byte[] input)
    {
        var n = (int)Math.Sqrt(input.Length);
        var array = new byte[n, n];
        Buffer.BlockCopy(input, 0, array, 0, input.Length);

        return array;
    }
}