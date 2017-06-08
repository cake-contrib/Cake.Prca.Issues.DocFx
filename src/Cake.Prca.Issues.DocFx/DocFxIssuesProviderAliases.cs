namespace Cake.Prca.Issues.DocFx
{
    using Core;
    using Core.Annotations;
    using Core.IO;

    /// <summary>
    /// Contains functionality related to importing warnings from DocFx to write them to pull requests.
    /// </summary>
    [CakeAliasCategory(CakeAliasConstants.MainCakeAliasCategory)]
    public static class DocFxIssuesProviderAliases
    {
        /// <summary>
        /// Gets an instance of a provider for warnings reported by DocFx using a log file from disk
        /// for a DocFx project in the repository root.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the the DocFx log file.</param>
        /// <returns>Instance of a provider for warnings reported by DocFx.</returns>
        /// <example>
        /// <para>Report warnings reported by DocFx to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     ReportIssuesToPullRequest(
        ///         DocFxIssuesFromFilePath(
        ///             "C:\build\docfx.log"),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ICodeAnalysisProvider DocFxIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));

            return context.DocFxIssuesFromFilePath(logFilePath, "/");
        }

        /// <summary>
        /// Gets an instance of a provider for warnings reported by DocFx using a log file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the the DocFx log file.</param>
        /// <param name="docRootPath">Path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.</param>
        /// <returns>Instance of a provider for warnings reported by DocFx.</returns>
        /// <example>
        /// <para>Report warnings reported by DocFx to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     ReportIssuesToPullRequest(
        ///         DocFxIssuesFromFilePath(
        ///             "C:\build\docfx.log",
        ///             "C:\build\doc"),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ICodeAnalysisProvider DocFxIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath,
            DirectoryPath docRootPath)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));
            docRootPath.NotNull(nameof(docRootPath));

            return context.DocFxIssues(DocFxIssuesSettings.FromFilePath(logFilePath, docRootPath));
        }

        /// <summary>
        /// Gets an instance of a provider for warnings reported by DocFx using log file content
        /// for a DocFx project in the repository root.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the the DocFx log file.</param>
        /// <returns>Instance of a provider for warnings reported by DocFx.</returns>
        /// <example>
        /// <para>Report warnings reported by DocFx to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     ReportIssuesToPullRequest(
        ///         DocFxIssuesFromContent(
        ///             logFileContent),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ICodeAnalysisProvider DocFxIssuesFromContent(
            this ICakeContext context,
            string logFileContent)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));

            return context.DocFxIssues(DocFxIssuesSettings.FromContent(logFileContent, "/"));
        }

        /// <summary>
        /// Gets an instance of a provider for warnings reported by DocFx using log file content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the the DocFx log file.</param>
        /// <param name="docRootPath">Path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.</param>
        /// <returns>Instance of a provider for warnings reported by DocFx.</returns>
        /// <example>
        /// <para>Report warnings reported by DocFx to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     ReportIssuesToPullRequest(
        ///         DocFxIssuesFromContent(
        ///             logFileContent,
        ///             "C:\build\doc"),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ICodeAnalysisProvider DocFxIssuesFromContent(
            this ICakeContext context,
            string logFileContent,
            DirectoryPath docRootPath)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            docRootPath.NotNull(nameof(docRootPath));

            return context.DocFxIssues(DocFxIssuesSettings.FromContent(logFileContent, docRootPath));
        }

        /// <summary>
        /// Gets an instance of a provider for warnings reported by DocFx using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the DocFx log.</param>
        /// <returns>Instance of a provider for warnings reported by DocFx.</returns>
        /// <example>
        /// <para>Report warnings reported by DocFx to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     var settings =
        ///         new DocFxIssuesSettings("C:\build\docfx.log");
        ///
        ///     ReportIssuesToPullRequest(
        ///         DocFxIssues(settings),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(CakeAliasConstants.CodeAnalysisProviderCakeAliasCategory)]
        public static ICodeAnalysisProvider DocFxIssues(
            this ICakeContext context,
            DocFxIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new DocFxIssuesProvider(context.Log, settings);
        }
    }
}
