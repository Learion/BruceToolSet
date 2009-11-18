namespace Eucalypto
{
    /// <summary>
    /// Enum to define the backup mode used for wiki articles.
    /// </summary>
    public enum WikiBackupMode
    {
        /// <summary>
        /// Backup always
        /// </summary>
        Always = 0,
        /// <summary>
        /// Backup only if requested
        /// </summary>
        Request = 1,
        /// <summary>
        /// Backup disabled
        /// </summary>
        Never = 2
    }
}