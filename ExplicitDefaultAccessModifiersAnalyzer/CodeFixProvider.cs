using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;

namespace ExplicitDefaultAccessModifiersAnalyzer
{
	[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ExplicitDefaultAccessModifiersCodeFixProvider)), Shared]
	public class ExplicitDefaultAccessModifiersCodeFixProvider : CodeFixProvider
	{
		const string Title = "Remove the noise";

		public sealed override ImmutableArray<string> FixableDiagnosticIds
		{
			get { return ImmutableArray.Create(ExplicitDefaultAccessModifiersAnalyzer.DiagnosticId); }
		}

		public sealed override FixAllProvider GetFixAllProvider()
		{
			return WellKnownFixAllProviders.BatchFixer;
		}

		public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
		{
			var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
			var diagnostic = context.Diagnostics.First();
			var diagnosticSpan = diagnostic.Location.SourceSpan;
			var syntax = root.FindToken(diagnosticSpan.Start);
			context.RegisterCodeFix(
				CodeAction.Create(
					title: Title,
					createChangedDocument: c => Fix(context.Document, syntax, c),
					equivalenceKey: Title),
				diagnostic);
		}

		async Task<Document> Fix(Document document, SyntaxToken syntax, CancellationToken cancellationToken)
		{
			var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
			var nextToken = syntax.GetNextToken();
			return document.WithSyntaxRoot(root
				.ReplaceTokens(new[] { syntax, nextToken }, (t, _) =>
				{
					if (t == syntax)
						return SyntaxFactory.Token(SyntaxKind.None);
					if (t == nextToken)
						return nextToken.WithLeadingTrivia(syntax.LeadingTrivia.AddRange(nextToken.LeadingTrivia));
					return default(SyntaxToken);
				}));
		}
	}
}