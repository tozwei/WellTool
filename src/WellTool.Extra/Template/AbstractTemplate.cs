using System.Collections.Generic;
using System.IO;

namespace WellTool.Extra.Template
{
    /// <summary>
    /// 抽象模板类，提供通用方法实现
    /// </summary>
    public abstract class AbstractTemplate : Template
    {
        /// <summary>
        /// 将模板与绑定参数融合后输出到Writer
        /// </summary>
        /// <param name="bindingMap">绑定的参数</param>
        /// <param name="writer">输出</param>
        public abstract void Render(IDictionary<object, object> bindingMap, TextWriter writer);

        /// <summary>
        /// 将模板与绑定参数融合后输出到流
        /// </summary>
        /// <param name="bindingMap">绑定的参数</param>
        /// <param name="output">输出</param>
        public abstract void Render(IDictionary<object, object> bindingMap, Stream output);

        /// <summary>
        /// 写出到文件
        /// </summary>
        /// <param name="bindingMap">绑定的参数</param>
        /// <param name="file">输出到的文件</param>
        public virtual void Render(IDictionary<object, object> bindingMap, FileInfo file)
        {
            using var writer = new StreamWriter(file.FullName);
            Render(bindingMap, writer);
        }

        /// <summary>
        /// 将模板与绑定参数融合后返回为字符串
        /// </summary>
        /// <param name="bindingMap">绑定的参数</param>
        /// <returns>融合后的内容</returns>
        public virtual string Render(IDictionary<object, object> bindingMap)
        {
            using var writer = new StringWriter();
            Render(bindingMap, writer);
            return writer.ToString();
        }
    }
}
