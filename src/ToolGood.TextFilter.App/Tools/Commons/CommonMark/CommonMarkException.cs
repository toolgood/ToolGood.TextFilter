using CommonMark.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonMark
{
    /// <summary>
    /// An exception that is caught during CommonMark parsing or formatting.
    /// </summary>
#if NET20 || NET35_CLIENT || NET40_CLIENT || NET45
    [Serializable]
#endif
    public class CommonMarkException : Exception
    {
        /// <summary>
        /// Gets the block that caused the exception. Can be <see langword="null"/>.
        /// </summary>
        public Block BlockElement { get; private set; }

        /// <summary>
        /// Gets the inline element that caused the exception. Can be <see langword="null"/>.
        /// </summary>
        public Inline InlineElement { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CommonMarkException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public CommonMarkException(string message) : base(message) { }

        /// <summary>Initializes a new instance of the <see cref="CommonMarkException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
        public CommonMarkException(string message, Exception innerException) : base(message, innerException) { }


        /// <summary>Initializes a new instance of the <see cref="CommonMarkException" /> class with a specified error message, a reference to the element that caused it and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="block">The block element that is related to the exception cause.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
        public CommonMarkException(string message, Block block, Exception innerException = null)
            : base(message, innerException) 
        {
            this.BlockElement = block;
        }

 
    }
}
