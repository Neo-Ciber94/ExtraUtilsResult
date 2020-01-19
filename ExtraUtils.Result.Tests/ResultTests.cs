using Xunit;
using ExtraUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraUtils.Tests
{
    public class ResultTests
    {
        [Fact()]
        public void GetErrorTest()
        {
            Result result = Result.Error(new Exception("Invalid date"));
            Assert.Equal("Invalid date", result.GetError().Message);
        }

        [Fact()]
        public void TryGetErrorTest()
        {
            Result result = Result.Error(new Exception("Invalid date"));
            Assert.True(result.TryGetError(out Exception e));
            Assert.Equal("Invalid date", e.Message);
            Assert.True(result.IsError);
        }

        [Fact()]
        public void ToStringTest()
        {
            Result result = Result.Error("Invalid data");
            Assert.Equal("Result(Invalid data)", result.ToString());

            result = Result.Ok(10);
            Assert.Equal("Result(Ok)", result.ToString());
        }

        // Extensions 

        [Fact()]
        public void ThrowIfErrorTest()
        {
            Result result = Result.Ok(10);
            Exception exception = Record.Exception(() => result.ThrowIfError());
            Assert.Null(exception);

            result = Result.Error(new Exception());
            exception = Record.Exception(() => result.ThrowIfError());
            Assert.NotNull(exception);
        }

        [Fact()]
        public void OnSuccessTest()
        {
            Result result = Result.Ok(10);
            int[] counter = new int[] { 0 };

            result.MatchOk(() =>
            {
                counter[0] = 1;
            });

            Assert.NotEqual(0, counter[0]);
            Assert.Equal(1, counter[0]);
        }

        [Fact()]
        public void OnErrorTest()
        {
            Result result = Result.Error("Invalid");
            int[] counter = new int[] { 0 };

            result.MatchError(e =>
            {
                counter[0] = 1;
                Assert.Equal("Invalid", e.Message);
            });

            Assert.NotEqual(0, counter[0]);
            Assert.Equal(1, counter[0]);
        }

        [Fact()]
        public void ThrowIfTypeTest()
        {
            Result result = Result.Error(new ArgumentException());
            Exception exception = Record.Exception(() => result.ThrowIfType(typeof(ArgumentException)));
            Assert.NotNull(exception);

            exception = Record.Exception(() => result.ThrowIfType(typeof(InvalidCastException)));
            Assert.Null(exception);
        }
    }
}