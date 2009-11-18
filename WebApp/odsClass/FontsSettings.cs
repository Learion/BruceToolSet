using System;

namespace SEOToolSet.WebApp.odsClass
{
    public class FontsSettings
    {
        public String FontFamily { get; set; }
        public Int32 HeaderFontSize { get; set; }
        public Int32 BodyFontSize { get; set; }
        public Int32 FooterFontSize { get; set; }
        public Boolean UsePixels { get; set; }
        public Boolean HeaderFixed { get; set; }

        public FontsSettings()
        {
            FontFamily = "Arial";
            HeaderFontSize = 13;
            BodyFontSize = 12;
            FooterFontSize = 11;
            UsePixels = false;
            HeaderFixed = false;
        }
    }
}
