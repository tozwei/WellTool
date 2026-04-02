using WellTool.Extra.Spring;

namespace WellTool.Extra.Tests;

/// <summary>
/// SpringUtil 测试类
/// </summary>
public class SpringUtilTest
{
    [Fact]
    public void TestSetApplicationContext_SetsContext()
    {
        // 测试设置应用上下文
        var mockContext = new MockApplicationContext();
        
        SpringUtil.SetApplicationContext(mockContext);
        
        Assert.NotNull(SpringUtil.GetApplicationContext());
    }

    [Fact]
    public void TestGetBean_ByName_ReturnsBean()
    {
        // 测试通过名称获取 Bean
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var bean = SpringUtil.GetBean<string>("testBean");
        
        Assert.Equal("testValue", bean);
    }

    [Fact]
    public void TestGetBean_ByType_ReturnsBean()
    {
        // 测试通过类型获取 Bean
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var bean = SpringUtil.GetBean<string>();
        
        Assert.Equal("testValue", bean);
    }

    [Fact]
    public void TestGetBean_ByNameAndType_ReturnsBean()
    {
        // 测试通过名称和类型获取 Bean
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var bean = SpringUtil.GetBean<string>("testBean", typeof(string));
        
        Assert.Equal("testValue", bean);
    }

    [Fact]
    public void TestGetBeansOfType_ReturnsAllBeans()
    {
        // 测试获取指定类型的所有 Bean
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var beans = SpringUtil.GetBeansOfType<string>();
        
        Assert.NotNull(beans);
        Assert.NotEmpty(beans);
    }

    [Fact]
    public void TestGetBeanNamesForType_ReturnsBeanNames()
    {
        // 测试获取指定类型的 Bean 名称
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var names = SpringUtil.GetBeanNamesForType(typeof(string));
        
        Assert.NotNull(names);
        Assert.NotEmpty(names);
    }

    [Fact]
    public void TestGetProperty_WithKey_ReturnsValue()
    {
        // 测试获取属性值
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var value = SpringUtil.GetProperty("test.key");
        
        Assert.Equal("testValue", value);
    }

    [Fact]
    public void TestGetProperty_WithKeyAndDefault_ReturnsValue()
    {
        // 测试获取属性值（带默认值）
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var value = SpringUtil.GetProperty("test.key", "default");
        
        Assert.Equal("testValue", value);
    }

    [Fact]
    public void TestGetProperty_WithDefault_ReturnsDefaultWhenKeyNotFound()
    {
        // 测试获取不存在的属性时返回默认值
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var value = SpringUtil.GetProperty("nonexistent.key", "default");
        
        Assert.Equal("default", value);
    }

    [Fact]
    public void TestGetProperty_Generic_WithDefault_ReturnsTypedValue()
    {
        // 测试泛型获取属性值
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var value = SpringUtil.GetProperty("test.int.key", 0);
        
        Assert.Equal(42, value);
    }

    [Fact]
    public void TestGetApplicationName_ReturnsName()
    {
        // 测试获取应用程序名称
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var name = SpringUtil.GetApplicationName();
        
        Assert.Equal("test-app", name);
    }

    [Fact]
    public void TestGetActiveProfiles_ReturnsProfiles()
    {
        // 测试获取活动配置文件
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var profiles = SpringUtil.GetActiveProfiles();
        
        Assert.NotNull(profiles);
        Assert.Contains("dev", profiles);
    }

    [Fact]
    public void TestGetActiveProfile_ReturnsFirstProfile()
    {
        // 测试获取第一个活动配置文件
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        var profile = SpringUtil.GetActiveProfile();
        
        Assert.Equal("dev", profile);
    }

    [Fact]
    public void TestRegisterBean_ValidBean_RegistersSuccessfully()
    {
        // 测试注册 Bean
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        // 不应该抛出异常
        var exception = Record.Exception(() => SpringUtil.RegisterBean("newBean", "newValue"));
        
        Assert.Null(exception);
    }

    [Fact]
    public void TestUnregisterBean_ValidBean_UnregistersSuccessfully()
    {
        // 测试注销 Bean
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        // 不应该抛出异常
        var exception = Record.Exception(() => SpringUtil.UnregisterBean("testBean"));
        
        Assert.Null(exception);
    }

    [Fact]
    public void TestPublishEvent_ValidEvent_PublishesSuccessfully()
    {
        // 测试发布事件
        var mockContext = new MockApplicationContext();
        SpringUtil.SetApplicationContext(mockContext);
        
        // 不应该抛出异常
        var exception = Record.Exception(() => SpringUtil.PublishEvent(new TestEvent()));
        
        Assert.Null(exception);
    }

    [Fact]
    public void TestGetBean_NoApplicationContext_ThrowsException()
    {
        // 测试未设置应用上下文时获取 Bean
        SpringUtil.SetApplicationContext(null);
        
        Assert.Throws<Exception>(() => SpringUtil.GetBean<string>());
    }

    [Fact]
    public void TestGetProperty_NoApplicationContext_ReturnsNull()
    {
        // 测试未设置应用上下文时获取属性
        SpringUtil.SetApplicationContext(null);
        
        var value = SpringUtil.GetProperty("test.key");
        
        Assert.Null(value);
    }

    /// <summary>
    /// 测试事件
    /// </summary>
    private class TestEvent
    {
        public string Message { get; set; } = "Test";
    }

    /// <summary>
    /// 模拟应用上下文
    /// </summary>
    private class MockApplicationContext : IApplicationContext
    {
        public T GetBean<T>(string name)
        {
            return (T)(object)"testValue";
        }

        public T GetBean<T>()
        {
            return (T)(object)"testValue";
        }

        public T GetBean<T>(params object[] args)
        {
            return (T)(object)"testValue";
        }

        public object GetBean(string name, Type type)
        {
            return "testValue";
        }

        public IDictionary<string, T> GetBeansOfType<T>()
        {
            return new Dictionary<string, T>
            {
                { "testBean", (T)(object)"testValue" }
            };
        }

        public string[] GetBeanNamesForType(Type type)
        {
            return new[] { "testBean" };
        }

        public string GetProperty(string key)
        {
            if (key == "test.key")
                return "testValue";
            if (key == "test.int.key")
                return "42";
            return null;
        }

        public string GetProperty(string key, string defaultValue)
        {
            return GetProperty(key) ?? defaultValue;
        }

        public T GetProperty<T>(string key, T defaultValue)
        {
            if (key == "test.key")
                return (T)(object)"testValue";
            if (key == "test.int.key")
                return (T)(object)42;
            return defaultValue;
        }

        public string[] GetActiveProfiles()
        {
            return new[] { "dev", "test" };
        }

        public void RegisterBean<T>(string beanName, T bean)
        {
            // 模拟注册
        }

        public void UnregisterBean(string beanName)
        {
            // 模拟注销
        }

        public void PublishEvent(object @event)
        {
            // 模拟发布事件
        }
    }
}
