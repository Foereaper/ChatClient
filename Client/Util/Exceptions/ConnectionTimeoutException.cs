using System;

namespace Client.Util
{
    /// <inheritdoc cref="Exception"/>
    /// <summary>
    /// Definition for custom exception regarding Connection Timeouts.
    /// </summary>
    /// <seealso cref="Exception" />
    public class ConnectionTimeoutException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionTimeoutException"/> class.
        /// </summary>
        public ConnectionTimeoutException() { }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionTimeoutException"/> class.
        /// </summary>
        /// <param name="aMessage">The message that describes the error.</param>
        public ConnectionTimeoutException(string aMessage) : base(aMessage) { }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionTimeoutException"/> class.
        /// </summary>
        /// <param name="aMessage">The message that describes the error.</param>
        /// <param name="anInner">The inner exception(s).</param>
        public ConnectionTimeoutException(string aMessage, Exception anInner) : base(aMessage, anInner) { }
    }
}
