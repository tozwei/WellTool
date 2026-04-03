using System;
using System.IO;
using System.Reflection;
using WellTool.Core.IO;
using WellTool.Extra.Template;

namespace WellTool.Extra.Template.Engine.Freemarker
{
    /// <summary>
    /// FreeMarker模板引擎封装
    /// 文档: https://freemarker.apache.org/
    /// </summary>
    public class FreemarkerEngine : TemplateEngine
    {
        private dynamic? _cfg;

        /// <summary>
        /// 默认构造
        /// </summary>
        public FreemarkerEngine()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">模板配置</param>
        public FreemarkerEngine(TemplateConfig config)
        {
            Init(config);
        }

        /// <summary>
        /// 使用指定配置文件初始化模板引擎
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <returns>this</returns>
        public TemplateEngine Init(TemplateConfig config)
        {
            if (config == null)
            {
                config = TemplateConfig.Default;
            }
            Init(CreateCfg(config));
            return this;
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="freemarkerCfg">Configuration</param>
        private void Init(dynamic freemarkerCfg)
        {
            this._cfg = freemarkerCfg;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="resource">资源</param>
        /// <returns>模板实现</returns>
        public override Template GetTemplate(string resource)
        {
            if (this._cfg == null)
            {
                Init(TemplateConfig.Default);
            }
            try
            {
                var template = _cfg.GetTemplate(resource);
                return FreemarkerTemplate.Wrap(template)!;
            }
            catch (IOException e)
            {
                throw new IORuntimeException(e);
            }
            catch (Exception e)
            {
                throw new TemplateException(e);
            }
        }

        /// <summary>
        /// 获取原始引擎的钩子方法，用于自定义特殊属性
        /// </summary>
        /// <returns>Configuration</returns>
        public dynamic? GetConfiguration()
        {
            return this._cfg;
        }

        /// <summary>
        /// 创建配置项
        /// </summary>
        /// <param name="config">模板配置</param>
        /// <returns>Configuration</returns>
        private dynamic CreateCfg(TemplateConfig config)
        {
            if (config == null)
            {
                config = new TemplateConfig();
            }

            // 创建FreeMarker Configuration
            var cfgType = Type.GetType("freemarker.template.Configuration, freemarker");
            if (cfgType == null)
            {
                throw new DllNotFoundException("FreeMarker库未找到，请添加FreeMarker NuGet包");
            }

            var cfg = Activator.CreateInstance(cfgType);

            // 设置基本配置
            cfg.SetLocalizedLookup(false);
            cfg.SetDefaultEncoding(config.Charset.WebName);

            switch (config.ResourceMode)
            {
                case TemplateConfig.ResourceModeEnum.Classpath:
                    var classLoader = Assembly.GetExecutingAssembly().GetType().Assembly.GetName().Name;
                    dynamic classTemplateLoader = CreateClassTemplateLoader(classLoader, config.Path ?? "");
                    cfg.SetTemplateLoader(classTemplateLoader);
                    break;
                case TemplateConfig.ResourceModeEnum.File:
                    cfg.SetTemplateLoader(CreateFileTemplateLoader(FileUtil.File(config.Path ?? "")));
                    break;
                case TemplateConfig.ResourceModeEnum.WebRoot:
                    cfg.SetTemplateLoader(CreateFileTemplateLoader(FileUtil.GetWebRoot()));
                    break;
                case TemplateConfig.ResourceModeEnum.String:
                    cfg.SetTemplateLoader(new SimpleStringTemplateLoader());
                    break;
            }

            return cfg;
        }

        private static object CreateClassTemplateLoader(string basePackagePath, string path)
        {
            var loaderType = Type.GetType("freemarker.cache.ClassTemplateLoader, freemarker");
            if (loaderType == null)
            {
                throw new DllNotFoundException("FreeMarker库未找到");
            }
            var classLoader = Assembly.GetExecutingAssembly().GetType().Assembly;
            return Activator.CreateInstance(loaderType, classLoader, path)!;
        }

        private static object CreateFileTemplateLoader(FileInfo? directory)
        {
            var loaderType = Type.GetType("freemarker.cache.FileTemplateLoader, freemarker");
            if (loaderType == null)
            {
                throw new DllNotFoundException("FreeMarker库未找到");
            }
            return Activator.CreateInstance(loaderType, directory)!;
        }
    }
}
