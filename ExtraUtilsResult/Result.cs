using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ExtraUtils.Utilities;

namespace ExtraUtils
{
    /// <summary>
    /// Represents a result that can be succesful or an <see cref="Exception"/>.
    /// </summary>
    /// <seealso cref="IResult" />
    [Serializable]
    public readonly partial struct Result : IResult, IEquatable<Result>
    {
        internal readonly Exception? _error;

        internal Result(Exception? error)
        {
            _error = error;
        }

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

        /// <summary>
        /// Gets the error of this result.
        /// </summary>
        /// <returns>
        /// The error.
        /// </returns>
        /// <exception cref="InvalidOperationException">If the result is a success.</exception>
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
        /// Gets a string representation of this result.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (IsSuccess)
            {
                return "Result(Ok)";
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
            return obj is Result result && Equals(result);
        }

        public bool Equals([AllowNull] Result other)
        {
            return EqualityComparer<Exception?>.Default.Equals(_error, other._error);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_error);
        }

        public static bool operator ==(Result left, Result right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Result left, Result right)
        {
            return !(left == right);
        }
    }
}
