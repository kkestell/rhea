using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Rhea.Ast
{
    public class ProgramBuilder : RheaBaseListener
    {
        public Nodes.Program Program { get; } = new Nodes.Program();

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            var builder = new FunctionBuilder(Program);
            ParseTreeWalker.Default.Walk(builder, context);

            Program.Functions.Add(builder.Function);
        }

        public override string ToString()
        {
            return Program.ToString();
        }
    }
}