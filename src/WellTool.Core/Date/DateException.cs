using System;

namespace WellTool.Core.Date
{
    public class DateException : Exception
    {
        public DateException() : base() { }

        public DateException(string message) : base(message) { }

        public DateException(string message, Exception innerException) : base(message, innerException) { }
    }
}