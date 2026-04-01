using System.Collections.Generic;
using System.IO;

namespace WellTool.Extra.Template
{
    /// <summary>
    /// 抽象模板接口
    /// </summary>
    public interface Template
    {
        /// <summary>
        /// 将模板与绑定参数融合后输出到Writer
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <param name="writer">输出</param>
        void Render(IDictionary<object, object> bindingMap, TextWriter writer);

        /// <summary>
        /// 将模板与绑定参数融合后输出到流
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <param name="output">输出</param>
        void Render(IDictionary<object, object> bindingMap, Stream output);

        /// <summary>
        /// 写出到文件
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <param name="file">输出到的文件</param>
        void Render(IDictionary<object, object> bindingMap, FileInfo file);

        /// <summary>
        /// 将模板与绑定参数融合后返回为字符串
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <returns>融合后的内容</returns>
        string Render(IDictionary<object, object> bindingMap);
    }
}