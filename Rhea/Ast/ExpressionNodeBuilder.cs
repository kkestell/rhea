using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class ExpressionNodeBuilder : RheaBaseListener
    {
        public ExpressionNode Expression { get; private set; }

        public override void EnterLiteral(RheaParser.LiteralContext context)
        {
            var builder = new LiteralExpressionNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Expression = builder.LiteralExpression;
        }

        public override void EnterName(RheaParser.NameContext context)
        {
            Expression = new NameExpressionNode
            {
                Value = context.GetText()
            };
        }
    }
}
