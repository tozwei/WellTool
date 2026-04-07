// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Xunit;

namespace WellTool.Poi.Tests;

/// <summary>
/// 测试集合初始化器，确保EPPlus许可证在所有测试前被设置
/// </summary>
public class EPPlusCollectionFixture
{
    public EPPlusCollectionFixture()
    {
        // 触发Poi项目的静态构造函数
        WellTool.Poi.EPPlusInitializer.Initialize();
    }
}

[CollectionDefinition("EPPlusCollection")]
public class EPPlusCollection : ICollectionFixture<EPPlusCollectionFixture>
{
}