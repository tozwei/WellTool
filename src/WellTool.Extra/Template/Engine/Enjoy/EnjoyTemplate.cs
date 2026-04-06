using System;
using System.Collections.Generic;
using System.IO;

namespace WellTool.Extra.Template.Engine.Enjoy
{
    /// <summary>
    /// Enjoy模板实现
    /// </summary>
    public class EnjoyTemplate : Template
    {
        private readonly string _template;

        public EnjoyTemplate(string template)
        {
            _template = template;
        }

        /// <summary>
        /// 渲染模板
        /// </summary>
        /// <param name="bindingMap">绑定参数</param>
        /// <param name="writer">输出流</param>
        public void Render(IDictionary<object, object> bindingMap, TextWriter writer)
        {
            // 这里需要集成 JinianNet.JiTemplate 或类似库
            writer.Write(_template);
        }

        /// <summary>
        /// 渲染模板到流
        /// </summary>
        /// <param name="bindingMap">绑定参数</param>
        /// <param name="output">输出流</param>
        public void Render(IDictionary<object, object> bindingMap, Stream output)
        {
            using (var writer = new StreamWriter(output))
            {
                Render(bindingMap, writer);
                writer.Flush();
            }
        }

        /// <summary>
        /// 渲染模板到文件
        /// </summary>
        /// <param name="bindingMap">绑定参数</param>
        /// <param name="file">文件</param>
        public void Render(IDictionary<object, object> bindingMap, FileInfo file)
        {
            using (var writer = file.CreateText())
            {
                Render(bindingMap, writer);
            }
        }

        /// <summary>
        /// 渲染模板并返回字符串
        /// </summary>
        /// <param name="bindingMap">绑定参数</param>
        /// <returns>渲染结果</returns>
        public string Render(IDictionary<object, object> bindingMap)
        {
            using (var writer = new StringWriter())
            {
                Render(bindingMap, writer);
                return writer.ToString();
            }
        }
    }
}
