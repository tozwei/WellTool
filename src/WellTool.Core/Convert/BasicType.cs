using System;
using System.Collections.Generic;

namespace WellTool.Core.Convert
{
    public enum BasicType
    {
        Byte,
        Short,
        Int,
        Integer,
        Long,
        Double,
        Float,
        Boolean,
        Char,
        Character,
        String
    }

    public static class BasicTypeUtil
    {
        public static readonly Dictionary<Type, Type> WrapperPrimitiveMap = new Dictionary<Type, Type>
        {
            { typeof(bool), typeof(bool) },
            { typeof(byte), typeof(byte) },
            { typeof(char), typeof(char) },
            { typeof(double), typeof(double) },
            { typeof(float), typeof(float) },
            { typeof(int), typeof(int) },
            { typeof(long), typeof(long) },
            { typeof(short), typeof(short) },
            { typeof(bool?), typeof(bool) },
            { typeof(byte?), typeof(byte) },
            { typeof(char?), typeof(char) },
            { typeof(double?), typeof(double) },
            { typeof(float?), typeof(float) },
            { typeof(int?), typeof(int) },
            { typeof(long?), typeof(long) },
            { typeof(short?), typeof(short) }
        };

        public static readonly Dictionary<Type, Type> PrimitiveWrapperMap = new Dictionary<Type, Type>
        {
            { typeof(bool), typeof(bool?) },
            { typeof(byte), typeof(byte?) },
            { typeof(char), typeof(char?) },
            { typeof(double), typeof(double?) },
            { typeof(float), typeof(float?) },
            { typeof(int), typeof(int?) },
            { typeof(long), typeof(long?) },
            { typeof(short), typeof(short?) }
        };

        public static Type Wrap(Type type)
        {
            if (type == null || !type.IsPrimitive)
            {
                return type;
            }

            if (PrimitiveWrapperMap.TryGetValue(type, out var wrapperType))
            {
                return wrapperType;
            }

            return type;
        }

        public static Type Unwrap(Type type)
        {
            if (type == null || type.IsPrimitive)
            {
                return type;
            }

            if (WrapperPrimitiveMap.TryGetValue(type, out var primitiveType))
            {
                return primitiveType;
            }

            return type;
        }
    }
}