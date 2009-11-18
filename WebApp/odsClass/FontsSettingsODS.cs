using System;
using System.Web;
using System.Web.UI.WebControls;

namespace SEOToolSet.WebApp.odsClass
{
    public class FontsSettingsODS
    {
        private const string _tokenFontSettings = "FontSettings";
        public static FontsSettings GetFontSettingsFromSession()
        {
            return (FontsSettings)HttpContext.Current.Session[_tokenFontSettings];
        }

        public static FontsSettings GetFontSettings()
        {
            var fontSettings = GetFontSettingsFromSession() ?? PutInSession(new FontsSettings());
            return fontSettings;
        }

        public static void UpdateFontSettings(String FontFamily,
                                              Int32 HeaderFontSize,
                                              Int32 BodyFontSize,
                                              Int32 FooterFontSize,
                                              Boolean UsePixels,
                                              Boolean HeaderFixed)
        {
            var ce = GetFontSettingsFromSession();
            if (ce == null) return;
            ce.FontFamily = FontFamily;
            ce.HeaderFontSize = HeaderFontSize;
            ce.BodyFontSize = BodyFontSize;
            ce.FooterFontSize = FooterFontSize;
            ce.UsePixels = UsePixels;
            ce.HeaderFixed = HeaderFixed;
        }



        private static FontsSettings PutInSession(FontsSettings fontSettings)
        {
            HttpContext.Current.Session[_tokenFontSettings] = fontSettings;
            return fontSettings;
        }

        public static ListItemCollection GetFontFamilies()
        {
            var col = new ListItemCollection
                          {
                              new ListItem("Arial", "Arial"),
                              new ListItem("Verdana", "Verdana"),
                              new ListItem("Lucida Sans", "Lucida Sans"),
                              new ListItem("Calibri", "Calibri"),
                              new ListItem("Trebuchet MS", "Trebuchet MS")
                          };
            return col;
        }

        public static ListItemCollection FontSizes()
        {
            var col = new ListItemCollection
                          {
                              new ListItem("10", "10"),
                              new ListItem("11", "11"),
                              new ListItem("12", "12"),
                              new ListItem("13", "13"),
                              new ListItem("14", "14"),
                              new ListItem("15", "15"),
                              new ListItem("16", "16"),
                              new ListItem("17", "17"),
                              new ListItem("18", "18"),
                              new ListItem("19", "19"),
                              new ListItem("20", "20")
                          };
            return col;
        }

        public static Double getPercentageAmountFromPixels(Int32 fontSize)
        {
            Double val = 0;
            switch (fontSize)
            {
                case 10: val = 77; break;
                case 11: val = 85; break;
                case 12: val = 93; break;
                case 13: val = 100; break;
                case 14: val = 108; break;
                case 15: val = 116; break;
                case 16: val = 123.1; break;
                case 17: val = 131; break;
                case 18: val = 138.5; break;
                case 19: val = 146.5; break;
                case 20: val = 153.9; break;
            }

            return val;
        }

        public static void removeFontSettingsFromSession()
        {
            HttpContext.Current.Session[_tokenFontSettings] = null;
        }
    }
}