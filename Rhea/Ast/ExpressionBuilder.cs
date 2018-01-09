using Antlr4.Runtime.Misc;
using Rhea.Ast.Nodes;
using System;
using System.Globalization;
using Antlr4.Runtime.Tree;

namespace Rhea.Ast
{
    class ExpressionBuilder : RheaBaseVisitor<Expression>
    {
        public override Expression VisitNumber([NotNull] RheaParser.NumberContext context)
        {
            return new Number
            {
                Value = double.Parse(context.value.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent)
            };
        }

        public override Expression VisitVariable([NotNull] RheaParser.VariableContext context)
        {
            return new Variable
            {
                Name = context.value.Text
            };
        }

        public override Expression VisitParensExpression([NotNull] RheaParser.ParensExpressionContext context)
        {
            return Visit(context.expression());
        }

        public override Expression VisitFunctionCall(RheaParser.FunctionCallContext context)
        {
            var builder = new ArgumentListBuilder();
            ParseTreeWalker.Default.Walk(builder, context.arguments);

            return new FunctionCall
            {
                Name = context.functionName.Text,
                Arguments = builder.Arguments
            };
        }

        public override Expression VisitUnaryExpression([NotNull] RheaParser.UnaryExpressionContext context)
        {
            UnaryExpression node;

            switch(context.op.Type)
            {
                case RheaLexer.OP_ADD:
                    node = new UnaryExpression();
                    break;
                case RheaLexer.OP_SUB:
                    node = new UnaryNegation();
                    break;
                default:
                    throw new NotImplementedException();
            }

            node.Expression = Visit(context.expression());

            return node;
        }

        public override Expression VisitInfixExpression([NotNull] RheaParser.InfixExpressionContext context)
        {
            InfixExpression node;

            switch(context.op.Type)
            {
                case RheaLexer.OP_ADD:
                    node = new Addition();
                    break;
                case RheaLexer.OP_SUB:
                    node = new Subtraction();
                    break;
                case RheaLexer.OP_MUL:
                    node = new Multiplication();
                    break;
                case RheaLexer.OP_DIV:
                    node = new Division();
                    break;
                case RheaLexer.OP_EQ:
                    node = new RelationalEquality();
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
