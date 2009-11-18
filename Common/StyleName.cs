namespace SEOToolSet.Common
{
    /// <summary>
    /// Stores the names of classes used in the reports (they must have the same values as the names of tokens used in the ReportsFacade)
    /// </summary>
    public static class StyleName
    {
        /// <summary>
        /// Represents the parameter used for indicate the class name for highlight a keyword in a report
        /// </summary>
        public const string Keyword = "Keyword";

        /// <summary>
        /// Represents the parameter used for indicate the class name for highlight a optimized word in a report
        /// </summary>
        public const string OptimizedWord = "OptimizedWord";

        /// <summary>
        /// Represents the parameter used for indicate the class name for the failed result in a report
        /// </summary>
        public const string ResultError = "ResultError";

        /// <summary>
        /// Represents the parameter used for indicate the class name for the successful result in a report
        /// </summary>
        public const string ResultSuccess = "ResultSuccess";
    }
}