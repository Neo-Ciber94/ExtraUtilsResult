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
        /// <param name="onSuccess">The maps the value.</param>
        /// <param name="onError">The maps the errpr.</param>
        /// <returns>The result of the mapping.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Fold<T, TResult>(this Result<T> result, Func<T, TResult> onSuccess, Func<Exception, TResult> onError)
        {
            return result.IsSuccess ?
                onSuccess(result._value) :
                onError(result._error!);
        }

        /// <summary>
        /// Maps the result either if is a success or an error.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="onSuccess">The maps the value.</param>
        /// <param name="onError">The maps the errpr.</param>
        /// <returns>The result of the mapping.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Fold<T, TError, TResult>(this Result<T, TError> result, Func<T, TResult> onSuccess, Func<TError, TResult> onError)
        {
            return result.IsSuccess ?
                onSuccess(result._value) :
                onError(result._error);
        }
    }
}