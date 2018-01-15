using System;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    internal class ExpressionBuilder : RheaBaseVisitor<Expression>
    {
        public Block Scope { get; set; }

        public ExpressionBuilder(Block scope)
        {
            Scope = scope;
        }

        public override Expression VisitNumber([NotNull] RheaParser.NumberContext context)
        {
            var val = context.value.Text;

            System.Int64 int64;
            System.Double float64;
            
            if (System.Int64.TryParse(val, out int64))
            {
                return new Nodes.Int64
                {
                    Scope = Scope,
                    Value = int64
                };
            }

            if (System.Double.TryParse(val, out float64))
            {
                return new Nodes.Float64
                {
                    Scope = Scope,
                    Value = float64
                };
            }

            throw new NotImplementedException();
        }

        public override Expression VisitNumberWithPrecision(RheaParser.NumberWithPrecisionContext context)
        {
            var value = context.value.Text;
            var type = context.numericType.Text;

            switch (type)
            {
                case "int32":
                    return new Nodes.Int32
                    {
                        Scope = Scope,
                        Value = System.Int32.Parse(value)
                    };
                case "float32":
                    return new Nodes.Float32
                    {
                        Scope = Scope,
                        Value = System.Single.Parse(value)
                    };
                default:
                    throw new Exception($"Don't know how to make a {type}");
            }
        }

        public override Expression VisitVariable([NotNull] RheaParser.VariableContext context)
        {
            return new Variable
            {
                Scope = Scope,
                Name = context.value.Text
            };
        }

        public override Expression VisitParensExpression([NotNull] RheaParser.ParensExpressionContext context)
        {
            return new ParensExpression
            {
                Scope = Scope,
                Expression = Visit(context.expression())
            };
        }

        public override Expression VisitFunctionCall(RheaParser.FunctionCallContext context)
        {
            var builder = new ArgumentListBuilder(Scope);
            ParseTreeWalker.Default.Walk(builder, context.arguments);

            return new FunctionCall
            {
                Scope = Scope,
                Name = context.functionName.Text,
                Arguments = builder.Arguments
            };
        }

        public override Expression VisitUnaryExpression([NotNull] RheaParser.UnaryExpressionContext context)
        {
            UnaryExpression node;

            switch (context.op.Type)
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

            node.Scope = Scope;
            node.Expression = Visit(context.expression());

            return node;
        }

        public override Expression VisitInfixExpression([NotNull] RheaParser.InfixExpressionContext context)
        {
            InfixExpression node;

            switch (context.op.Type)
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
            node.Left.Scope = Scope;

            node.Right = Visit(context.right);
            node.Right.Scope = Scope;

            return node;
        }
    }
}