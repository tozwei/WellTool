using System;

namespace WellTool.Core.Exceptions
{
    public class RuntimeException : BaseException
    {
        public RuntimeException() { }

        public RuntimeException(string message) : base(message) { }

        public RuntimeException(string message, Exception innerException) : base(message, innerException) { }

        public RuntimeException(Exception innerException) : base(innerException.Message, innerException) { }
    }
}