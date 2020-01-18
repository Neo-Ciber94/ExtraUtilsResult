using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ExtraUtils.Utilities;

namespace ExtraUtils
{
    /// <summary>
    /// Represents a result that can be either a value <typeparamref name="T"/> or an error <typeparamref name="TError"/>.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <typeparam name="TError">The type of the error.</typeparam>
    /// <seealso cref="IResult" />
    [Serializable]
    public readonly struct Result<T, TError> : IResult, IEquatable<Result<T, TError>>
    {
        internal readonly T _value;
        internal readonly TError _error;
        private readonly bool _isSuccess;

        public bool IsSuccess
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _isSuccess;
        }

        public bool IsError
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => !_isSuccess;
        }

        internal Result(T value, TError error, bool isSuccess)
        {
            _value = value;
            _error = error;
            _isSuccess = isSuccess;
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
        /// <exception cref="InvalidOperationException">If the result is a succesful.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TError GetError()
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
        public bool TryGetError([MaybeNullWhen(false)] out TError error)
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
        public bool ContainsError(TError error)
        {
            return IsError ? EqualityComparer<TError>.Default.Equals(_error, error) : false;
        }

        /// <summary>
        /// Gets a string representation of this result.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return IsSuccess ? $"Result({_value})" : $"Result({_error})";
        }

        public override bool Equals(object? obj)
        {
            return obj is Result<T, TError> result && Equals(result);
        }

        public bool Equals([AllowNull] Result<T, TError> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value) &&
                   EqualityComparer<TError>.Default.Equals(_error, other._error);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T>(in Result<T, TError> result)
        {
            if (result.IsSuccess)
            {
                return new Result<T>(result._value, null);
            }

            TError error = result._error;
            Exception? exception = error is Exception e ? e : new Exception(error!.ToString());
            return new Result<T>(result._value, exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result(in Result<T, TError> result)
        {
            if (result.IsSuccess)
            {
                return new Result();
            }

            TError error = result._error;
            Exception? exception = error is Exception e ? e : new Exception(error!.ToString());
            return new Result(exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(in Result<T, TError> result)
        {
            return result.Value;
        }

        public static bool operator ==(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(in Result<T, TError> left, in Result<T, TError> right)
        {
            return !(left == right);
        }
    }
}
