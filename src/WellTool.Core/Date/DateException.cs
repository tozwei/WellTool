using System;

namespace WellTool.Core.Date
{
    public class DateException : System.Exception
    {
        public DateException() : base() { }

        public DateException(string message) : base(message) { }

        public DateException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}