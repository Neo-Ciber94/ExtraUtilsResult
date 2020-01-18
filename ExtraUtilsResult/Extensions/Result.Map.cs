using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Maps the value of this result.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>A result with a new value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> mapper)
        {
            return result.IsSuccess ? 
                new Result<TResult>(mapper(result._value), null) : 
                new Result<TResult>(default!, result._error);
        }

        /// <summary>
        /// Maps the value of this result.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>A result with a new value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult, TError> Map<T, TError, TResult>(this Result<T, TError> result, Func<T, TResult> mapper)
        {
            return result.IsSuccess ?
                new Result<TResult, TError>(mapper(result._value), default!, isSuccess: true) :
                new Result<TResult, TError>(default!, result._error, isSuccess: false);
        }

        /// <summary>
        /// Maps the error of this result.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>A result with a new value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TResult> MapError<T, TResult>(this Result<T> result, Func<Exception, TResult> mapper)
        {
            return result.IsError ?
                new Result<T, TResult>(default!, mapper(result._error!), isSuccess: false) :
                new Result<T, TResult>(result._value!, default!, isSuccess: true);
        }

        /// <summary>
        /// Maps the error of this result.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>A result with a new value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TResult> MapError<T, TError, TResult>(this Result<T, TError> result, Func<TError, TResult> mapper)
        {
            return result.IsError ?
                new Result<T, TResult>(default!, mapper(result._error), isSuccess: false) :
                new Result<T, TResult>(result._value!, default!, isSuccess: true);
        }
    }
}
