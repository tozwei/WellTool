namespace WellTool.Poi.Word
{
    public class FontInfo
    {
        public string FontFamilyName { get; set; }
        
        public float Size { get; set; } = 12;
        
        public bool Bold { get; set; }
        
        public bool Italic { get; set; }

        public static FontInfo Default => new FontInfo
        {
            FontFamilyName = "宋体",
            Size = 12,
            Bold = false,
            Italic = false
        };
    }
}