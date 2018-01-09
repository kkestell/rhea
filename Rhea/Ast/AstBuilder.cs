using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    public class AstBuilder : RheaBaseListener
    {
        Nodes.Program Program { get; } = new Nodes.Program();

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            var builder = new FunctionBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Program.Functions.Add(builder.Function);
        }

        public override string ToString()
        {
            return Program.ToString();
        }
    }
}