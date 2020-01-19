using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Maps the result either if is a success or an error.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="ok">The maps the value.</param>
        /// <param name="error">The maps the errpr.</param>
        /// <returns>The result of the mapping.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Match<TResult>(this Result result, Func<TResult> ok, Func<Exception, TResult> error)
        {
            return result.IsSuccess ?
                ok() :
                error(result._error!);
        }

        /// <summary>
        /// Maps the result either if is a success or an error.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="ok">The maps the value.</param>
        /// <param name="error">The maps the errpr.</param>
        /// <returns>The result of the mapping.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Match<T, TResult>(this Result<T> result, Func<T, TResult> ok, Func<Exception, TResult> error)
        {
            return result.IsSuccess ?
                ok(result._value) :
                error(result._error!);
        }

        /// <summary>
        /// Maps the result either if is a success or an error.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="ok">The maps the value.</param>
        /// <param name="error">The maps the errpr.</param>
        /// <returns>The result of the mapping.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Match<T, TError, TResult>(this Result<T, TError> result, Func<T, TResult> ok, Func<TError, TResult> error)
        {
            return result.IsSuccess ?
                ok(result._value) :
                error(result._error);
        }
    }
}