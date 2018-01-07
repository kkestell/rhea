using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class AstBuilder : RheaBaseListener
    {
        public ProgramNode Program { get; } = new ProgramNode();

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            var builder = new FunctionNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Program.Functions.Add(builder.Function);
        }
    }
}