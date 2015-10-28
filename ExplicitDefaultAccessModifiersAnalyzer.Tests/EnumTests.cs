using System;
using TestHelper;
using NUnit.Framework;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis;

namespace ExplicitDefaultAccessModifiersAnalyzer.Tests
{
	[TestFixture]
	public class EnumTests : CodeFixVerifier
	{
		[Test]
		public void RegularExplicitInternalEnum()
		{
			const string Test = @"
namespace N
{
	internal enum E
	{ }
}";
			const string Fixed = @"
namespace N
{
	enum E
	{ }
}";
			var expected = new DiagnosticResult
			{
				Id = ExplicitDefaultAccessModifiersAnalyzer.DiagnosticId,
				Severity = DiagnosticSeverity.Warning,
				Locations = new[]
				{
					new DiagnosticResultLocation("Test.cs", 4, 2)
				}
			};
			VerifyCSharpDiagnostic(Test, expected);
			VerifyCSharpFix(Test, Fixed);
		}

		[Test]
		public void NestedExplicitPrivateEnumInClass()
		{
			const string Test = @"
namespace N
{
	class C
	{
		private enum NE
		{ }
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{
		enum NE
		{ }
	}
}";
			var expected = new DiagnosticResult
			{
				Id = ExplicitDefaultAccessModifiersAnalyzer.DiagnosticId,
				Severity = DiagnosticSeverity.Warning,
				Locations = new[]
				{
					new DiagnosticResultLocation("Test0.cs", 6, 3)
				}
			};
			VerifyCSharpDiagnostic(Test, expected);
			VerifyCSharpFix(Test, Fixed);
		}

		[Test]
		public void NestedExplicitPrivateEnumInStruct()
		{
			const string Test = @"
namespace N
{
	struct S
	{
		private enum NE
		{ }
	}
}";
			const string Fixed = @"
namespace N
{
	struct S
	{
		enum NE
		{ }
	}
}";
			var expected = new DiagnosticResult
			{
				Id = ExplicitDefaultAccessModifiersAnalyzer.DiagnosticId,
				Severity = DiagnosticSeverity.Warning,
				Locations = new[]
				{
					new DiagnosticResultLocation("Test0.cs", 6, 3)
				}
			};
			VerifyCSharpDiagnostic(Test, expected);
			VerifyCSharpFix(Test, Fixed);
		}

		protected override CodeFixProvider GetCSharpCodeFixProvider()
		{
			return new ExplicitDefaultAccessModifiersCodeFixProvider();
		}

		protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
		{
			return new ExplicitDefaultAccessModifiersAnalyzer();
		}
	}
}