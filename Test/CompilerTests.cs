using System;

using Xunit;

using Rhea;
using Rhea.Errors;

namespace Test
{
	public class CompilerTests
	{
		#region Misc

		[Fact]
		public void Fibonaci()
		{
			new Compiler().Compile(@"
				fun fib(n : int64) -> int64 {
					if (n == 0) {
						return 0
					}
					if (n == 1) {
						return 1
					}
					return fib(n - 2) + fib(n - 1)
				}

				fun main() -> int64 {
					return fib(10)
				}
			");
		}

		[Fact]
		public void FizzBuzz()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					for(var i in 0..100) {
						if(i % 3 == 0) {
							# Fizz
						}
						if(i % 5 == 0) {
							# Buzz
						}
						if((i % 3 != 0) && (i % 5 != 0)) {
							# i
						}
					}
				}
			");
		}

		#endregion

		#region Compiler Checks

		[Fact]
		public void IfExpressionMustEvaluateToABoolean()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : int64
						x = 0
						if(x) {
						}
					}
				")
			);
		}

		[Fact]
		public void MultipleDeclarationsInScope()
		{
			Assert.Throws<Exception>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : int64
						var x : string
					}
				")
			);
		}

		[Fact]
		public void AssignmentTypes()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : int64
						x = 0.5
					}
				")
			);
		}

		#endregion

		#region Syntax

		#region Return Statements

		[Fact]
		public void ReturnVoid()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					return
				}
			");
		}

		[Fact]
		public void ReturnVoidWrongType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						return 1
					}
				")
			);
		}

		[Fact]
		public void ReturnNothing()
		{
			Assert.Throws<Exception>(() =>
				new Compiler().Compile(@"
					fun main() -> int32 {
					}
				")
			);
		}

		[Fact]
		public void ReturnLiteral()
		{
			new Compiler().Compile(@"
				fun main() -> int64 {
					return 0
				}
			");
		}

		[Fact]
		public void ReturnLiteralWrongType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> int32 {
						return 0
					}
				")
			);
		}

		[Fact]
		public void ReturnExpression()
		{
			new Compiler().Compile(@"
				fun main() -> int64 {
					return 1 + 1
				}
			");
		}

		[Fact]
		public void ReturnExpressionWrongType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> int64 {
						return 1.5 + 1.5
					}
				")
			);
		}

		[Fact]
		public void ReturnStructMember()
		{
			new Compiler().Compile(@"
				struct foo {
					bar : int64
				}

				fun main() -> int64 {
					var quux : foo
					quux.bar = 0
					return quux.bar
				}
			");
		}

		[Fact]
		public void ReturnStructMemberWrongType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					struct foo {
						bar : int32
					}

					fun main() -> int64 {
						var quux : foo
						quux.bar = 0
						return quux.bar
					}
				")
			);
		}

		[Fact]
		public void ReturnMethodCall()
		{
			new Compiler().Compile(@"
				struct foo {
					bar : int64
				}

				fun foo#bar(self : foo) -> int64 {
					return self.bar
				}

				fun main() -> int64 {
					var quux : foo
					quux.bar = 42
					return quux.bar()
				}
			");
		}

		[Fact]
		public void ReturnMethodCallWrongType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					struct foo {
						bar : int32
					}

					fun foo#bar(self : foo) -> int32 {
						return self.bar
					}

					fun main() -> int64 {
						var quux : foo
						quux.bar = int32(42)
						return quux.bar()
					}
				")
			);
		}

		#endregion

		#region For Statements

		[Fact]
		public void ForRange()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					for(var x in 0..10) {
					}
				}
			");
		}

		#endregion

		#region If Statements

		[Fact]
		public void IfEqual()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					var x = 0
					if(x == 0) {
					}
				}
			");
		}

		[Fact]
		public void IfBoolean()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					if(true) {
					}
				}
			");
		}

		#endregion

		#region Structs

		[Fact]
		public void MethodCallReceiverMustBeStruct()
		{
			Assert.Throws<Exception>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var foo = 0
						foo.bar()
					}
				")
			);
		}

		[Fact]
		public void MethodCall()
		{
			new Compiler().Compile(@"
				struct quux {
				}

				fun quux#bar() -> int64 {
					return 0
				}

				fun main() -> int64 {
					var foo : quux
					return foo.bar()
				}
			");
		}

		[Fact]
		public void MethodCallFunctionMustExist()
		{
			Assert.Throws<Exception>(() =>
				new Compiler().Compile(@"
					struct quux {
					}

					fun main() -> void {
						var foo : quux
						foo.bar()
					}
				")
			);
		}

		#endregion

		#endregion
	}
}
