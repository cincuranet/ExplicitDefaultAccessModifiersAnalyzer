using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ExplicitDefaultAccessModifiersAnalyzer
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class ExplicitDefaultAccessModifiersAnalyzer : DiagnosticAnalyzer
	{
		public const string DiagnosticId = "EDAM001";

		static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, DiagnosticId, "Explicit default access modifiers are just noise.", "Naming", DiagnosticSeverity.Warning, isEnabledByDefault: true);

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

		public override void Initialize(AnalysisContext context)
		{
			context.RegisterSyntaxNodeAction(AnalyzeClassOrStructOrInterfaceDeclaration, SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration, SyntaxKind.InterfaceDeclaration, SyntaxKind.EnumDeclaration);
			context.RegisterSyntaxNodeAction(AnalyzeFieldOrEvent, SyntaxKind.FieldDeclaration, SyntaxKind.EventFieldDeclaration);
			context.RegisterSyntaxNodeAction(AnalyzeMethodOrConstructor, SyntaxKind.MethodDeclaration, SyntaxKind.ConstructorDeclaration);
			context.RegisterSyntaxNodeAction(AnalyzeProperty, SyntaxKind.PropertyDeclaration);
			context.RegisterSyntaxNodeAction(AnalyzeDelegate, SyntaxKind.DelegateDeclaration);
		}

		static void AnalyzeClassOrStructOrInterfaceDeclaration(SyntaxNodeAnalysisContext context)
		{
			var node = (BaseTypeDeclarationSyntax)context.Node;
			var defaultModifier = node.Parent.IsKind(SyntaxKind.NamespaceDeclaration)
				? SyntaxKind.InternalKeyword
				: SyntaxKind.PrivateKeyword;
			HandleDefaultModifier(context, node.Modifiers, defaultModifier);
		}

		static void AnalyzeFieldOrEvent(SyntaxNodeAnalysisContext context)
		{
			var node = (BaseFieldDeclarationSyntax)context.Node;
			HandleDefaultModifier(context, node.Modifiers, SyntaxKind.PrivateKeyword);
		}

		static void AnalyzeMethodOrConstructor(SyntaxNodeAnalysisContext context)
		{
			var node = (BaseMethodDeclarationSyntax)context.Node;
			HandleDefaultModifier(context, node.Modifiers, SyntaxKind.PrivateKeyword);
		}

		static void AnalyzeProperty(SyntaxNodeAnalysisContext context)
		{
			var node = (PropertyDeclarationSyntax)context.Node;
			HandleDefaultModifier(context, node.Modifiers, SyntaxKind.PrivateKeyword);
		}

		static void AnalyzeDelegate(SyntaxNodeAnalysisContext context)
		{
			var node = (DelegateDeclarationSyntax)context.Node;
			var defaultModifier = node.Parent.IsKind(SyntaxKind.NamespaceDeclaration)
				? SyntaxKind.InternalKeyword
				: SyntaxKind.PrivateKeyword;
			HandleDefaultModifier(context, node.Modifiers, defaultModifier);
		}

		static void HandleDefaultModifier(SyntaxNodeAnalysisContext context, SyntaxTokenList modifiers, SyntaxKind defaultModifier)
		{
			var index = modifiers.IndexOf(defaultModifier);
			if (index != -1)
			{
				context.ReportDiagnostic(Diagnostic.Create(Rule, modifiers[index].GetLocation()));
			}
		}
	}
}
