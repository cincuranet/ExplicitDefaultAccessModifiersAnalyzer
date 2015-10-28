using System;
using TestHelper;
using NUnit.Framework;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis;

namespace ExplicitDefaultAccessModifiersAnalyzer.Tests
{
	[TestFixture]
	public class ClassStructInterfaceEnumTests : TestsBase
	{
		[Test]
		public void RegularExplicitInternalClass()
		{
            const string Test = @"
namespace N
{
	internal class C
	{ }
}";
			const string Fixed = @"
namespace N
{
	class C
	{ }
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
		public void PartialExplicitInternalClass()
		{
			const string Test = @"
namespace N
{
	internal partial class C
	{ }
}";
			const string Fixed = @"
namespace N
{
	partial class C
	{ }
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
		public void RegularExplicitInternalStruct()
		{
			const string Test = @"
namespace N
{
	internal struct S
	{ }
}";
			const string Fixed = @"
namespace N
{
	struct S
	{ }
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
		public void RegularExplicitInternalInterface()
		{
			const string Test = @"
namespace N
{
	internal interface I
	{ }
}";
			const string Fixed = @"
namespace N
{
	interface I
	{ }
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
		public void NestedExplicitPrivateClassInClass()
		{
			const string Test = @"
namespace N
{
	class C
	{
		private class NC
		{ }
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{
		class NC
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
		public void NestedExplicitPrivateClassInStruct()
		{
			const string Test = @"
namespace N
{
	struct S
	{
		private class NC
		{ }
	}
}";
			const string Fixed = @"
namespace N
{
	struct S
	{
		class NC
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
		public void NestedExplicitPrivateStructInClass()
		{
			const string Test = @"
namespace N
{
	class C
	{
		private struct NS
		{ }
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{
		struct NS
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
		public void NestedExplicitPrivateStructInStruct()
		{
			const string Test = @"
namespace N
{
	struct S
	{
		private struct NS
		{ }
	}
}";
			const string Fixed = @"
namespace N
{
	struct S
	{
		struct NS
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

		[Test]
		public void NestedExplicitPrivateInterfaceInClass()
		{
			const string Test = @"
namespace N
{
	class C
	{
		private interface NI
		{ }
	}
}";
			const string Fixed = @"
namespace N
{
	class C
	{
		interface NI
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
		public void NestedExplicitPrivateInterfaceInStruct()
		{
			const string Test = @"
namespace N
{
	struct S
	{
		private interface NI
		{ }
	}
}";
			const string Fixed = @"
namespace N
{
	struct S
	{
		interface NI
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
	}
}