
namespace Eucalypto
{
    /// <summary>
    /// Enum to define the validation mode for the XHTML snippets
    /// </summary>
    public enum XHtmlMode
    {
        /// <summary>
        /// No validation
        /// </summary>
        None = 0,
        /// <summary>
        /// Basic validation only. Check if not supported tags are present (like body, html, head, script, ...). See the XHTMLText for the full list.
        /// </summary>
        BasicValidation = 1,
        /// <summary>
        /// Strict validation. Only permits certain tags (p, a, br, table, h1, h2, ...). See the XHTMLText for the full list.
        /// </summary>
        StrictValidation = 2
    }
}
