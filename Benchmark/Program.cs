﻿using BenchmarkDotNet.Running;

namespace Benchmark;

public static class Program
{
    public static void Main(string[] args)
        => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
}