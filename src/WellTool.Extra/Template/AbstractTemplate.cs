using System.Collections.Generic;
using System.IO;

namespace WellTool.Extra.Template
{
    /// <summary>
    /// 抽象模板，提供将模板融合后写出到文件、返回字符串等方法
    /// </summary>
    public abstract class AbstractTemplate : Template
    {
        /// <summary>
        /// 将模板与绑定参数融合后输出到Writer
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <param name="writer">输出</param>
        public abstract void Render(IDictionary<object, object> bindingMap, TextWriter writer);

        /// <summary>
        /// 将模板与绑定参数融合后输出到流
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <param name="output">输出</param>
        public abstract void Render(IDictionary<object, object> bindingMap, Stream output);

        /// <summary>
        /// 写出到文件
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <param name="file">输出到的文件</param>
        public void Render(IDictionary<object, object> bindingMap, FileInfo file)
        {
            using (FileStream outStream = file.Create())
            {
                Render(bindingMap, outStream);
            }
        }

        /// <summary>
        /// 将模板与绑定参数融合后返回为字符串
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <returns>融合后的内容</returns>
        public string Render(IDictionary<object, object> bindingMap)
        {
            using (StringWriter writer = new StringWriter())
            {
                Render(bindingMap, writer);
                return writer.ToString();
            }
        }
    }
}