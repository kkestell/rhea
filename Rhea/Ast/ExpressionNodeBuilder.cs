using Antlr4.Runtime.Misc;
using Rhea.Ast.Nodes;
using System;
using System.Globalization;

namespace Rhea.Ast
{
    class ExpressionNodeBuilder : RheaBaseVisitor<ExpressionNode>
    {
        public override ExpressionNode VisitNumber([NotNull] RheaParser.NumberContext context)
        {
            return new NumberNode
            {
                Value = double.Parse(context.value.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent)
            };
        }

        public override ExpressionNode VisitVariable([NotNull] RheaParser.VariableContext context)
        {
            return new VariableNode
            {
                Name = context.value.Text
            };
        }

        public override ExpressionNode VisitParensExpression([NotNull] RheaParser.ParensExpressionContext context)
        {
            return Visit(context.expression());
        }

        public override ExpressionNode VisitUnaryExpression([NotNull] RheaParser.UnaryExpressionContext context)
        {
            UnaryExpressionNode node;

            switch(context.op.Type)
            {
                case RheaLexer.OP_ADD:
                    node = new UnaryExpressionNode();
                    break;
                case RheaLexer.OP_SUB:
                    node = new UnaryNegationExpressionNode();
                    break;
                default:
                    throw new NotImplementedException();
            }

            node.Expression = Visit(context.expression());

            return node;
        }

        public override ExpressionNode VisitInfixExpression([NotNull] RheaParser.InfixExpressionContext context)
        {
            InfixExpressionNode node;

            switch(context.op.Type)
            {
                case RheaLexer.OP_ADD:
                    node = new AdditionNode();
                    break;
                case RheaLexer.OP_SUB:
                    node = new SubtractionNode();
                    break;
                case RheaLexer.OP_MUL:
                    node = new MultiplicationNode();
                    break;
                case RheaLexer.OP_DIV:
                    node = new DivisionNode();
                    break;
                default:
                    throw new NotSupportedException();
            }

            node.Left = Visit(context.left);
            node.Right = Visit(context.right);

            return node;
        }
    }
}
