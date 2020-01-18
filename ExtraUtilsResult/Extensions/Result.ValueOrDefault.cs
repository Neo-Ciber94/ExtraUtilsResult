using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Gets the value or the default if the result is an error.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>The result of the value or the default.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueOrDefault<T>(this Result<T> result)
        {
            return result.IsSuccess ? result._value : default!;
        }

        /// <summary>
        /// Gets the value or the default if the result is an error.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>The result of the value or the default.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueOrDefault<T, TError>(this Result<T, TError> result)
        {
            return result.IsSuccess ? result._value : default!;
        }

        /// <summary>
        /// Gets the value or the default if the result is an error.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The result of the value or the default.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueOrDefault<T>(this Result<T> result, T defaultValue)
        {
            return result.IsSuccess ? result._value : defaultValue!;
        }

        /// <summary>
        /// Gets the value or the default if the result is an error.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The result of the value or the default.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ValueOrDefault<T, TError>(this Result<T, TError> result, T defaultValue)
        {
            return result.IsSuccess ? result._value : defaultValue!;
        }
    }
}
