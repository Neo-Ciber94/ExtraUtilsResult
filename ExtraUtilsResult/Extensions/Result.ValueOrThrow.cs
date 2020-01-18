using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Gets the value or throw the exception of the result if an error.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>The value of the result</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueOrThrow<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return result._value;
            }

            throw result._error!;
        }

        /// <summary>
        /// Gets the value or throw the exception of the result if an error.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <typeparam name="TError">Type of the exception</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>The value of the result</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueOrThrow<T, TError>(this Result<T, TError> result) where TError: Exception
        {
            if (result.IsSuccess)
            {
                return result._value;
            }

            throw result._error!;
        }
    }
}
