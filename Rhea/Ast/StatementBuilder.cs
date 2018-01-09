using System.Linq;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class StatementBuilder : RheaBaseVisitor<Statement>
    {
        public override Statement VisitVariableDeclaration(RheaParser.VariableDeclarationContext context)
        {
            return new VariableDeclaration
            {
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText())
            };
        }

        public override Statement VisitVariableInitialization(RheaParser.VariableInitializationContext context)
        {
            return new VariableInitialization
            {
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText()),
                Expression = new ExpressionBuilder().Visit(context.expression())
            };
        }

        public override Statement VisitReturnStatement(RheaParser.ReturnStatementContext context)
        {
            return new ReturnStatement
            {
                Expression = new ExpressionBuilder().Visit(context.expression())
            };
        }

        public override Statement VisitIfStatement(RheaParser.IfStatementContext context)
        {
            return new IfStatement
            {
                Expression = new ExpressionBuilder().Visit(context.expression()),
                Block = new Block
                {
                    Statements = context.block()._statements.Select(s => new StatementBuilder().Visit(s))
                }
            };
        }
    }
}
