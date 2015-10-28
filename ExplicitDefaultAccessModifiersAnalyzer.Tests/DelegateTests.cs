using System;
using TestHelper;
using NUnit.Framework;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis;

namespace ExplicitDefaultAccessModifiersAnalyzer.Tests
{
	[TestFixture]
	public class DelegateTests : TestsBase
	{
		[Test]
		public void ExplicitPrivateDelegateInsideClass()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		private delegate void D();
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{ 
		delegate void D();
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
		public void ImplicitPrivateDelegateInsideClass()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		delegate void D();
	}
}";
			VerifyCSharpDiagnostic(Test);
		}

		[Test]
		public void ExplicitInternalDelegateInsideNamespace()
		{
			const string Test = @"
namespace N
{
	internal delegate void D();
}";
			const string Fixed = @"
namespace N
{
	delegate void D();
}";
			var expected = new DiagnosticResult
			{
				Id = ExplicitDefaultAccessModifiersAnalyzer.DiagnosticId,
				Severity = DiagnosticSeverity.Warning,
				Locations = new[]
				{
					new DiagnosticResultLocation("Test0.cs", 4, 2)
				}
			};
			VerifyCSharpDiagnostic(Test, expected);
			VerifyCSharpFix(Test, Fixed);
		}

		[Test]
		public void ImplicitInternalDelegateInsideNamespace()
		{
			const string Test = @"
namespace N
{
	delegate void D();
}";
			VerifyCSharpDiagnostic(Test);
		}
	}
}