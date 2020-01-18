using System;
using BenchmarkDotNet.Attributes;

namespace ExtraUtils
{
    [ShortRunJob]
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    public class ResultVsExceptionBenchmark
    {
        public enum Error { InvalidArgument }

        [Params(1, 2, 3, 4)]
        public int Value { get; set; }

        [Benchmark(Baseline = true)]
        public void Exception()
        {
            try
            {
                var result = GetOrException(Value);
            }
            catch (Exception)
            {

            }
        }

        [Benchmark]
        public void ResultOrException()
        {
            var result = GetResultOrException(Value);
        }

        [Benchmark]
        public void ResultOrError()
        {
            var result = GetResultOrError(Value);
        }

        private int GetOrException(int value)
        {
            if (value % 2 == 0)
            {
                return value * 2;
            }
            else
            {
                throw new Exception();
            }
        }

        private Result<int> GetResultOrException(int value)
        {
            if (value % 2 == 0)
            {
                return Result.Ok(value * 2);
            }
            else
            {
                return Result.Error<int>(new Exception());
            }
        }

        private Result<int, Error> GetResultOrError(int value)
        {
            if (value % 2 == 0)
            {
                return Result.Ok<int, Error>(value * 2);
            }
            else
            {
                return Result.Error<int, Error>(Error.InvalidArgument);
            }
        }
    }
}
