
namespace ExtraUtils
{
    /// <summary>
    /// Represents a result.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets a value indicating whether this result is a success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the result is a success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether this result is an error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the result is an error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError { get; }
    }
}
