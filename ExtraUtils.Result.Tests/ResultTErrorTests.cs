using System;
using Xunit;

namespace ExtraUtils.Tests
{
    public class ResultTErrorTests
    {
        [Fact()]
        public void GetErrorTest()
        {
            Result<int, Exception> result = Result.Error<int, Exception>(new Exception("Invalid date"));
            Assert.Equal("Invalid date", result.GetError().Message);
        }

        [Fact()]
        public void TryGetValueTest()
        {
            Result<int, Exception> result = Result.Ok<int, Exception>(10);
            Assert.True(result.TryGetValue(out int value));
            Assert.Equal(10, value);
            Assert.True(result.IsSuccess);

            Assert.Throws<InvalidOperationException>(() =>
            {
                var error = result.GetError();
            });
        }

        [Fact()]
        public void TryGetErrorTest()
        {
            Result<int, Exception> result = Result.Error<int, Exception>(new Exception("Invalid date"));
            Assert.True(result.TryGetError(out Exception e));
            Assert.Equal("Invalid date", e.Message);
            Assert.True(result.IsError);

            Assert.Throws<InvalidOperationException>(() =>
            {
                var value = result.Value;
            });
        }

        [Fact()]
        public void ContainsValueTest()
        {
            Result<int, Exception> result = Result.Ok<int, Exception>(10);
            Assert.True(result.ContainsValue(10));
            Assert.False(result.ContainsValue(1));
            Assert.False(result.ContainsValue(5));

            Result<int, Exception> result2 = Result.Error<int, Exception>(new Exception("Invalid date"));
            Assert.False(result2.ContainsValue(10));
        }

        [Fact()]
        public void ContainsErrorTest()
        {
            Result<int, string> result = Result.Error<int, string>("Invalid data");

            Assert.True(result.ContainsError("Invalid data"));
            Assert.False(result.ContainsError("Invalid"));
            Assert.False(result.ContainsValue(10));
        }

        [Fact()]
        public void ToStringTest()
        {
            Result<int, string> result = Result.Error<int, string>("Invalid data");
            Assert.Equal("Result(Invalid data)", result.ToString());

            result = Result.Ok<int, string>(10);
            Assert.Equal("Result(10)", result.ToString());
        }

        [Fact()]
        public void ResultToResultT1()
        {
            Result<int, Exception> result = Result.Ok<int, Exception>(10);
            Result<int> result2 = result;

            Assert.Equal(10, result2.Value);
        }

        [Fact()]
        public void ResultToResultT2()
        {
            Result<int, Exception> result = Result.Ok<int, Exception>(10);
            Result result2 = result;

            Assert.True(result2.IsSuccess);
        }

        [Fact()]
        public void ResultToResultT3()
        {
            Result<int, Exception> result = Result.Error<int, Exception>(new Exception("Invalid"));
            Result<int> result2 = result;

            Assert.Equal("Invalid", result2.GetError().Message);
        }

        [Fact()]
        public void ResultToResultT4()
        {
            Result<int, Exception> result = Result.Error<int, Exception>(new Exception("Invalid"));
            Result result2 = result;

            Assert.Equal("Invalid", result2.GetError().Message);
        }

        [Fact()]
        public void ResultToResultT5()
        {
            Result<int, string> result = Result.Error<int, string>("Invalid");
            Result<int> result2 = result;

            Assert.Equal("Invalid", result2.GetError().Message);
        }

        [Fact()]
        public void ResultToResultT6()
        {
            Result<int, string> result = Result.Error<int, string>("Invalid");
            Result result2 = result;

            Assert.Equal("Invalid", result2.GetError().Message);
        }

        // Extensions 

        [Fact()]
        public void ValueOrDefaultTest1()
        {
            Result<int, string> result = Result.Ok<int, string>(10);
            Assert.Equal(10, result.ValueOrDefault());

            result = Result.Error<int, string>("Error");
            Assert.Equal(default, result.ValueOrDefault());
        }

        [Fact()]
        public void ValueOrDefaultTest2()
        {
            Result<int, string> result = Result.Ok<int, string>(10);
            Assert.Equal(10, result.ValueOrDefault());

            result = Result.Error<int, string>("Error");
            Assert.Equal(22, result.ValueOrDefault(22));
        }

        [Fact()]
        public void ValueOrNullTest()
        {
            Result<int, string> result = Result.Ok<int, string>(10);
            Assert.Equal(10, result.ValueOrNull());

            result = Result.Error<int, string>("Error");
            Assert.Null(result.ValueOrNull());
        }

        [Fact()]
        public void ValueOrThrowTest()
        {
            Result<int, Exception> result = Result.Ok<int, Exception>(10);
            Assert.Equal(10, result.ValueOrThrow());

            result = Result.Error<int, Exception>(new Exception());
            Assert.Throws<Exception>(() =>
            {
                var error = result.ValueOrThrow();
            });
        }

        [Fact()]
        public void ThrowIfErrorTest()
        {
            Result<int, Exception> result = Result.Ok<int, Exception>(10);
            Exception exception = Record.Exception(() => result.ThrowIfError());
            Assert.Null(exception);

            result = Result.Error<int, Exception>(new Exception());
            exception = Record.Exception(() => result.ThrowIfError());
            Assert.NotNull(exception);
        }

        [Fact()]
        public void MapTest()
        {
            Result<int, string> result = Result.Ok<int, string>(10);
            Assert.Equal("10", result.Map(e => e.ToString()));

            Assert.Throws<InvalidOperationException>(() =>
            {
                var error = result.GetError();
            });

            result = Result.Error<int, string>("Error");
            Assert.Equal("Error", result.Map(e => e.ToString()).GetError());
        }

        [Fact()]
        public void MapErrorTest()
        {
            Result<int, string> result = Result.Ok<int, string>(10);
            Assert.Equal(10, result.MapError(e => e + "0").Value);

            result = Result.Error<int, string>("Error");
            Assert.Equal("Errors", result.MapError(e => e + "s").GetError());

            Assert.Throws<InvalidOperationException>(() =>
            {
                var value = result.Value;
            });
        }

        [Fact()]
        public void MathTest1()
        {
            Result<int, string> result = Result.Ok<int, string>(10);
            var value = result.Match(e => "Ok", e => "Fail");
            Assert.Equal("Ok", value);

            result = Result.Error<int, string>("Error");
            value = result.Match(
                ok: e => "Ok", 
                error: e => "Fail"
            );

            Assert.Equal("Fail", value);
        }


        [Fact()]
        public void MatchTest2()
        {
            Result<int, string> result = Result.Ok<int, string>(10);
            int[] counter = new int[] { 0 };

            result.Match(
                ok: e =>
                {
                    counter[0] = 1;
                    Assert.Equal(10, e);
                },
                error: e => { }
            );

            Assert.NotEqual(0, counter[0]);
            Assert.Equal(1, counter[0]);
        }

        [Fact()]
        public void OnSuccessTest()
        {
            Result<int, string> result = Result.Ok<int, string>(10);
            int[] counter = new int[] { 0 };

            result.OnSuccess(e => 
            { 
                counter[0] = 1;
                Assert.Equal(10, e);
            });

            Assert.NotEqual(0, counter[0]);
            Assert.Equal(1, counter[0]);
        }

        [Fact()]
        public void OnErrorTest()
        {
            Result<int, string> result = Result.Error<int, string>("Invalid");
            int[] counter = new int[] { 0 };

            result.OnError(e =>
            {
                counter[0] = 1;
                Assert.Equal("Invalid", e);
            });

            Assert.NotEqual(0, counter[0]);
            Assert.Equal(1, counter[0]);
        }
    }
}