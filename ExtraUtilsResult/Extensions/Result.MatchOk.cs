using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Performs the given action if the result is a success.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="ok">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MatchOk(this Result result, Action ok)
        {
            if (result.IsSuccess)
            {
                ok();
            }
        }

        /// <summary>
        /// Performs the given action if the result is a success.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="ok">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MatchOk<T>(this Result<T> result, Action<T> ok)
        {
            if (result.IsSuccess)
            {
                ok(result._value);
            }
        }

        /// <summary>
        /// Performs the given action if the result is a success.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="ok">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MathOk<T, TError>(this Result<T, TError> result, Action<T> ok)
        {
            if (result.IsSuccess)
            {
                ok(result._value);
            }
        }
    }
}
