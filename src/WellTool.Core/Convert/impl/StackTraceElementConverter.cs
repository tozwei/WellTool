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
            map.TryGetValue("className", out var classNameValue);
            map.TryGetValue("methodName", out var methodNameValue);
            map.TryGetValue("fileName", out var fileNameValue);
            map.TryGetValue("lineNumber", out var lineNumberValue);
            
            var declaringClass = classNameValue?.ToString() ?? "";
            var methodName = methodNameValue?.ToString() ?? "";
            var fileName = fileNameValue?.ToString();
            var lineNumberStr = lineNumberValue?.ToString() ?? "0";
            var lineNumber = int.TryParse(lineNumberStr, out var ln) ? ln : 0;

            var methodInfo = new StackFrame(fileName, lineNumber);
            return methodInfo;
        }
        return new StackFrame();
    }
}
