using System;

namespace WellTool.Core.Exceptions
{
    public class BaseException : System.Exception
    {
        public BaseException() { }

        public BaseException(string message) : base(message) { }

        public BaseException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
