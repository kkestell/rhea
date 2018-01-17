using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Rhea.Ast;
using Rhea.Ast.Nodes;

namespace Rhea
{
    public class Compiler
    {
        public string Compile(string source)
        {
            var input = new AntlrInputStream(source);

            var lexer = new RheaLexer(input);
            var tokens = new CommonTokenStream(lexer);

            var parser = new RheaParser(tokens);
            var tree = parser.program();

            var builder = new ProgramBuilder();
            ParseTreeWalker.Default.Walk(builder, tree);

            foreach (var f in builder.Program.Functions)
            {
                var returnStatements = FindStatements<ReturnStatement>(f.Block).ToList();

                if (f.Type.Value == "void")
                {
                    foreach (var s in returnStatements)
                    {
                        if (s.Expression != null)
                            throw new Exception($"Function {f.Name} must return void, but it returns a {s.Expression.InferredType}.");
                    }
                }
                else
                {
                    if (!returnStatements.Any())
                        throw new Exception($"Function {f.Name} must return a {f.Type.Value}, but it returns nothing.");

                    foreach (var s in returnStatements)
                    {
                        if (s.Expression.InferredType != f.Type)
                            throw new Exception($"Function {f.Name} must return a {f.Type.Value}, not a {s.Expression.InferredType.Value}.");
                    }
                }
            }

            foreach (var function in builder.Program.Functions)
            {
                foreach (var ifStatement in FindStatements<IfStatement>(function.Block))
                {
                    var typeName = ifStatement.Expression.InferredType.Value;

                    if (typeName != "bool")
                        throw new Exception($"Expression must evaluate to a bool, but evaluates to a {typeName}");
                }
            }

            foreach (var function in builder.Program.Functions)
            {
                foreach (var variableDeclaration in FindStatements<VariableDeclaration>(function.Block))
                {
                    var duplicateDeclarations = variableDeclaration
                        .Scope
                        .Statements
                        .OfType<VariableDeclaration>()
                        .Where(d => d.Name == variableDeclaration.Name);

                    if (duplicateDeclarations.Count() > 1)
                        throw new Exception($"Multiple declaration of variable {variableDeclaration.Name}");
                }
            }

            return builder.ToString();
        }

        IEnumerable<T> FindStatements<T>(Block scope)
        {
            var childStatements = new List<T>();

            var scopes = scope
                .Statements
                .OfType<StatementWithBlock>();

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
