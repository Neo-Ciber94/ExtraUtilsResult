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
        /// <param name="onSuccess">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnSuccess(this Result result, Action onSuccess)
        {
            if (result.IsSuccess)
            {
                onSuccess();
            }
        }

        /// <summary>
        /// Performs the given action if the result is a success.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="onSuccess">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnSuccess<T>(this Result<T> result, Action<T> onSuccess)
        {
            if (result.IsSuccess)
            {
                onSuccess(result._value);
            }
        }

        /// <summary>
        /// Performs the given action if the result is a success.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="onSuccess">The action to perform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnSuccess<T, TError>(this Result<T, TError> result, Action<T> onSuccess)
        {
            if (result.IsSuccess)
            {
                onSuccess(result._value);
            }
        }
    }
}
