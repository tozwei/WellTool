using System;

namespace WellTool.Core.Exceptions
{
    public class UtilException : BaseException
    {
        public UtilException() { }

        public UtilException(string message) : base(message) { }

        public UtilException(string message, System.Exception innerException) : base(message, innerException) { }

        public UtilException(System.Exception innerException) : base(innerException.Message, innerException) { }
    }
}
