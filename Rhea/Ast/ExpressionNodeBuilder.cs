using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;
using System;
using System.Globalization;

namespace Rhea.Ast
{
    class NumberNode : AtomNode
    {
        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    class VariableNode : AtomNode
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    class UnaryExpressionNode : ExpressionNode
    {
        public ExpressionNode Expression { get; set; }

        public override string ToString()
        {
            return $"({Expression})";
        }
    }

    class UnaryNegationExpressionNode : UnaryExpressionNode
    {
        public override string ToString()
        {
            return $"(-{Expression})";
        }
    }

    class AdditionNode : InfixExpressionNode
    {
        public override string ToString()
        {
            return $"({Left} + {Right})";
        }
    }

    class SubtractionNode : InfixExpressionNode
    {
        public override string ToString()
        {
            return $"({Left} - {Right})";
        }
    }

    class MultiplicationNode : InfixExpressionNode
    {
        public override string ToString()
        {
            return $"({Left} * {Right})";
        }
    }

    class DivisionNode : InfixExpressionNode
    {
        public override string ToString()
        {
            return $"({Left} / {Right})";
        }
    }

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
