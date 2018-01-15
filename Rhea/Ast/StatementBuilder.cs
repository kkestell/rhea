using System.Linq;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    internal class StatementBuilder : RheaBaseVisitor<Statement>
    {
        Block Scope { get; }

        public StatementBuilder(Block scope)
        {
            Scope = scope;
        }

        public override Statement VisitVariableDeclaration(RheaParser.VariableDeclarationContext context)
        {
            return new VariableDeclaration
            {
                Scope = Scope,
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText())
            };
        }

        public override Statement VisitVariableInitialization(RheaParser.VariableInitializationContext context)
        {
            return new VariableInitialization
            {
                Scope = Scope,
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText()),
                Expression = new ExpressionBuilder(Scope).Visit(context.expression())
            };
        }

        public override Statement VisitReturnStatement(RheaParser.ReturnStatementContext context)
        {
            var returnStatement = new ReturnStatement
            {
                Scope = Scope
            };

            if (context.expression() != null)
            {
                returnStatement.Expression = new ExpressionBuilder(Scope).Visit(context.expression());
            }

            return returnStatement;
        }

        public override Statement VisitIfStatement(RheaParser.IfStatementContext context)
        {
            var newBlock = new Block
            {
                Scope = Scope
            };

           newBlock.Statements = context.block()._statements.Select(s => new StatementBuilder(newBlock).Visit(s));
            
            return new IfStatement
            {
                Scope = Scope,
                Expression = new ExpressionBuilder(Scope).Visit(context.expression()),
                Block = newBlock
            };
        }
    }
}