using System.Text;

namespace WellTool.Extra.Template
{
    /// <summary>
    /// 模板配置
    /// </summary>
    public class TemplateConfig
    {
        /// <summary>
        /// 默认配置
        /// </summary>
        public static readonly TemplateConfig Default = new TemplateConfig();

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Charset { get; set; }

        /// <summary>
        /// 模板路径，如果ClassPath或者WebRoot模式，则表示相对路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 模板资源加载方式
        /// </summary>
        public ResourceModeType ResourceMode { get; set; }

        /// <summary>
        /// 自定义引擎，当多个jar包引入时，可以自定使用的默认引擎
        /// </summary>
        public Type CustomEngine { get; set; }

        /// <summary>
        /// 是否使用缓存
        /// </summary>
        public bool UseCache { get; set; } = true;

        /// <summary>
        /// 默认构造，使用UTF8编码，默认从ClassPath获取模板
        /// </summary>
        public TemplateConfig() : this(null)
        {
        }

        /// <summary>
        /// 构造，默认UTF-8编码
        /// </summary>
        /// <param name="path">模板路径，如果ClassPath或者WebRoot模式，则表示相对路径</param>
        public TemplateConfig(string path) : this(Encoding.UTF8, path, ResourceModeType.String)
        {
        }

        /// <summary>
        /// 构造，默认UTF-8编码
        /// </summary>
        /// <param name="path">模板路径，如果ClassPath或者WebRoot模式，则表示相对路径</param>
        /// <param name="resourceMode">模板资源加载方式</param>
        public TemplateConfig(string path, ResourceModeType resourceMode) : this(Encoding.UTF8, path, resourceMode)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="charset">编码</param>
        /// <param name="path">模板路径，如果ClassPath或者WebRoot模式，则表示相对路径</param>
        /// <param name="resourceMode">模板资源加载方式</param>
        public TemplateConfig(Encoding charset, string path, ResourceModeType resourceMode)
        {
            Charset = charset;
            Path = path;
            ResourceMode = resourceMode;
        }

        /// <summary>
        /// 获取编码字符串
        /// </summary>
        /// <returns>编码字符串</returns>
        public string GetCharsetStr()
        {
            return Charset?.EncodingName;
        }

        /// <summary>
        /// 设置自定义引擎，null表示系统自动判断
        /// </summary>
        /// <param name="customEngine">自定义引擎，null表示系统自动判断</param>
        /// <returns>this</returns>
        public TemplateConfig SetCustomEngine(Type customEngine)
        {
            CustomEngine = customEngine;
            return this;
        }

        /// <summary>
        /// 设置是否使用缓存
        /// </summary>
        /// <param name="useCache">是否使用缓存</param>
        /// <returns>this</returns>
        public TemplateConfig SetUseCache(bool useCache)
        {
            UseCache = useCache;
            return this;
        }

        /// <summary>
        /// 资源加载方式枚举
        /// </summary>
        public enum ResourceModeType
        {
            /// <summary>
            /// 从ClassPath加载模板
            /// </summary>
            ClassPath,
            /// <summary>
            /// 从File目录加载模板
            /// </summary>
            File,
            /// <summary>
            /// 从WebRoot目录加载模板
            /// </summary>
            WebRoot,
            /// <summary>
            /// 从模板文本加载模板
            /// </summary>
            String,
            /// <summary>
            /// 复合加载模板（分别从File、ClassPath、Web-root、String方式尝试查找模板）
            /// </summary>
            Composite
        }
    }
}