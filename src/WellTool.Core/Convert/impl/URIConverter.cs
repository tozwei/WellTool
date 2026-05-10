using System;
using System.IO;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    public class URIConverter : AbstractConverter<Uri>
    {
        protected override Uri ConvertInternal(object value)
        {
            try
            {
                if (value is FileInfo file)
                {
                    return new Uri(file.FullName);
                }

                if (value is Uri uri)
                {
                    return uri;
                }

                string str = ConvertToStr(value);
                return string.IsNullOrEmpty(str) ? null : new Uri(str);
            }
            catch
            {
                return null;
            }
        }
    }
}