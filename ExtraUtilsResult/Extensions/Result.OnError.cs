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
        /// <param name="onError">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnError(this Result result, Action<Exception> onError)
        {
            if (result.IsError)
            {
                onError(result._error!);
            }
        }

        /// <summary>
        /// Performs the given action if the result is an error.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="onError">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnError<T>(this Result<T> result, Action<Exception> onError)
        {
            if (result.IsError)
            {
                onError(result._error!);
            }
        }

        /// <summary>
        /// Performs the given action if the result is an error.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="onError">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnError<T, TError>(this Result<T, TError> result, Action<TError> onError)
        {
            if (result.IsError)
            {
                onError(result._error);
            }
        }
    }
}
