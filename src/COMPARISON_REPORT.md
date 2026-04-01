# Hutool 与 WellTool 模块对比报告

## 核心模块对比 (hutool-core vs WellTool.Core)

### 1. Util 工具类对比

#### Hutool 有但 WellTool 缺失的 Util 类

- **BooleanUtil** - 布尔类型工具类
- **ByteUtil** - 字节类型工具类
- **CharsetUtil** - 字符集工具类
- **CharUtil** - 字符工具类
- **ClassLoaderUtil** - ClassLoader 工具类
- **CoordinateUtil** - 坐标工具类
- **CreditCodeUtil** - 统一社会信用代码工具类
- **DesensitizedUtil** - 脱敏工具类
- **EnumUtil** - 枚举工具类
- **EscapeUtil** - 转义工具类
- **HashUtil** - Hash 工具类
- **HexUtil** - Hex 工具类
- **IdcardUtil** - 身份证工具类
- **IdUtil** - ID 生成工具类
- **JAXBUtil** - JAXB 工具类
- **JdkUtil** - JDK 工具类
- **JNDIUtil** - JNDI 工具类
- **ModifierUtil** - 反射工具类
- **ObjUtil** - 对象工具类
- **PageUtil** - 分页工具类
- **PhoneUtil** - 手机号工具类
- **PrimitiveArrayUtil** - 基本类型数组工具类
- **RadixUtil** - 进制转换工具类
- **ReferenceUtil** - 引用工具类
- **ReflectUtil** - 反射工具类
- **ReUtil** - 正则工具类
- **RuntimeUtil** - 运行时工具类
- **SerializeUtil** - 序列化工具类
- **ServiceLoaderUtil** - ServiceLoader 工具类
- **SystemPropsUtil** - 系统属性工具类
- **TypeUtil** - 类型工具类
- **URLUtil** - URL 工具类
- **VersionUtil** - 版本工具类
- **XmlUtil** - XML 工具类
- **ZipUtil** - ZIP 压缩工具类

#### WellTool 已有的 Util 类

- ArrayUtil ✓
- ClassUtil ✓
- CollectionUtil ✓
- DateUtil ✓
- IOUtil ✓
- IterUtil ✓
- NumberUtil ✓
- RandomUtil ✓
- StringUtil.cs / StrUtil ✓
- ThreadUtil ✓

### 2. Date 日期模块对比

#### Hutool 有但 WellTool 缺失的类

- **ChineseDate** - 农历日期
- **DateModifier** - 日期修改器
- **DateTime** - 日期时间对象（WellTool 有 DateTimeExt，但不完整）
- **LocalDateTimeUtil** - LocalDateTime 工具类
- **Quarter** - 季度枚举（可能有，需要确认）
- **StopWatch** - 秒表计时器
- **TemporalAccessorUtil** - TemporalAccessor 工具类
- **TemporalUtil** - 时间工具类
- **TimeInterval** - 时间间隔
- **TimeZoneUtil** - 时区工具类
- **Zodiac** - 生肖
- **ZoneUtil** - 时区工具类

#### WellTool 已有的 Date 类

- BetweenFormatter ✓
- Calendar (CalendarUtil) ✓
- DateBetween ✓
- DateException ✓
- DateField ✓
- DatePattern ✓
- DateUnit ✓
- DateUtil ✓
- Month ✓
- Quarter ✓
- StopWatch ✓
- Week ✓

### 3. Codec 编解码模块对比

#### Hutool 有但 WellTool 缺失的类

- **Base16Codec** - Base16 编解码
- **Decoder** - 解码器接口
- **Encoder** - 编码器接口

#### WellTool 已有的 Codec 类（基本完整）

- Base32 ✓
- Base32Codec ✓
- Base58 ✓
- Base58Codec ✓
- Base62 ✓
- Base62Codec ✓
- Base64 ✓
- Base64Decoder ✓
- Base64Encoder ✓
- BCD ✓
- Caesar ✓
- Hashids ✓
- Morse ✓
- PercentCodec ✓
- PunyCode ✓
- Rot ✓

### 4. Collection 集合模块对比

#### Hutool 有但 WellTool 缺失的类

- **ArrayIter** - 数组迭代器
- **AvgPartition** - 平均分区
- **CollStreamUtil** - 集合流工具
- **ComputeIter** - 计算迭代器
- **CopiedIter** - 复制迭代器
- **EnumerationIter** - 枚举迭代器
- **FilterIter** - 过滤迭代器
- **IterableIter** - Iterable 迭代器
- **IteratorEnumeration** - 迭代器枚举适配器
- **IterChain** - 迭代器链
- **LineIter** - 行迭代器
- **NodeListIter** - NodeList 迭代器
- **Partition** - 分区接口
- **PartitionIter** - 分区迭代器
- **RandomAccessAvgPartition** - 随机访问平均分区
- **RandomAccessPartition** - 随机访问分区
- **ResettableIter** - 可重置迭代器
- **SpliteratorUtil** - Spliterator 工具
- **TransCollection** - 转换集合
- **TransIter** - 转换迭代器
- **TransSpliterator** - 转换 Spliterator
- **UniqueKeySet** - 唯一键 Set

#### WellTool 已有的 Collection 类

- BoundedPriorityQueue ✓
- CollUtil ✓
- ConcurrentHashSet ✓
- IterUtil ✓
- ListUtil ✓

### 5. IO 模块对比

#### Hutool 有但 WellTool 缺失的类

- **AppendableWriter** - Appendable 写入器
- **BOMInputStream** - BOM 输入流
- **BomReader** - BOM 读取器
- **BufferUtil** - Buffer 工具类
- **CharsetDetector** - 字符集检测器
- **FastByteArrayOutputStream** - 快速字节数组输出流
- **FastByteBuffer** - 快速字节缓冲区
- **FastStringWriter** - 快速 StringWriter
- **FileMagicNumber** - 文件魔法数字
- **FileTypeUtil** - 文件类型工具类
- **LimitedInputStream** - 限制输入流
- **LineHandler** - 行处理器
- **ManifestUtil** - Manifest 工具类
- **NioUtil** - NIO 工具类
- **NullOutputStream** - 空输出流
- **StreamProgress** - 流进度接口
- **ValidateObjectInputStream** - 验证对象输入流

#### WellTool 已有的 IO 类

- FileAppender ✓
- FileReader ✓
- FileUtil ✓
- FileWriter ✓
- IORuntimeException ✓
- IoUtil ✓
- ResourceUtil ✓

### 6. Text 文本模块（完全缺失）

#### Hutool 有但 WellTool 完全缺失的类

- **AntPathMatcher** - Ant 路径匹配器
- **ASCIIStrCache** - ASCII 字符串缓存
- **CharPool** - 字符池
- **CharSequenceUtil** - CharSequence 工具类
- **NamingCase** - 命名风格转换
- **PasswdStrength** - 密码强度
- **Simhash** - _simhash_算法
- **StrBuilder** - 字符串构建器
- **StrFormatter** - 字符串格式化器
- **StrJoiner** - 字符串连接器
- **StrMatcher** - 字符串匹配器
- **StrPool** - 字符串池
- **StrSplitter** - 字符串分割器
- **TextSimilarity** - 文本相似度
- **UnicodeUtil** - Unicode 工具类

### 7. Lang 语言模块对比

#### Hutool 有但 WellTool 缺失的类

- **Assert** - 断言工具类
- **Chain** - 责任链
- **ClassScanner** - 类扫描器
- **ConsistentHash** - 一致性 Hash
- **ConsoleTable** - 控制台表格
- **DefaultSegment** - 默认分段
- **Editor** - 编辑器接口
- **EnumItem** - 枚举项
- **Filter** - 过滤器接口
- **JarClassLoader** - JAR 类加载器
- **Matcher** - 匹配器接口
- **ObjectId** - ObjectId 生成器
- **Opt** - 选项类（类似 Optional）
- **Pair** - 键值对
- **ParameterizedTypeImpl** - 参数化类型实现
- **PatternPool** - 正则池
- **Pid** - PID 生成器
- **Range** - 范围
- **RegexPool** - 正则池
- **Replacer** - 替换器接口
- **ResourceClassLoader** - 资源类加载器
- **Segment** - 分段接口
- **SimpleCache** - 简单缓存
- **Tuple** - 元组
- **TypeReference** - 类型引用
- **UUID** - UUID 生成器
- **Validator** - 验证器
- **Version** - 版本号类
- **WeightListRandom** - 权重列表随机
- **WeightRandom** - 权重随机

#### WellTool 已有的 Lang 类

- Assert ✓
- Console ✓
- Dict ✓
- ObjectUtil ✓
- Pair ✓
- Singleton ✓
- Snowflake ✓
- StringUtil ✓

### 8. Map 模块对比

#### Hutool 有但 WellTool 缺失的类

- **AbsEntry** - 抽象 Entry
- **BiMap** - 双向 Map（可能有，需要确认）
- **CamelCaseLinkedMap** - 驼峰式 LinkedMap
- **CamelCaseMap** - 驼峰式 Map
- **CaseInsensitiveLinkedMap** - 大小写不敏感 LinkedMap
- **CaseInsensitiveTreeMap** - 大小写不敏感 TreeMap
- **CustomKeyMap** - 自定义键 Map
- **FixedLinkedHashMap** - 固定大小 LinkedHashMap
- **ForestMap** - 森林 Map
- **FuncKeyMap** - 函数键 Map
- **FuncMap** - 函数 Map
- **LinkedForestMap** - 链接森林 Map
- **MapBuilder** - Map 构建器
- **MapProxy** - Map 代理
- **MapWrapper** - Map 包装器
- **ReferenceConcurrentMap** - 引用并发 Map
- **SafeConcurrentHashMap** - 安全并发 HashMap
- **TableMap** - 表 Map
- **TolerantMap** - 容错 Map
- **TransMap** - 转换 Map
- **TreeEntry** - 树 Entry
- **WeakConcurrentMap** - 弱引用并发 Map

#### WellTool 已有的 Map 类

- BiMap ✓
- CamelCaseMap ✓
- CaseInsensitiveMap ✓
- MapUtil ✓
- MapWrapper ✓

### 9. Net 网络模块（完全缺失）

#### Hutool 有但 WellTool 完全缺失的类

- **DefaultTrustManager** - 默认信任管理器
- **FormUrlencoded** - 表单 URL 编码
- **Ipv4Util** - IPv4 工具类
- **LocalPortGenerater** - 本地端口生成器
- **MaskBit** - 掩码位
- **NetUtil** - 网络工具类
- **PassAuth** - 密码认证
- **ProxySocketFactory** - 代理 Socket 工厂
- **RFC3986** - RFC3986 标准
- **SSLContextBuilder** - SSL 上下文构建器
- **SSLProtocols** - SSL 协议
- **SSLUtil** - SSL 工具类
- **URLDecoder** - URL 解码器
- **URLEncoder** - URL 编码器
- **URLEncodeUtil** - URL 编码工具类
- **UserPassAuthenticator** - 用户名密码认证器

### 10. Thread 线程模块对比

#### Hutool 有但 WellTool 缺失的类

- **AsyncUtil** - 异步工具类
- **BlockPolicy** - 阻塞策略
- **ConcurrencyTester** - 并发测试器
- **DelegatedExecutorService** - 委托执行器服务
- **ExecutorBuilder** - 执行器构建器
- **FinalizableDelegatedExecutorService** - 可终结委托执行器服务
- **GlobalThreadPool** - 全局线程池
- **NamedThreadFactory** - 命名线程工厂
- **RecyclableBatchThreadPoolExecutor** - 可回收批量线程池执行器
- **RejectPolicy** - 拒绝策略
- **SemaphoreRunnable** - 信号量 Runnable
- **SyncFinisher** - 同步完成器
- **ThreadException** - 线程异常
- **ThreadFactoryBuilder** - 线程工厂构建器

#### WellTool 已有的 Thread 类

- ThreadUtil ✓（在 Util 目录中）

### 11. Exceptions 异常模块对比

#### Hutool 有但 WellTool 缺失的类

- **CheckedUtil** - 检查异常工具类
- **DependencyException** - 依赖异常
- **InvocationTargetRuntimeException** - 调用目标运行时异常
- **NotInitedException** - 未初始化异常
- **StatefulException** - 状态异常
- **ValidateException** - 验证异常

#### WellTool 已有的 Exceptions 类

- BaseException ✓
- ExceptionUtil ✓
- UtilException ✓

### 12. 其他缺失的模块

#### Hutool 有但 WellTool 完全缺失的包

- **annotation** - 注解模块
- **bean** - Bean 操作模块
- **builder** - 构建器模式模块
- **clone** - 克隆模块
- **comparator** - 比较器模块
- **compiler** - 编译器模块
- **compress** - 压缩模块
- **convert** - 转换器模块
- **getter** - Getter 模块
- **img** - 图片处理模块
- **math** - 数学运算模块
- **stream** - 流模块
- **swing** - Swing 模块（.NET 不需要）

## 其他主要模块对比

### hutool-aop vs WellTool.Aop

需要进一步对比具体类

### hutool-bloomFilter vs WellTool.BloomFilter

需要进一步对比具体类

### hutool-cache vs WellTool.Cache

需要进一步对比具体类

### hutool-captcha vs WellTool.Captcha

需要进一步对比具体类

### hutool-cron vs WellTool.Cron

需要进一步对比具体类

### hutool-crypto vs WellTool.Crypto

需要进一步对比具体类

### hutool-db vs WellTool.DB

需要进一步对比具体类

### hutool-dfa vs WellTool.Dfa

需要进一步对比具体类

### hutool-extra vs WellTool.Extra

需要进一步对比具体类

### hutool-http vs WellTool.Http

需要进一步对比具体类

### hutool-json vs WellTool.Json

需要进一步对比具体类

### hutool-jwt vs WellTool.Jwt

需要进一步对比具体类

### hutool-log vs WellTool.Log

需要进一步对比具体类

### hutool-poi vs WellTool.Poi

需要进一步对比具体类

### hutool-script vs WellTool.Script

需要进一步对比具体类

### hutool-setting vs WellTool.Setting

需要进一步对比具体类

### hutool-socket vs WellTool.Socket

需要进一步对比具体类

### hutool-system vs WellTool.System

需要进一步对比具体类

## 总结

根据以上对比，WellTool.Core 模块主要缺失以下内容：

### 高优先级缺失（建议优先补充）

1. **Text 文本模块** - 完全缺失，包括文本处理、字符串处理等相关工具
2. **Net 网络模块** - 完全缺失，包括网络工具、SSL 工具等
3. **Thread 线程模块** - 大部分缺失，只有 ThreadUtil
4. **Exceptions 异常模块** - 缺少多种异常类型
5. **Lang 语言模块** - 缺少大量工具类，如 ConsistentHash、Validator、UUID 等
6. **IO 模块** - 缺少多种流处理和文件处理工具类
7. **Collection 集合模块** - 缺少多种迭代器和集合工具
8. **Util 工具类** - 缺少大量常用工具类，如 BooleanUtil、EnumUtil、ReflectUtil 等

### 中等优先级缺失

1. **Map 模块** - 缺少多种特殊 Map 实现
2. **Date 模块** - 缺少农历、时区等相关工具

### 低优先级缺失

1. **Codec 模块** - 已基本完整，只缺少个别编解码器
2. **swing 模块** - .NET 平台不需要

## 建议

1. **优先补充核心工具类**：BooleanUtil、CharsetUtil、EnumUtil、ReflectUtil、XmlUtil 等
2. **补充 Text 模块**：这是非常重要的文本处理模块
3. **补充 Net 模块**：网络工具在实际开发中使用频率很高
4. **完善 Thread 模块**：补充线程池、异步工具等
5. **完善 Lang 模块**：补充 Validator、UUID、ConsistentHash 等常用工具
6. **完善 IO 模块**：补充各种流处理工具
7. **完善 Collection 模块**：补充各种迭代器和集合工具
