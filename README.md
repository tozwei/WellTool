# WellTool

🍬A set of tools that keep C# sweet.

👉 https://github.com/tozwei/WellTool 👈

## 📚简介
WellTool 是一个功能丰富且易用的 C# 工具库，通过诸多实用工具类的使用，旨在帮助开发者快速、便捷地完成各类开发任务。
这些封装的工具涵盖了字符串、数字、集合、编码、日期、文件、IO、加密、数据库、JSON、HTTP 客户端等一系列操作，
可以满足各种不同的开发需求。

## 🎁WellTool 名称的由来
WellTool = Well + tool，寓意工具库的完善与易用，同时也表达了对开发者的美好祝愿。

## 🍺WellTool 理念
WellTool 既是一个工具集，也是一个知识库，我们从不自诩代码原创，大多数工具类都是搬运而来，因此：

- 你可以引入使用，也可以拷贝和修改使用，而不必标注任何信息，只是希望能把 bug 及时反馈回来。
- 我们努力健全中文注释，为源码学习者提供良好地学习环境，争取做到人人都能看得懂。

## 🛠️包含组件
一个 C# 基础工具类，对文件、流、加密解密、转码、正则、线程、XML 等方法进行封装，组成各种 Util 工具类，同时提供以下组件：

| 模块 | 介绍 |
| --- | --- |
| WellTool.Core | 核心，包括 Bean 操作、日期、各种 Util 等 |
| WellTool.AI | AI 大模型封装实现 |
| WellTool.Aop | 动态代理封装，提供切面支持 |
| WellTool.BloomFilter | 布隆过滤，提供一些 Hash 算法的布隆过滤 |
| WellTool.Cache | 简单缓存实现 |
| WellTool.Captcha | 图片验证码实现 |
| WellTool.Cron | 定时任务模块，提供类 Crontab 表达式的定时任务 |
| WellTool.Crypto | 加密解密模块，提供对称、非对称和摘要算法封装 |
| WellTool.DB | 数据操作，基于 ActiveRecord 思想 |
| WellTool.Dfa | 基于 DFA 模型的多关键字查找 |
| WellTool.Extra | 扩展模块，对第三方封装 |
| WellTool.Http | 基于 HttpClient 的 Http 客户端封装 |
| WellTool.Json | JSON 实现 |
| WellTool.Jwt | JSON Web Token (JWT) 封装实现 |
| WellTool.Log | 自动识别日志实现的日志门面 |
| WellTool.Poi | 针对 Excel 和 Word 的封装 |
| WellTool.Script | 脚本执行封装，例如 JavaScript |
| WellTool.Setting | 功能更强大的 Setting 配置文件封装 |
| WellTool.Socket | 基于 C# 的网络通信封装 |
| WellTool.System | 系统参数调用封装（系统信息等） |

可以根据需求对每个模块单独引入，也可以通过引入 WellTool.All 方式引入所有模块。

## 📝文档
- 📘中文文档
- 📙参考 API

## 📦安装
### 📦NuGet
在项目的包管理器控制台中执行以下命令：

```powershell
Install-Package WellTool.All
```

或在项目文件中添加依赖：

```xml
<PackageReference Include="WellTool.All" Version="1.0.0" />
```

## 🔔️注意
WellTool 支持 .NET Framework 4.6+ 和 .NET Core 3.1+，对其他平台没有测试，不能保证所有工具类或工具方法可用。

## 🚽编译安装
访问 WellTool 的 GitHub 主页：https://github.com/tozwei/WellTool 下载整个项目源码，然后进入 WellTool 项目目录执行：

```powershell
dotnet build
```

然后就可以使用 NuGet 引入了。

## 🏗️添砖加瓦
### 🎋分支说明
WellTool 的源码分为两个分支，功能如下：

| 分支 | 作用 |
| --- | --- |
| master | 主分支，release 版本使用的分支，与 NuGet 提交的包一致，不接收任何 pr 或修改 |
| dev | 开发分支，默认为下个版本的版本，接受修改或 pr |

### 🐞提供 bug 反馈或建议
提交问题反馈请说明正在使用的 .NET 版本、WellTool 版本和相关依赖库版本。

- GitHub issue

### 🧬贡献代码的步骤

1. 在 GitHub 上 fork 项目到自己的 repo
2. 把 fork 过去的项目也就是你的项目 clone 到你的本地
3. 修改代码（记得一定要修改 dev 分支）
4. commit 后 push 到自己的库（dev 分支）
5. 登录 GitHub 在你首页可以看到一个 pull request 按钮，点击它，填写一些说明信息，然后提交即可。
6. 等待维护者合并

### 📐PR 遵照的原则
WellTool 欢迎任何人为 WellTool 添砖加瓦，贡献代码，不过维护者是一个强迫症患者，为了照顾病人，需要提交的 pr（pull request）符合一些规范，规范如下：

- 注释完备，尤其每个新增的方法应按照 C# 文档规范标明方法说明、参数说明、返回值说明等信息，必要时请添加单元测试，如果愿意，也可以加上你的大名。
- WellTool 的缩进按照 Visual Studio 默认设置，代码风格保持一致。
- 请尽量保持 PR 的单一性，一个 PR 只解决一个问题或添加一个功能，不要在一个 PR 中提交多个不相关的修改。
- 提交代码前请确保所有单元测试通过。

## 📄许可证
WellTool 采用 MIT 许可证，详情请查看 [LICENSE](LICENSE) 文件。

## 🙏致谢
WellTool 借鉴了许多优秀的开源项目，在此表示感谢：

- [Hutool](https://gitee.com/chinabugotech/hutool) - Java 工具库
- [NPOI](https://github.com/nissl-lab/npoi) - .NET POI 实现
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) - JSON 处理库

## 📞联系我们
- GitHub: https://github.com/tozwei/WellTool
- Gitee: https://gitee.com/zwell/WellTool

---

**WellTool，让 C# 编程更甜蜜！** 🍬