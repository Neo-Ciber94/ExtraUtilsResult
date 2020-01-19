using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Performs the given action if the result is an error.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="error">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MatchError(this Result result, Action<Exception> error)
        {
            if (result.IsError)
            {
                error(result._error!);
            }
        }

        /// <summary>
        /// Performs the given action if the result is an error.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="error">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MatchError<T>(this Result<T> result, Action<Exception> error)
        {
            if (result.IsError)
            {
                error(result._error!);
            }
        }

        /// <summary>
        /// Performs the given action if the result is an error.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="error">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MatchError<T, TError>(this Result<T, TError> result, Action<TError> error)
        {
            if (result.IsError)
            {
                error(result._error);
            }
        }
    }
}
