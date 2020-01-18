using System.Runtime.CompilerServices;

namespace ExtraUtils
{
	public static partial class ResultExtensions
	{
		/// <summary>
		/// Gets the value or null if the result is an error.
		/// </summary>
		/// <typeparam name="T">Type of the value</typeparam>
		/// <param name="result">The result.</param>
		/// <returns>The result of the value or null.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T? ValueOrNull<T>(this Result<T> result) where T : struct
		{
			return result.IsSuccess ? result._value : default(T?)!;
		}

		/// <summary>
		/// Gets the value or null if the result is an error.
		/// </summary>
		/// <typeparam name="T">Type of the value</typeparam>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <param name="result">The result.</param>
		/// <returns>The result of the value or null.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T? ValueOrNull<T, TError>(this Result<T, TError> result) where T : struct
		{
			return result.IsSuccess ? result._value : default(T?)!;
		}
	}
}
