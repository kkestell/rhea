using System.Linq;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    internal class StatementBuilder : RheaBaseVisitor<Statement>
    {
        private readonly IScope parent;
        private readonly Block scope;

        public StatementBuilder(Block scope, IScope parent)
        {
            this.scope = scope;
            this.parent = parent;
        }

        public override Statement VisitVariableDeclaration(RheaParser.VariableDeclarationContext context)
        {
            return new VariableDeclaration
            {
                Scope = scope,
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText())
            };
        }

        public override Statement VisitVariableInitialization(RheaParser.VariableInitializationContext context)
        {
            var expression = new ExpressionBuilder(scope).Visit(context.expression());
            
            return new VariableInitialization
            {
                Scope = scope,
                Name = context.name().GetText(),
                Type = expression.InferredType,
                Expression = expression
            };
        }

        public override Statement VisitReturnStatement(RheaParser.ReturnStatementContext context)
        {
            var returnStatement = new Return
            {
                Scope = scope
            };

            if (context.expression() != null)
                returnStatement.Expression = new ExpressionBuilder(scope).Visit(context.expression());

            return returnStatement;
        }

        public override Statement VisitIfStatement(RheaParser.IfStatementContext context)
        {
            var newIfStatement = new If
            {
                Scope = scope,
                Expression = new ExpressionBuilder(scope).Visit(context.expression())
            };

            var newBlock = new Block
            {
                Scope = scope,
                Parent = parent
            };

            newBlock.Statements = context.block()._statements.Select(s => new StatementBuilder(newBlock, newIfStatement.Block).Visit(s));

            newIfStatement.Block = newBlock;

            return newIfStatement;
        }

        public override Statement VisitForRange(RheaParser.ForRangeContext context)
        {
            var range = new Range
            {
                Start = new ExpressionBuilder(scope).Visit(context.range().start),
                End = new ExpressionBuilder(scope).Visit(context.range().end)
            };

            var newForRange = new ForRange
            {
                Scope = scope,
                Range = range,
                Iterator = new VariableDeclaration
                {
                    Name = context.name().GetText(),
                    Scope = scope,
                    Type = range.InferredType
                },
                Parent = parent
            };

            var newBlock = new Block
            {
                Scope = scope,
                Parent = newForRange
            };

            newBlock.Statements = context.block()._statements.Select(s => new StatementBuilder(newBlock, newForRange.Block).Visit(s));

            newForRange.Block = newBlock;

            return newForRange;
        }
    }
}