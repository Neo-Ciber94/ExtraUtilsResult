using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// If the result if an error throw it if is of the given type.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="exceptionType">The type of the exception.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfType(this Result result, Type exceptionType)
        {
            if (result.IsError && result.GetError().GetType() == exceptionType)
            {
                throw result._error!;
            }
        }

        /// <summary>
        /// If the result if an error throw it if is of the given type.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="exceptionType">The type of the exception.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfType<T>(this Result<T> result, Type exceptionType)
        {
            if (result.IsError && result.GetError().GetType() == exceptionType)
            {
                throw result._error!;
            }
        }
    }
}
