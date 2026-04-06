// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace WellTool.Extra;

/// <summary>
/// 额外工具类
/// </summary>
public class ExtraUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static ExtraUtil Instance { get; } = new ExtraUtil();

    /// <summary>
    /// 获取压缩工具
    /// </summary>
    /// <returns>压缩工具</returns>
    public CompressUtil Compress()
    {
        return CompressUtil.Instance;
    }

    /// <summary>
    /// 获取表情符号工具
    /// </summary>
    /// <returns>表情符号工具</returns>
    public EmojiUtil Emoji()
    {
        return EmojiUtil.Instance;
    }

    /// <summary>
    /// 获取表达式工具
    /// </summary>
    /// <returns>表达式工具</returns>
    public ExpressionUtil Expression()
    {
        return ExpressionUtil.Instance;
    }

    /// <summary>
    /// 获取FTP工具
    /// </summary>
    /// <returns>FTP工具</returns>
    public FtpUtil Ftp()
    {
        return FtpUtil.Instance;
    }

    /// <summary>
    /// 获取邮件工具
    /// </summary>
    /// <returns>邮件工具</returns>
    public MailUtil Mail()
    {
        return MailUtil.Instance;
    }

    /// <summary>
    /// 获取拼音工具
    /// </summary>
    /// <returns>拼音工具</returns>
    public PinyinUtil Pinyin()
    {
        return PinyinUtil.Instance;
    }

    /// <summary>
    /// 获取二维码工具
    /// </summary>
    /// <returns>二维码工具（静态类返回null）</returns>
    public object QrCode()
    {
        // 静态类没有实例
        return null;
    }

    /// <summary>
    /// 获取SSH工具
    /// </summary>
    /// <returns>SSH工具</returns>
    public SshUtil Ssh()
    {
        return SshUtil.Instance;
    }

    /// <summary>
    /// 获取模板工具
    /// </summary>
    /// <returns>模板工具</returns>
    public TemplateUtil Template()
    {
        return TemplateUtil.Instance;
    }

    /// <summary>
    /// 获取分词工具
    /// </summary>
    /// <returns>分词工具</returns>
    public TokenizerUtil Tokenizer()
    {
        return TokenizerUtil.Instance;
    }

    /// <summary>
    /// 获取验证工具
    /// </summary>
    /// <returns>验证工具</returns>
    public object Validation()
    {
        return null;
    }
}