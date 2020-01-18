using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils
{
    public partial struct Result
    {
        /// <summary>
        /// Creates a succesful <see cref="Result"/>.
        /// </summary>
        /// <returns>A succesful result.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result Ok()
        {
            return new Result();
        }

        /// <summary>
        /// Creates a <see cref="Result{T}"/> with a success value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>A result with a success value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, null);
        }

        /// <summary>
        /// Creates a <see cref="Result{T, TError}"/> with a success value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>A result with a success value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TError> Ok<T, TError>(T value)
        {
            return new Result<T, TError>(value, default!, isSuccess: true);
        }

        /// <summary>
        /// Creates a failed <see cref="Result"/>.
        /// </summary>
        /// <param name="msg">The error message.</param>
        /// <returns>A result with an error message.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result Error(string msg)
        {
            return new Result(new Exception(msg));
        }

        /// <summary>
        /// Creates a <see cref="Result{T}"/> with an error value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam
        /// <param name="msg">The error message.</param>
        /// <returns>A result with an error message.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Error<T>(string msg)
        {
            return new Result<T>(default!, new Exception(msg));
        }

        /// <summary>
        /// Creates a <see cref="Result"/> with an error value.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>A result with an error value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result Error(Exception error)
        {
            return new Result(error);
        }

        /// <summary>
        /// Creates a <see cref="Result{T}"/> with an error value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam
        /// <param name="error">The error.</param>
        /// <returns>A result with an error value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Error<T>(Exception error)
        {
            return new Result<T>(default!, error);
        }

        /// <summary>
        /// Creates a <see cref="Result{T, TError}"/> with an error value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="error">The error.</param>
        /// <returns>A result with an error value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TError> Error<T, TError>(TError error)
        {
            return new Result<T, TError>(default!, error, isSuccess: false);
        }
    }
}
