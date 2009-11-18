using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;

namespace Eucalypto
{
    /// <summary>
    /// Class to format and validate an XHTML valid snippet of text.
    /// </summary>
    public class XHTMLText
    {
        #region Helper methods
        /// <summary>
        /// Convert a plain text to an XHTML valid text.
        /// </summary>
        public static string FromPlainText(string plainText, PlainTextMode mode)
        {
            if (mode == PlainTextMode.CSSPlainText)
            {
                string htmlEncoded = System.Web.HttpUtility.HtmlEncode(plainText);

                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append("<div class=\"plainText\">");
                builder.Append(htmlEncoded);
                builder.Append("</div>");

                return builder.ToString();
            }
            else if (mode == PlainTextMode.XHtmlConversion)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                using (System.IO.StringReader reader = new System.IO.StringReader(plainText))
                {
                    string line;
                    while ( (line = reader.ReadLine()) != null )
                    {
                        if (line != null && line.Length > 0)
                        {
                            //Replace the space and tab characters at the begind of the line with a nont breaking space
                            for (int col = 0; col < line.Length; col++)
                            {
                                if (line[col] == ' ')
                                {
                                    builder.Append("&#160;");
                                }
                                else if (line[col] == '\t')
                                {
                                    builder.Append("&#160;");
                                }
                                else
                                {
                                    string subLine = System.Web.HttpUtility.HtmlEncode(line.Substring(col));

                                    builder.Append(subLine);

                                    break;
                                }
                            }
                        }

                        builder.AppendLine("<br />");
                    }
                }

                return builder.ToString();
            }
            else
                throw new EucalyptoException("Mode not valid");
        }
        #endregion

        private System.Xml.XmlDocument mDoc = new System.Xml.XmlDocument();
        /// <summary>
        /// Constructor
        /// </summary>
        public XHTMLText()
        {
        }

        /// <summary>
        /// Load an xhtml snippet.
        /// </summary>
        /// <param name="xhtml"></param>
        public void Load(string xhtml)
        {
            mDoc = CreateXHTMLDoc(xhtml);
        }

        private System.Xml.XmlDocument CreateXHTMLDoc(string xml)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("<div>");
            builder.Append(xml);
            builder.Append("</div>");

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(builder.ToString());

            return doc;
        }

        /// <summary>
        /// Check if the html is a valid text using the XML document validator.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="error"></param>
        public bool IsValid(XHtmlMode mode, out Exception error)
        {
            try
            {
                CheckElements(mDoc, mode);

                error = null;
                return true;
            }
            catch (Exception ex)
            {
                error = ex;
                return false;
            }
        }

        private static string[] VALID_TAGS;
        private static string[] INVALID_TAGS;

        private void CheckStrictValidation(System.Xml.XmlElement element)
        {
            if (VALID_TAGS == null)
            {
                VALID_TAGS = new string[] { "a", "b", "p", "pre", "img", "i", "br", "sub", "sup",
                                            "cite", "strong", "dfn", "em", "kbd", "blockquote", "address", 
                                            "div", "span", 
                                            "h1", "h2", "h3", "h4", "h5", "h6",
                                            "big", "small",
                                            "ul", "li", "ol",
                                            "code", "samp", 
                                            "table", "td", "tbody", "tr", "th", "thead", "tfoot", "caption", "colgroup",  
                                            "label", "hr", 
                                            "map", "area",
                                            "form", "input", "button", "fieldset", "select", "option", "optgroup", "legend", "textarea", 
                                            "object", "param", 
                                            "iframe"};
                Array.Sort(VALID_TAGS);
            }

            string tag = element.Name.ToLowerInvariant();
            int index = Array.BinarySearch(VALID_TAGS, tag);
            if (index < 0)
                throw new TagInvalidException(element.Name);

            ////Check for style attribute
            //foreach (System.Xml.XmlAttribute attribute in element.Attributes)
            //{
            //    if (string.Equals(attribute.Name, "style", StringComparison.InvariantCultureIgnoreCase))
            //        throw new TagAttributeInvalidException(attribute.Name);
            //}
        }

        private void CheckBasicValidation(System.Xml.XmlElement element)
        {
            if (INVALID_TAGS == null)
            {
                INVALID_TAGS = new string[] { "html", "body", "head", "script" };
                Array.Sort(INVALID_TAGS);
            }

            string tag = element.Name.ToLowerInvariant();
            int index = Array.BinarySearch(INVALID_TAGS, tag);
            if (index >= 0)
                throw new TagInvalidException(element.Name);
        }

        private void CheckElements(System.Xml.XmlDocument doc, XHtmlMode mode)
        {
            if (mode == XHtmlMode.None)
                return;

            //Select all the nodes
            System.Xml.XmlNodeList list = doc.SelectNodes("//*");
            foreach (System.Xml.XmlElement element in list)
            {
                if (mode == XHtmlMode.StrictValidation)
                    CheckStrictValidation(element);
                else if (mode == XHtmlMode.BasicValidation)
                    CheckBasicValidation(element);
                else
                    throw new ArgumentException("XHtmlMode not supported", "mode");
            }
        }

        /// <summary>
        /// Automatically create the table of contents, insert the ID attribute for the heading tag if not found
        /// </summary>
        public string GenerateTOC()
        {
            return HTMLHeadingParser.GenerateTOC(mDoc);
        }

        /// <summary>
        /// Search for a div element with the TOC id and insert inside this element the TOC content.
        /// Returns true if the TOC element is presend otherwise false
        /// </summary>
        public bool InsertTOC(string TOC)
        {
            System.Xml.XmlNode tocNode = mDoc.SelectSingleNode("//div[@id='TOC']");

            if (tocNode != null)
            {
                tocNode.InnerXml = TOC;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Replace the links searching for any anchor (a) or image (img) element.
        /// The delegate (callback method) passed is used to generate the new link based on the previous value.
        /// </summary>
        public void ReplaceLinks(ReplaceLinkHandler replaceMethod)
        {
            System.Xml.XmlNodeList anchors = mDoc.SelectNodes("//a");
            System.Xml.XmlNodeList images = mDoc.SelectNodes("//img");

            foreach (System.Xml.XmlElement element in anchors)
            {
                string href = element.GetAttribute("href");

                string newUrl;
                replaceMethod(href, out newUrl);
                element.SetAttribute("href", newUrl);
            }

            foreach (System.Xml.XmlElement element in images)
            {
                string href = element.GetAttribute("src");

                string newUrl;
                replaceMethod(href, out newUrl);
                element.SetAttribute("src", newUrl);
            }
        }

        /// <summary>
        /// Delegate method used to replace the links
        /// </summary>
        /// <param name="oldUrl"></param>
        /// <param name="newUrl"></param>
        public delegate void ReplaceLinkHandler(string oldUrl, out string newUrl);

        /// <summary>
        /// Get a short text of the current xhtml
        /// </summary>
        /// <returns></returns>
        public string GetShortText()
        {
            string shortText = RenderText();

            //Take only the first 100 characters
            if (shortText.Length > 100)
                shortText = shortText.Substring(0, 100);

            return shortText;
        }

        /// <summary>
        /// Return the text without xhtml tags
        /// </summary>
        /// <returns></returns>
        public string RenderText()
        {
            return mDoc.InnerText;
        }

        /// <summary>
        /// Return the text complete with xhtml tags
        /// </summary>
        /// <returns></returns>
        public string RenderHTML()
        {
            return mDoc.InnerXml;
        }

        /// <summary>
        /// Return the xhtml completed with the tags and well indented
        /// </summary>
        /// <returns></returns>
        public string GetXhtml()
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(stream, System.Text.Encoding.UTF8);
                writer.Formatting = System.Xml.Formatting.Indented;
                mDoc.DocumentElement.WriteContentTo(writer);
                writer.Flush();

                stream.Seek(0, System.IO.SeekOrigin.Begin);
                System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

                return reader.ReadToEnd();
            }
        }
    }
}