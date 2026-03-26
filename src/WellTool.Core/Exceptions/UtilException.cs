using System;

namespace WellTool.Core.Exceptions
{
    public class UtilException : BaseException
    {
        public UtilException() { }

        public UtilException(string message) : base(message) { }

        public UtilException(string message, Exception innerException) : base(message, innerException) { }
    }
}
