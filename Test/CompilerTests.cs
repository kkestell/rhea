using Xunit;

using Rhea;
using Rhea.Errors;
using System;

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

		// TODO: Add support for &&
		/*
		[Fact]
		public void FizzBuzz()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					for (var i in 0..100) {
						if (i % 3 == 0) {
							# Fizz
						}
						if (i % 5 == 0) {
							# Buzz
						}
						if ((i % 3 != 0) && (i % 5 != 0)) {
							# i
						}
					}
				}
			");
		}
		*/

		#endregion

		#region Compiler Checks

		[Fact]
		public void FunctionCallParameterCount()
		{
			Assert.Throws<ArgumentError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						foo(1)
					}

					fun foo() -> void {
					}
				")
			);

			Assert.Throws<ArgumentError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						foo()
					}

					fun foo(a : int64) -> void {
					}
				")
			);
		}

		[Fact]
		public void FunctionCallParameterType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						foo(1)
					}

					fun foo(a : float64) -> void {
					}
				")
			);
		}

		[Fact]
		public void VariableInitializationExpressionInvalidType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x = 1 + 1.5
					}
				")
			);
		}

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
			Assert.Throws<MultipleDeclarationError>(() =>
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
						x = ""invalid""
					}
				")
			);
		}

		#endregion

		#region Syntax

		#region Variable Initialization

		[Fact]
		public void AssignToFunctionResultUseOfUndefinedVariableAsArgument()
		{
			Assert.Throws<UseOfUndefinedVariableError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var quux = foo(xxxxxxxxxxxx)
					}

					fun foo(x : int64) -> int64 {
						return x
					}
				")
			);
		}

		#endregion

		#region Function Calls

		[Fact]
		public void UseOfUndefinedVariableAsArgument()
		{
			Assert.Throws<UseOfUndefinedVariableError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						foo(xxxxxxxxxxxx)
					}

					fun foo(x : int64) -> int64 {
						return x
					}
				")
			);
		}

		#endregion

		#region Numbers

		[Fact]
		public void Integers()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					var i32 : int32
					var i64 : int64

					i32 = int32(1)
					i64 = 1

					var foo = addi32(i32, int32(1))
					var bar = addi64(i64, 1)
				}

				fun addi32(x : int32, y : int32) -> int32 {
					return x + y
				}

				fun addi64(x : int64, y : int64) -> int64 {
					return x + y
				}
			");
		}

		[Fact]
		public void IntegerTypes()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : int32
						x = 1
					}
				")
			);

			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : int64
						x = int32(1)
					}
				")
			);

			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : int64
						var y : int32
						var z = x + y
					}
				")
			);
		}

		[Fact]
		public void Floats()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					var f32 : float32
					var f64 : float64

					f32 = float32(1.0)
					f64 = 1.0

					var foo = addf32(f32, float32(1.0))
					var bar = addf64(f64, 1.0)
				}

				fun addf32(x : float32, y : float32) -> float32 {
					return x + y
				}

				fun addf64(x : float64, y : float64) -> float64 {
					return x + y
				}
			");
		}

		[Fact]
		public void FloatTypes()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : float32
						x = 1.0
					}
				")
			);

			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : float64
						x = float32(1)
					}
				")
			);

			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : float64
						var y : float32
						var z = x + y
					}
				")
			);
		}

		#endregion

		#region Assignent

		[Fact]
		public void AssignmentToUndefinedVariable()
		{
			Assert.Throws<UseOfUndefinedVariableError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						foo = 1
					}
				")
			);
		}

		[Fact]
		public void AssignmentTypesMustMatch()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> void {
						var x : int64
						x = 3.14
					}
				")
			);
		}

		#endregion

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
			Assert.Throws<FunctionMustReturnAValueError>(() =>
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
					return 1
				}
			");
		}

		[Fact]
		public void ReturnExpressionWrongType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> int64 {
						return 1.5
					}
				")
			);
		}

		[Fact]
		public void ReturnStructMember()
		{
			new Compiler().Compile(@"
				fun main() -> int64 {
					var quux : foo
					quux.bar = 0
					return quux.bar
				}

				struct foo {
					bar : int64
				}
			");
		}

		[Fact]
		public void ReturnStructMemberWrongType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> int64 {
						var quux : foo
						quux.bar = 0
						return quux.bar
					}

					struct foo {
						bar : int32
					}
				")
			);
		}

		[Fact]
		public void ReturnMethodCall()
		{
			new Compiler().Compile(@"
				fun main() -> int64 {
					var quux : foo
					quux.bar = 42
					return quux.bar()
				}

				struct foo {
					bar : int64
				}

				fun foo#bar(self : foo) -> int64 {
					return self.bar
				}
			");
		}

		[Fact]
		public void ReturnMethodCallWrongType()
		{
			Assert.Throws<TypeError>(() =>
				new Compiler().Compile(@"
					fun main() -> int64 {
						var quux : foo
						quux.bar = int32(42)
						return quux.bar()
					}

					struct foo {
						bar : int32
					}

					fun foo#bar(self : foo) -> int32 {
						return self.bar
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

		[Fact]
		public void IfFunctionThatReturnsBoolean()
		{
			new Compiler().Compile(@"
				fun main() -> void {
					if(foo()) {
					}
				}

				fun foo() -> bool {
					return true
				}
			");
		}

		#endregion

		#region Structs

		[Fact]
		public void MethodCallReceiverMustBeStruct()
		{
			Assert.Throws<UseOfUndefinedStructError>(() =>
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
			Assert.Throws<UseOfUndefinedFunctionError>(() =>
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
