namespace WellTool.Captcha.Generator
{
    public interface CodeGenerator
    {
        string Generate();

        bool Verify(string code, string userInputCode);
    }
}
