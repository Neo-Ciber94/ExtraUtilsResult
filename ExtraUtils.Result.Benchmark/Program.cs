using System;
using BenchmarkDotNet.Running;
using ExtraUtils;

namespace ExtraUtils.Benchmark
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<ResultVsExceptionBenchmark>();
        }
    }
}
