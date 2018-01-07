using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class LiteralExpressionNodeBuilder : RheaBaseListener
    {
        public LiteralExpressionNode LiteralExpression { get; private set; }

        public override void EnterIntegerLiteral(RheaParser.IntegerLiteralContext context)
        {
            LiteralExpression = new IntegerLiteralExpressionNode
            {
                Value = int.Parse(context.GetText())
            };
        }

        public override void EnterStringLiteral(RheaParser.StringLiteralContext context)
        {
            LiteralExpression = new StringLiteralNode
            {
                Value = context.GetText()
            };
        }
    }
}
