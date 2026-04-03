using System.Diagnostics;
using System.Globalization;

namespace WellTool.Core.Convert.impl;

/// <summary>
/// StackTraceElement转换器
/// </summary>
public class StackTraceElementConverter : AbstractConverter<StackFrame>
{
    protected override StackFrame ConvertInternal(object value)
    {
        if (value is IDictionary<string, object> map)
        {
            var declaringClass = map.GetValueOrDefault("className")?.ToString() ?? "";
            var methodName = map.GetValueOrDefault("methodName")?.ToString() ?? "";
            var fileName = map.GetValueOrDefault("fileName")?.ToString();
            var lineNumberStr = map.GetValueOrDefault("lineNumber")?.ToString() ?? "0";
            var lineNumber = int.TryParse(lineNumberStr, out var ln) ? ln : 0;

            var methodInfo = new StackFrame(fileName, lineNumber);
            return methodInfo;
        }
        return new StackFrame();
    }
}
