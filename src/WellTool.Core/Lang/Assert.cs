using System;

namespace WellTool.Core.Lang
{
    public static class Assert
    {
        public static void NotNull(object obj, string message = "Object must not be null")
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void NotEmpty(string str, string message = "String must not be empty")
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(message);
            }
        }

        public static void NotBlank(string str, string message = "String must not be blank")
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(message);
            }
        }

        public static void IsTrue(bool condition, string message = "Condition must be true")
        {
            if (!condition)
            {
                throw new ArgumentException(message);
            }
        }

        public static void IsFalse(bool condition, string message = "Condition must be false")
        {
            if (condition)
            {
                throw new ArgumentException(message);
            }
        }

        public static void Equals(object expected, object actual, string message = "Objects must be equal")
        {
            if (!object.Equals(expected, actual))
            {
                throw new ArgumentException(message);
            }
        }

        public static void NotEquals(object expected, object actual, string message = "Objects must not be equal")
        {
            if (object.Equals(expected, actual))
            {
                throw new ArgumentException(message);
            }
        }

        public static void IsInstanceOfType(object obj, Type type, string message = "Object must be instance of specified type")
        {
            if (!type.IsInstanceOfType(obj))
            {
                throw new ArgumentException(message);
            }
        }

        public static void IsAssignableFrom(Type fromType, Type toType, string message = "Type must be assignable from specified type")
        {
            if (!fromType.IsAssignableFrom(toType))
            {
                throw new ArgumentException(message);
            }
        }
    }
}