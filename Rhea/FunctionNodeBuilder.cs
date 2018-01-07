using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Rhea
{
    class FunctionNodeBuilder : RheaBaseListener
    {
        public FunctionNode Function { get; private set; }

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            Function = new FunctionNode
            {
                Name = context.name().GetText(),
                Type = context.type().GetText()
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
            var builder = new BlockNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Function.Block = builder.Block;
        }
    }
}