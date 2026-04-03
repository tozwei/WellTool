namespace WellTool.AI.Model.OpenAI;

/// <summary>
/// OpenAI公共类
/// </summary>
public static class OpenaiCommon
{
    /// <summary>
    /// OpenAI推理参数
    /// </summary>
    public enum OpenaiReasoning
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// OpenAI视觉参数
    /// </summary>
    public enum OpenaiVision
    {
        Auto,
        Low,
        High
    }

    /// <summary>
    /// OpenAI音频参数
    /// </summary>
    public enum OpenaiSpeech
    {
        Alloy,
        Ash,
        Coral,
        Echo,
        Fable,
        Onyx,
        Nova,
        Sage,
        Shimmer
    }
}
