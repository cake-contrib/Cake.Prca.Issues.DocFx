namespace Cake.Prca.Issues.DocFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Diagnostics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Provider for warnings reported by DocFx.
    /// </summary>
    internal class DocFxIssuesProvider : CodeAnalysisProvider
    {
        private readonly DocFxIssuesSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public DocFxIssuesProvider(ICakeLog log, DocFxIssuesSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;
        }

        /// <inheritdoc />
        protected override IEnumerable<ICodeAnalysisIssue> InternalReadIssues(PrcaCommentFormat format)
        {
            return
                from logEntry in this.settings.LogFileContent.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries).Select(x => "{" + x + "}")
                let logEntryObject = JsonConvert.DeserializeObject<JToken>(logEntry)
                let severity = (string)logEntryObject.SelectToken("message_severity")
                let file = (string)logEntryObject.SelectToken("file")
                let message = (string)logEntryObject.SelectToken("message")
                let source = (string)logEntryObject.SelectToken("source") ?? "DocFx"
                where
                    severity == "warning" &&
                    !string.IsNullOrWhiteSpace(message)
                select
                    new CodeAnalysisIssue<DocFxIssuesProvider>(
                        file,
                        null,
                        message,
                        0,
                        source);
        }
    }
}
