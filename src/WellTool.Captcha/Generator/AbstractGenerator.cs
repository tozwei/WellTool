namespace WellTool.Captcha.Generator
{
    public abstract class AbstractGenerator : CodeGenerator
    {
        protected readonly string BaseStr;
        protected readonly int Length;

        public AbstractGenerator(int count) : this("abcdefghjkmnpqrstuvwxyzABCDEFGH JKMNPQRSTUVWXYZ23456789", count)
        {
        }

        public AbstractGenerator(string baseStr, int length)
        {
            BaseStr = baseStr;
            Length = length;
        }

        public int GetLength()
        {
            return Length;
        }

        public abstract string Generate();

        public abstract bool Verify(string code, string userInputCode);
    }
}
