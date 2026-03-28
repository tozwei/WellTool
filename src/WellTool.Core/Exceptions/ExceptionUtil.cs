using System;
using System.Collections.Generic;
using System.Text;

namespace WellTool.Core.Exceptions
{
    public static class ExceptionUtil
    {
        public static string GetMessage(Exception e)
        {
            if (e == null)
            {
                return null;
            }
            return e.Message;
        }

        public static string GetStacktrace(Exception e)
        {
            if (e == null)
            {
                return null;
            }
            return e.StackTrace ?? string.Empty;
        }

        public static Exception GetRootCause(Exception e)
        {
            if (e == null)
            {
                return null;
            }
            Exception cause = e;
            while (cause.InnerException != null)
            {
                cause = cause.InnerException;
            }
            return cause;
        }

        public static string GetRootCauseMessage(Exception e)
        {
            Exception rootCause = GetRootCause(e);
            return rootCause == null ? null : rootCause.Message;
        }

        public static bool IsCausedBy(Exception e, params Type[] causeClasses)
        {
            if (e == null || causeClasses == null || causeClasses.Length == 0)
            {
                return false;
            }
            Exception cause = e;
            while (cause != null)
            {
                foreach (Type causeClass in causeClasses)
                {
                    if (causeClass.IsAssignableFrom(cause.GetType()))
                    {
                        return true;
                    }
                }
                cause = cause.InnerException;
            }
            return false;
        }

        public static T WrapRuntime<T>(Exception e) where T : Exception
        {
            if (e == null)
            {
                return null;
            }
            if (e is T runtimeException)
            {
                return runtimeException;
            }
            return (T)Activator.CreateInstance(typeof(T), e.Message, e);
        }
    }
}
