using System;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;

namespace SEOToolSet.WebApp.FlyingStyles
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FontsSettings : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/css";

            var fontSettings = odsClass.FontsSettingsODS.GetFontSettingsFromSession();
            var cssRules = "/*No rules*/";

            if (fontSettings != null)
            {
                cssRules = SetFlyingCssValues(fontSettings);
            }

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.Write(cssRules);

        }

        private static String SetFlyingCssValues(odsClass.FontsSettings settings)
        {
            var rules =
@"body { font-family: [FONT_FAMILY] !important; }
#hd { font-size: [HEADER_FONT_SIZE][UNITS_TOKEN]; }
#bd { font-size: [BODY_FONT_SIZE][UNITS_TOKEN]; }
#ft { font-size: [FOOTER_FONT_SIZE][UNITS_TOKEN]; }
[RULES_FOR_KEEP_HEADER_AT_TOP]
";


            rules = rules.Replace("[FONT_FAMILY]", settings.FontFamily);
            rules = rules.Replace("[HEADER_FONT_SIZE]", settings.UsePixels ? settings.HeaderFontSize.ToString() : odsClass.FontsSettingsODS.getPercentageAmountFromPixels(settings.HeaderFontSize).ToString());
            rules = rules.Replace("[BODY_FONT_SIZE]", settings.UsePixels ? settings.BodyFontSize.ToString() : odsClass.FontsSettingsODS.getPercentageAmountFromPixels(settings.BodyFontSize).ToString());
            rules = rules.Replace("[FOOTER_FONT_SIZE]", settings.UsePixels ? settings.FooterFontSize.ToString() : odsClass.FontsSettingsODS.getPercentageAmountFromPixels(settings.FooterFontSize).ToString());
            rules = rules.Replace("[UNITS_TOKEN]", settings.UsePixels ? "px" : "%");

            rules = rules.Replace("[RULES_FOR_KEEP_HEADER_AT_TOP]", settings.HeaderFixed ? GetHeaderFixedSetOfRules() :
                                  String.Empty);

            return rules;
        }

        private static string GetHeaderFixedSetOfRules()
        {
            return @"#hd { position:fixed;top:0;width:100%;} 
                     #bd .wrapper { padding: 143px 20px 100px; }
                     #ft {left:0;position:fixed;width:100%;bottom:0;}                     
                    ";
        }


        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
