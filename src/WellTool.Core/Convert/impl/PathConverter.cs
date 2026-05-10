using System.IO;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// Path杞崲鍣?
/// </summary>
public class PathConverter : AbstractConverter<FileInfo>
{
    protected override FileInfo ConvertInternal(object value)
    {
        try
        {
            if (value is Uri uri)
            {
                return new FileInfo(uri.LocalPath);
            }
            if (value is FileInfo fi)
            {
                return fi;
            }
            if (value is string path)
            {
                return new FileInfo(path);
            }
            return new FileInfo(ConvertToStr(value));
        }
        catch
        {
            return new FileInfo(ConvertToStr(value));
        }
    }
}

