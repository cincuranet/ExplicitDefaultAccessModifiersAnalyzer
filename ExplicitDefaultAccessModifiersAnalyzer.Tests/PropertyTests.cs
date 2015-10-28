using System;
using TestHelper;
using NUnit.Framework;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis;

namespace ExplicitDefaultAccessModifiersAnalyzer.Tests
{
	[TestFixture]
	public class PropertyTests : CodeFixVerifier
	{
		[Test]
		public void ExplicitPrivateProperty()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		private int I { set { } };
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{ 
		int I { set { } };
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
		public void ExplicitPrivateAbstractProperty()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		private abstract int I { set { } };
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{ 
		abstract int I { set { } };
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
		public void ImplicitPrivateProperty()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		int I { set { } };
	}
}";
			VerifyCSharpDiagnostic(Test);
		}

		[Test]
		public void PublicAbstractProperty()
		{
			const string Test = @"
namespace N
{
	class C
	{ 
		public abstract int I { set { } };
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