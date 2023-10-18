namespace Framework;

public static class ThrowHelper
{
    public static void ArgumentIsEven(int argument, string name)
    {
        if (argument % 2 == 0)
            throw new ArgumentException($"{name} is not an odd size");
    }
}