# WellTool.Setting 使用示例

## 📚 快速开始

### 1. 安装 NuGet 包

```bash
dotnet add package YamlDotNet --version 13.7.1
dotnet add package Microsoft.Extensions.Logging.Abstractions --version 8.0.0
```

或直接引用项目：

```xml
<ProjectReference Include="WellTool.Setting.csproj" />
```

## 🔧 Setting 配置文件使用

### 创建配置文件 (app.setting)

```ini
# 应用程序配置
app_name=MyApplication
app_version=1.0.0
debug=true
port=8080

# 数据库配置
[Database]
connection_string=localhost:5432
username=admin
password=secret123
max_connections=100

# 日志配置
[Logging]
level=INFO
path=${app_name}/logs
format=json
```

### C# 代码读取配置

```csharp
using WellTool.Setting;

// 方法 1: 直接创建实例
using var setting = new Setting("app.setting");

// 读取基本类型
string appName = setting.GetStr("app_name", "DefaultApp");
int port = setting.GetInt("port", 8080);
bool debug = setting.GetBool("debug", false);

// 读取分组配置
string connStr = setting.GetByGroup("connection_string", "Database");
string username = setting.GetByGroup("username", "Database");
int maxConn = setting.GetInt("max_connections", "Database", 10);

// 读取数组类型
string[] formats = setting.GetStrings("allowed_formats", "Api");

// 使用工具类获取（带缓存）
var cachedSetting = SettingUtil.Get("app.setting");
```

## 📄 Properties 文件使用

### 创建配置文件 (config.properties)

```properties
# 应用配置
app.name=MyApplication
app.version=2.0.0
app.debug=true

# 服务器配置
server.host=localhost
server.port:8080
```

### C# 代码读取

```csharp
using WellTool.Setting.Dialect;

// 方法 1: 直接创建实例
using var props = new Props("config.properties");

// 读取值
string appName = props.GetStr("app.name", "DefaultApp");
int appVersion = props.GetInt("app.version", 1);
bool isDebug = props.GetBool("app.debug", false);

// 读取服务器配置
string host = props.GetStr("server.host", "localhost");
int port = props.GetInt("server.port", 80);

// 使用方法 2: 工具类
var propsUtil = PropsUtil.GetProp("config.properties");

// 查找第一个存在的配置文件
var firstProps = PropsUtil.GetFirstFoundProp(
    "app.properties",
    "config.properties",
    "default.properties"
);
```

## 📝 YAML 文件使用

### 创建配置文件 (config.yml)

```yaml
server:
  host: localhost
  port: 8080
  ssl: true

database:
  url: jdbc:mysql://localhost:3306/mydb
  username: root
  password: secret
  
logging:
  level: INFO
  format: json
  outputs:
    - console
    - file
```

### C# 代码读取

```csharp
using WellTool.Setting.Yaml;

// 方法 1: 读取为字典
var config = YamlUtil.LoadByPath("config.yml");

// 访问嵌套配置
if (config.TryGetValue("server", out var serverObj) && 
    serverObj is IDictionary<string, object> server)
{
    var host = server["host"];
    var port = server["port"];
}

// 方法 2: 读取为强类型对象
public class ServerConfig
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool Ssl { get; set; }
}

var serverConfig = YamlUtil.LoadByPath<ServerConfig>("server.yml");

// 方法 3: 序列化对象为 YAML
var myConfig = new
{
    Name = "Test",
    Value = 123,
    Items = new[] { "item1", "item2" }
};

YamlUtil.Dump(myConfig, "output.yml");
```

## 🎯 实际应用场景

### 场景 1: ASP.NET Core 配置

```csharp
// Program.cs 或 Startup.cs
using WellTool.Setting;

public class Program
{
    public static void Main(string[] args)
    {
        // 加载应用配置
        using var setting = new Setting("app.setting");
        
        var builder = WebApplication.CreateBuilder(args);
        
        // 从配置初始化服务
        var connectionString = setting.GetByGroup("connection_string", "Database");
        builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseNpgsql(connectionString));
            
        var port = setting.GetInt("port", 8080);
        builder.WebHost.UseUrls($"http://*:{port}");
        
        var app = builder.Build();
        app.Run();
    }
}
```

### 场景 2: 多环境配置

```csharp
using WellTool.Setting;

public class ConfigManager : IDisposable
{
    private readonly Setting _setting;
    
    public ConfigManager(string environment)
    {
        // 根据环境加载不同配置
        var configFile = $"app.{environment}.setting";
        _setting = new Setting(configFile);
    }
    
    public string GetConnectionString()
    {
        return _setting.GetByGroup("connection_string", "Database");
    }
    
    public bool IsDebugMode()
    {
        return _setting.GetBool("debug", false);
    }
    
    public void Dispose()
    {
        _setting?.Dispose();
    }
}

// 使用示例
using var config = new ConfigManager("production");
var connStr = config.GetConnectionString();
```

### 场景 3: 动态配置更新

```csharp
using WellTool.Setting;

public class AppConfig
{
    private Setting _setting;
    private readonly string _configFile;
    
    public AppConfig(string configFile)
    {
        _configFile = configFile;
        LoadConfig();
    }
    
    private void LoadConfig()
    {
        _setting?.Dispose();
        _setting = new Setting(_configFile);
    }
    
    public void Reload()
    {
        LoadConfig();
    }
    
    public string GetValue(string key, string? group = null)
    {
        if (group == null)
            return _setting.GetStr(key);
        
        return _setting.GetByGroup(key, group);
    }
}

// 使用示例
var config = new AppConfig("app.setting");
var value = config.GetValue("key", "Group");

// 重新加载配置
config.Reload();
```

### 场景 4: 依赖注入集成

```csharp
using WellTool.Setting;
using Microsoft.Extensions.DependencyInjection;

// 注册配置服务
services.AddSingleton<IConfiguration>(provider =>
{
    var setting = new Setting("app.setting");
    return new SettingConfiguration(setting);
});

// 定义配置接口
public interface IConfiguration
{
    string GetString(string key, string? defaultValue = null);
    int GetInt(string key, int defaultValue);
    bool GetBool(string key, bool defaultValue);
}

// 实现配置类
public class SettingConfiguration : IConfiguration
{
    private readonly Setting _setting;
    
    public SettingConfiguration(Setting setting)
    {
        _setting = setting;
    }
    
    public string GetString(string key, string? defaultValue = null)
    {
        return _setting.GetStr(key, defaultValue ?? string.Empty);
    }
    
    public int GetInt(string key, int defaultValue)
    {
        return _setting.GetInt(key, defaultValue);
    }
    
    public bool GetBool(string key, bool defaultValue)
    {
        return _setting.GetBool(key, defaultValue);
    }
}

// 在控制器中使用
public class HomeController : Controller
{
    private readonly IConfiguration _config;
    
    public HomeController(IConfiguration config)
    {
        _config = config;
    }
    
    public IActionResult Index()
    {
        var appName = _config.GetString("app_name");
        var port = _config.GetInt("port", 8080);
        ViewBag.AppName = appName;
        return View();
    }
}
```

## 🔍 高级功能

### 变量替换

```ini
# 定义基础路径
base_path=/opt/myapp

# 使用变量
log_path=${base_path}/logs
data_path=${base_path}/data
config_path=${base_path}/config
```

C# 代码：

```csharp
using var setting = new Setting("app.setting");

// 自动替换变量
var logPath = setting.GetStr("log_path");
// 结果：/opt/myapp/logs
```

### 分组管理

```csharp
using var setting = new Setting("app.setting");

// 获取所有分组
var groups = setting.Groups();
foreach (var group in groups)
{
    Console.WriteLine($"Group: {group}");
}

// 检查是否包含某个键
if (setting.ContainsKey("key", "Database"))
{
    var value = setting.GetByGroup("key", "Database");
}

// 动态添加配置
setting.Put("new_key", "new_value", "NewGroup");

// 删除配置
setting.Remove("old_key", "OldGroup");
```

## ⚠️ 注意事项

### 1. 资源释放

Setting、Props 都实现了 IDisposable，使用后请释放：

```csharp
// 推荐：使用 using 语句
using var setting = new Setting("app.setting");

// 或者手动释放
var setting = new Setting("app.setting");
try
{
    // 使用配置
}
finally
{
    setting.Dispose();
}
```

### 2. 异常处理

```csharp
try
{
    var setting = new Setting("app.setting");
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"配置文件不存在：{ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"加载配置失败：{ex.Message}");
}
```

### 3. 线程安全

- ✅ GroupedMap 是线程安全的
- ✅ Setting 支持多线程读取
- ⚠️ 动态修改配置时需要自行处理并发

## 📖 更多资源

- [完整文档](README_COMPLETE.md)
- [Java 原版文档](hutool-setting/README.md)
- [YamlDotNet 文档](https://github.com/aaubry/YamlDotNet)

---

**最后更新**: 2026 年 3 月 24 日  
**维护者**: WellTool Team
