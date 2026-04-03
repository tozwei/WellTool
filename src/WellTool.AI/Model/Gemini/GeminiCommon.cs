namespace WellTool.AI.Model.Gemini;

/// <summary>
/// Gemini公共类
/// </summary>
public static class GeminiCommon
{
    /// <summary>
    /// 要生成的图片数量
    /// </summary>
    public enum GeminiImageCount
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }

    /// <summary>
    /// 生成的图片大小
    /// </summary>
    public enum GeminiImageSize
    {
        Size1K,
        Size2K
    }

    /// <summary>
    /// 宽高比
    /// </summary>
    public enum GeminiAspectRatio
    {
        Square,
        Portrait3_4,
        Landscape4_3,
        Portrait9_16,
        Landscape16_9
    }

    /// <summary>
    /// 人物生成权限
    /// </summary>
    public enum GeminiPersonGeneration
    {
        DontAllow,
        AllowAdult,
        AllowAll
    }

    /// <summary>
    /// 生成的视频的时长(秒)
    /// </summary>
    public enum GeminiDurationSeconds
    {
        Four = 4,
        Six = 6,
        Eight = 8
    }

    /// <summary>
    /// 语音音色
    /// </summary>
    public enum GeminiVoice
    {
        Aoede,
        Charon,
        Kore,
        Fenrir,
        Puck
    }
}
