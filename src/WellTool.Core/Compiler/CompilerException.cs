using System;

namespace WellTool.Core.Compiler
{
    public class CompilerException : System.Exception
    {
        public CompilerException() { }

        public CompilerException(string message) : base(message) { }

        public CompilerException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
