using System;
using TestHelper;
using NUnit.Framework;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis;

namespace ExplicitDefaultAccessModifiersAnalyzer.Tests
{
	[TestFixture]
	public class MethodConstructorTests : CodeFixVerifier
	{
		[Test]
		public void ExplicitPrivateMethod()
		{
            const string Test = @"
namespace N
{
	class C
	{ 
		private int M() { };
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{ 
		int M() { };
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
		public void ImplicitPrivateMethod()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		int M() { };
	}
}";
			VerifyCSharpDiagnostic(Test);
		}

		[Test]
		public void InternalMethod()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		internal int M() { };
	}
}";
			VerifyCSharpDiagnostic(Test);
		}

		[Test]
		public void ExplicitPrivateConstructor()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		private C() { };
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{ 
		C() { };
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
		public void ImplicitPrivateConstructor()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		C() { };
	}
}";
			VerifyCSharpDiagnostic(Test);
		}

		[Test]
		public void PublicConstructor()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		public C() { };
	}
}";
			VerifyCSharpDiagnostic(Test);
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