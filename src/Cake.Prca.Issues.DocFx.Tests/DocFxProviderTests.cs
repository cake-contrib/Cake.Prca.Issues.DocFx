﻿namespace Cake.Prca.Issues.DocFx.Tests
{
    using System.Linq;
    using Core.IO;
    using Shouldly;
    using Testing;
    using Xunit;

    public class DocFxProviderTests
    {
        public sealed class TheMsBuildCodeAnalysisProviderCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new DocFxIssuesProvider(
                        null,
                        DocFxIssuesSettings.FromContent("Foo", @"c:\Source\Cake.Prca")));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                var result = Record.Exception(() =>
                    new DocFxIssuesProvider(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Theory]
            [InlineData(@"c:\Source\Cake.Prca\docs", "docs/")]
            [InlineData(@"docs", "docs/")]
            [InlineData(@"c:\Source\Cake.Prca\src\docs", "src/docs/")]
            [InlineData(@"src\docs", "src/docs/")]
            [InlineData(@"c:\Source\Cake.Prca", "")]
            [InlineData(@"/", "")]
            public void Should_Read_Issue_Correct(string docRootPath, string docRelativePath)
            {
                // Given
                var fixture = new DocFxProviderFixture("docfx.json", docRootPath);

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                CheckIssue(
                    issues[0],
                    docRelativePath + @"index.md",
                    null,
                    "Build Document.LinkPhaseHandler.Apply Templates",
                    null,
                    0,
                    "Invalid cross reference \"[Foo](xref:foo)\".");
            }

            [Fact]
            public void Should_Read_Line_Correct()
            {
                // Given
                var fixture = new DocFxProviderFixture("entry-with-line.json", @"/");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                CheckIssue(
                    issues[0],
                    @"bar.md",
                    45,
                    "Build Document.LinkPhaseHandler.ConceptualDocumentProcessor.Save",
                    null,
                    0,
                    "Invalid file link:(~/foo.md).");
            }

            private static void CheckIssue(
                ICodeAnalysisIssue issue,
                string affectedFileRelativePath,
                int? line,
                string rule,
                string ruleUrl,
                int priority,
                string message)
            {
                if (issue.AffectedFileRelativePath == null)
                {
                    affectedFileRelativePath.ShouldBeNull();
                }
                else
                {
                    issue.AffectedFileRelativePath.ToString().ShouldBe(new FilePath(affectedFileRelativePath).ToString());
                    issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "Issue path is not relative");
                }

                issue.Line.ShouldBe(line);
                issue.Rule.ShouldBe(rule);

                if (issue.RuleUrl == null)
                {
                    ruleUrl.ShouldBeNull();
                }
                else
                {
                    issue.RuleUrl.ToString().ShouldBe(ruleUrl);
                }

                issue.Priority.ShouldBe(priority);
                issue.Message.ShouldBe(message);
            }
        }
    }
}
