using System;

namespace Client.Util
{
    /// <inheritdoc cref="Exception"/>
    /// <summary>
    /// Definition for custom exception regarding Command Syntax.
    /// </summary>
    /// <seealso cref="Exception" />
    public class CommandSyntaxException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSyntaxException"/> class.
        /// </summary>
        public CommandSyntaxException() { }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSyntaxException"/> class.
        /// </summary>
        /// <param name="aMessage">The message that describes the error.</param>
        public CommandSyntaxException(string aMessage) : base(aMessage) { }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSyntaxException"/> class.
        /// </summary>
        /// <param name="aMessage">The message that describes the error.</param>
        /// <param name="anInner">The inner exception(s).</param>
        public CommandSyntaxException(string aMessage, Exception anInner) : base(aMessage, anInner) { }
    }
}
