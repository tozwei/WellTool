using System;
using System.IO;
using System.Reflection;

namespace WellTool.Core.IO
{
    public static class ResourceUtil
    {
        public static Stream GetResourceStream(string resourceName)
        {
            return GetResourceStream(Assembly.GetCallingAssembly(), resourceName);
        }

        public static Stream GetResourceStream(Assembly assembly, string resourceName)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            if (string.IsNullOrEmpty(resourceName))
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            string fullResourceName = GetFullResourceName(assembly, resourceName);
            return assembly.GetManifestResourceStream(fullResourceName);
        }

        public static string GetResourceString(string resourceName)
        {
            return GetResourceString(Assembly.GetCallingAssembly(), resourceName);
        }

        public static string GetResourceString(Assembly assembly, string resourceName)
        {
            using (var stream = GetResourceStream(assembly, resourceName))
            {
                if (stream == null)
                {
                    return null;
                }
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static byte[] GetResourceBytes(string resourceName)
        {
            return GetResourceBytes(Assembly.GetCallingAssembly(), resourceName);
        }

        public static byte[] GetResourceBytes(Assembly assembly, string resourceName)
        {
            using (var stream = GetResourceStream(assembly, resourceName))
            {
                if (stream == null)
                {
                    return null;
                }
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        private static string GetFullResourceName(Assembly assembly, string resourceName)
        {
            if (resourceName.StartsWith("/"))
            {
                resourceName = resourceName.Substring(1);
            }

            string[] resourceNames = assembly.GetManifestResourceNames();
            foreach (string fullName in resourceNames)
            {
                if (fullName.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase))
                {
                    return fullName;
                }
            }

            return resourceName;
        }
    }
}