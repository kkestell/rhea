using System;
using System.Linq;

using Antlr4.Runtime.Misc;

using Rhea.Ast.Nodes;
using Int32 = Rhea.Ast.Nodes.Int32;
using Int64 = Rhea.Ast.Nodes.Int64;

namespace Rhea.Ast
{
	class ExpressionBuilder : RheaBaseVisitor<Expression>
	{
		readonly Block parentBlock;

		public ExpressionBuilder(Block parentBlock)
		{
			this.parentBlock = parentBlock;
		}

		public override Expression VisitNumber([NotNull] RheaParser.NumberContext context)
		{
			var val = context.value.Text;

			if (long.TryParse(val, out var int64))
			{
				return new Int64
				{
					ParentBlock = parentBlock,
					Value = int64
				};
			}

			if (double.TryParse(val, out var float64))
			{
				return new Float64
				{
					ParentBlock = parentBlock,
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
					return new Int32
					{
						ParentBlock = parentBlock,
						Value = int.Parse(value)
					};
				case "float32":
					return new Float32
					{
						ParentBlock = parentBlock,
						Value = float.Parse(value)
					};
				default:
					throw new Exception($"Don't know how to make a {type}");
			}
		}

		public override Expression VisitVariable([NotNull] RheaParser.VariableContext context)
		{
			return new Variable
			{
				ParentBlock = parentBlock,
				Name = context.value.Text
			};
		}

		public override Expression VisitTrue(RheaParser.TrueContext context)
		{
			return new True();
		}

		public override Expression VisitFalse(RheaParser.FalseContext context)
		{
			return new False();
		}

		public override Expression VisitParensExpression([NotNull] RheaParser.ParensExpressionContext context)
		{
			return new ParensExpression
			{
				ParentBlock = parentBlock,
				Expression = Visit(context.expression())
			};
		}

		public override Expression VisitFunctionCall(RheaParser.FunctionCallContext context)
		{
			return new FunctionCall
			{
				ParentBlock = parentBlock,
				Name = context.name().GetText(),
				Arguments = context._arguments.Select(a => new ExpressionBuilder(parentBlock).Visit(a))
			};
		}

		public override Expression VisitMethodCall(RheaParser.MethodCallContext context)
		{
			var receiver = new ExpressionBuilder(parentBlock).Visit(context.receiver);

			return new MethodCall
			{
				ParentBlock = parentBlock,
				Name = context.methodName.Text,
				Receiver = receiver,
				Arguments = context._arguments.Select(a => new ExpressionBuilder(parentBlock).Visit(a))
			};
		}

		public override Expression VisitUnaryExpression([NotNull] RheaParser.UnaryExpressionContext context)
		{
			Unary node;

			switch (context.op.Type)
			{
				// FIXME: WTF?
				case RheaLexer.OP_ADD:
				case RheaLexer.OP_SUB:
					node = new UnaryNegation();
					break;
				default:
					throw new NotImplementedException();
			}

			node.ParentBlock = parentBlock;
			node.Expression = Visit(context.expression());

			return node;
		}

		public override Expression VisitInfixExpression([NotNull] RheaParser.InfixExpressionContext context)
		{
			Infix node;

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
				case RheaLexer.OP_MOD:
					node = new Modulo();
					break;
				case RheaLexer.OP_LT:
					node = new LessThan();
					break;
				case RheaLexer.OP_LT_EQ:
					node = new LessThanEqual();
					break;
				case RheaLexer.OP_GT:
					node = new GreaterThan();
					break;
				case RheaLexer.OP_GT_EQ:
					node = new GreaterThanEqual();
					break;
				case RheaLexer.OP_EQ:
					node = new Equality();
					break;
				case RheaLexer.OP_NE:
					node = new Inequality();
					break;
				default:
					throw new NotSupportedException();
			}

			node.Left = Visit(context.left);
			node.Left.ParentBlock = parentBlock;

			node.Right = Visit(context.right);
			node.Right.ParentBlock = parentBlock;

			return node;
		}

		public override Expression VisitMemberAccess(RheaParser.MemberAccessContext context)
		{
			return new MemberAccess
			{
				ParentBlock = parentBlock,
				VariableName = context.structName.Text,
				MemberName = context.memberName.Text
			};
		}

		public override Expression VisitStringLiteral(RheaParser.StringLiteralContext context)
		{
			return new StringLiteral
			{
				Context = context,
				ParentBlock = parentBlock,
				Value = context.value.Text.Replace("\"", "")
			};
		}
	}
}