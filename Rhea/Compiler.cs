using System;
using System.Collections.Generic;
using System.Linq;

using Antlr4.Runtime;

using Rhea.Ast;
using Rhea.Ast.Nodes;
using Rhea.Errors;

namespace Rhea
{
	public class Compiler
	{
		readonly string standardLibrary = @"
			struct string {}
			extern fun string#dup(self : string) -> string
			extern fun string#trim(self : string) -> string
			extern fun string#length(self : string) -> int64
		";

		public string Compile(string source)
		{
			var stdlib = CompileModule(standardLibrary);

			var module = CompileModule(source, stdlib);

			CheckModule(module);

			return module.ToString();
		}

		Module CompileModule(string source, Module parent = null)
		{
			source = source.Replace("\r\n", "\n");

			var input = new AntlrInputStream(source);

			var lexer = new RheaLexer(input);
			var tokens = new CommonTokenStream(lexer);

			var parser = new RheaParser(tokens);
			var tree = parser.module();

			return new ModuleBuilder(parent).Visit(tree);
		}

		void CheckModule(Module module)
		{
			// Return statements

			foreach (var f in module.Functions)
			{
				var returnStatements = FindStatements<Return>(f.Block).ToList();

				if (f.Type.Name == "void")
				{
					// Void functions must return void

					foreach (var s in returnStatements)
					{
						if (s.Expression != null)
							throw new TypeError($"Function `{f.Name}` must return `void`, but it returns a `{s.Expression.InferredType.Name}`.\n\n{s.Source}");
					}
				}
				else
				{
					// Non-void functions must return something

					if (!returnStatements.Any())
						throw new Exception($"Function {f.Name} must return a {f.Type.Name}, but it returns nothing.");

					// And it must be the appropriate type

					foreach (var s in returnStatements)
					{
						if (s.Expression.InferredType != f.Type)
							throw new TypeError($"Function {f.Name} must return a {f.Type.Name}, not a {s.Expression.InferredType.Name}.");
					}
				}
			}

			// The expressions in if statements must evaluate to a boolean

			foreach (var function in module.Functions)
			{
				foreach (var ifStatement in FindStatements<If>(function.Block))
				{
					var typeName = ifStatement.Expression.InferredType.Name;

					if (typeName != "bool")
						throw new TypeError($"Expression must evaluate to a bool, but evaluates to a {typeName}");
				}
			}

			// Prevent multiple declarations of variables with the same name in the same scope

			foreach (var function in module.Functions)
			{
				foreach (var variableDeclaration in FindStatements<VariableDeclaration>(function.Block))
				{
					var duplicateDeclarations = variableDeclaration
						.ParentBlock
						.Statements
						.OfType<VariableDeclaration>()
						.Where(d => d.Name == variableDeclaration.Name);

					if (duplicateDeclarations.Count() > 1)
						throw new Exception($"Multiple declaration of variable {variableDeclaration.Name}");
				}
			}

			// Types of left and right sides of assignment statements must match

			foreach (var function in module.Functions)
			{
				foreach (var assignment in FindStatements<Assignment>(function.Block))
				{
					var declaration = assignment.ParentBlock.FindDeclaration(assignment.VariableName);

					if (declaration == null)
						throw new Exception($"Can't find declaration for {assignment.VariableName}");

					if (declaration.Type != assignment.Expression.InferredType)
						throw new TypeError($"Types of left ({declaration.Type.Name}) and right ({assignment.Expression.InferredType.Name}) sides of assignment statement must match");
				}
			}

			// Types of left and right sides of member assignment statements must match

			foreach (var function in module.Functions)
			{
				foreach (var assignment in FindStatements<MemberAssignment>(function.Block))
				{
					var variableDeclaration = assignment.ParentBlock.FindDeclaration(assignment.VariableName);

					if (variableDeclaration == null)
						throw new Exception($"Can't find declaration for {assignment.VariableName}");

					var structDeclaration = assignment.ParentBlock.FindStruct(variableDeclaration.Type.Name);

					if (structDeclaration == null)
						throw new Exception($"Can't find declaration for {variableDeclaration.Type.Name}");

					var member = structDeclaration.Members.SingleOrDefault(m => m.Name == assignment.MemberName);

					if (member == null)
						throw new Exception($"Struct {structDeclaration.Name} has no member named {assignment.MemberName}");

					if (member.Type != assignment.Expression.InferredType)
						throw new TypeError($"Types of left ({member.Type.Name}) and right ({assignment.Expression.InferredType.Name}) sides of member assignment statement must match");
				}
			}
		}

		static IEnumerable<T> FindStatements<T>(Block scope)
		{
			var childStatements = new List<T>();

			var scopes = scope
				.Statements
				.OfType<IStatementWithBlock>();

			foreach (var statement in scopes)
				childStatements.AddRange(FindStatements<T>(statement.Block));

			var returnStatements = scope
				.Statements
				.OfType<T>()
				.ToList();

			returnStatements.AddRange(childStatements);

			return returnStatements;
		}
	}
}
