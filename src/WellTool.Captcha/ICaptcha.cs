namespace WellTool.Captcha
{
    public interface ICaptcha
    {
        void CreateCode();

        string GetCode();

        bool Verify(string userInputCode);

        void Write(System.IO.Stream outStream);
    }
}
