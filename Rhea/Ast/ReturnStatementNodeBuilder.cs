using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class ReturnStatementNodeBuilder : RheaBaseListener
    {
        public ReturnStatementNode ReturnStatement { get; private set; }

        public override void EnterReturnStatement(RheaParser.ReturnStatementContext context)
        {
            ReturnStatement = new ReturnStatementNode();
        }

        public override void EnterExpression(RheaParser.ExpressionContext context)
        {
            var builder = new ExpressionNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            ReturnStatement.Expression = builder.Expression;
        }
    }
}
