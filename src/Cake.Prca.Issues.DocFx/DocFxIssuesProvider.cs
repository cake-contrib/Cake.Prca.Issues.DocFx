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
                let
                    logEntryObject = JsonConvert.DeserializeObject<JToken>(logEntry)
                where
                    (string)logEntryObject.SelectToken("message_severity") == "warning"
                select
                    new CodeAnalysisIssue<DocFxIssuesProvider>(
                        (string)logEntryObject.SelectToken("file"),
                        null,
                        (string)logEntryObject.SelectToken("message"),
                        0,
                        (string)logEntryObject.SelectToken("source"));
        }
    }
}
