namespace Cake.Prca.Issues.DocFx
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="DocFxIssuesProvider"/>.
    /// </summary>
    public class DocFxIssuesSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the DocFx log file.</param>
        protected DocFxIssuesSettings(FilePath logFilePath)
        {
            logFilePath.NotNull(nameof(logFilePath));

            using (var stream = new FileStream(logFilePath.FullPath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.LogFileContent = sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the the DocFx log file.</param>
        protected DocFxIssuesSettings(string logFileContent)
        {
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));

            this.LogFileContent = logFileContent;
        }

        /// <summary>
        /// Gets the content of the log file.
        /// </summary>
        public string LogFileContent { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="DocFxIssuesSettings"/> class from a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the DocFx log file.</param>
        /// <returns>Instance of the <see cref="DocFxIssuesSettings"/> class.</returns>
        public static DocFxIssuesSettings FromFilePath(FilePath logFilePath)
        {
            return new DocFxIssuesSettings(logFilePath);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="DocFxIssuesSettings"/> class from the content
        /// of a DocFx log file.
        /// </summary>
        /// <param name="logFileContent">Content of the DocFx log file.</param>
        /// <returns>Instance of the <see cref="DocFxIssuesSettings"/> class.</returns>
        public static DocFxIssuesSettings FromContent(string logFileContent)
        {
            return new DocFxIssuesSettings(logFileContent);
        }
    }
}
