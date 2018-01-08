using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class FunctionNodeBuilder : RheaBaseListener
    {
        public FunctionNode Function { get; private set; }

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            Function = new FunctionNode
            {
                Name = context.name().GetText(),
                Type = new TypeNode(context.type().GetText())
            };
        }

        public override void EnterArgument([NotNull] RheaParser.ArgumentContext context)
        {
            var arg = new FunctionArgumentNode
            {
                Name = context.name().GetText(),
                Type = context.type().GetText()
            };

            Function.Arguments.Add(arg);
        }

        public override void EnterBlock([NotNull] RheaParser.BlockContext context)
        {
            Function.Block = new BlockNode
            {
                Statements = context._statements.Select(s => new StatementNodeBuilder().Visit(s))
            };
        }
    }
}