using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ExtraUtils.Utilities;

namespace ExtraUtils
{
    /// <summary>
    /// Represents a result that can be a succesful value or an <see cref="Exception"/>.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <seealso cref="IResult" />
    [Serializable]
    public readonly struct Result<T> : IResult, IEquatable<Result<T>>
    {
        internal readonly T _value;
        internal readonly Exception? _error;

        public bool IsSuccess
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _error == null;
        }

        public bool IsError
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _error != null;
        }

        internal Result(T value, Exception? error)
        {
            _value = value;
            _error = error;
        }

        /// <summary>
        /// Gets the value of this result.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        /// <exception cref="InvalidOperationException">If the result is an error.</exception>
        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (IsSuccess)
                {
                    return _value;
                }

                throw new InvalidOperationException(ErrorMessages.NoValueResult);
            }
        }

        /// <summary>
        /// Gets the error of this result.
        /// </summary>
        /// <returns>
        /// The error.
        /// </returns>
        /// <exception cref="InvalidOperationException">If the result is a succes.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Exception GetError()
        {
            if (IsError)
            {
                return _error!;
            }

            throw new InvalidOperationException(ErrorMessages.NoErrorResult);
        }

        /// <summary>
        /// Attemps to get the value of this result.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the result is a success.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue([MaybeNullWhen(false)] out T value)
        {
            value = _value;
            return IsSuccess;
        }

        /// <summary>
        /// Attemps to get the error of this result.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns><c>true</c> if the result is an error.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetError([MaybeNullWhen(false)] out Exception? error)
        {
            error = _error;
            return IsError;
        }

        /// <summary>
        /// Determines whether this result contains the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified result contains the value; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsValue(T value)
        {
            return IsSuccess ? EqualityComparer<T>.Default.Equals(_value, value) : false;
        }

        /// <summary>
        /// Determines whether this result contains the given error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>
        ///   <c>true</c> if the specified result contains the error; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsError(Exception error)
        {
            return IsError ? _error == error : false;
        }

        /// <summary>
        /// Gets a string representation of this result.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (IsSuccess)
            {
                return $"Result({_value})";
            }

            string msg = _error!.Message;

            if (string.IsNullOrWhiteSpace(msg))
            {
                return $"Result({_error})";
            }

            return $"Result({msg})";
        }

        public override bool Equals(object? obj)
        {
            return obj is Result<T> result && Equals(result);
        }

        public bool Equals([AllowNull] Result<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value) &&
                   EqualityComparer<Exception?>.Default.Equals(_error, other._error);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result(in Result<T> result)
        {
            return result.IsSuccess ? new Result() : new Result(result.GetError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(in Result<T> result)
        {
            return result.Value;
        }

        public static bool operator ==(in Result<T> left, in Result<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(in Result<T> left, in Result<T> right)
        {
            return !(left == right);
        }
    }
}
