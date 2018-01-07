using Antlr4.Runtime.Misc;

namespace Rhea
{
    class BlockNodeBuilder : RheaBaseListener
    {
        public BlockNode Block { get; private set; }

        public override void EnterBlock([NotNull] RheaParser.BlockContext context)
        {
            Block = new BlockNode();
        }
    }
}