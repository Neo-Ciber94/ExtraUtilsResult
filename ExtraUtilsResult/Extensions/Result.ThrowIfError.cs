using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Throw the error if the result contains it.
        /// </summary>
        /// <param name="result">The result.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfError(this Result result)
        {
            if (result.IsError)
            {
                throw result._error!;
            }
        }

        /// <summary>
        /// Throw the error if the result contains it.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="result">The result.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfError<T>(this Result<T> result)
        {
            if (result.IsError)
            {
                throw result._error!;
            }
        }

        /// <summary>
        /// Throw the error if the result contains it.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="result">The result.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfError<T, TError>(this Result<T, TError> result) where TError : Exception
        {
            if (result.IsError)
            {
                throw result._error!;
            }
        }
    }
}
