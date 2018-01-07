using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class BlockNodeBuilder : RheaBaseListener
    {
        public BlockNode Block { get; private set; }

        public override void EnterBlock([NotNull] RheaParser.BlockContext context)
        {
            Block = new BlockNode();
        }

        public override void EnterStatement(RheaParser.StatementContext context)
        {
            var builder = new StatementNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Block.Statements.Add(builder.Statement);
        }
    }
}