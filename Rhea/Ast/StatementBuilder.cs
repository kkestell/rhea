using System.Linq;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class StatementBuilder : RheaBaseVisitor<Statement>
    {
        readonly IScope parentScope;
        readonly Block parentBlock;

        public StatementBuilder(Block parentBlock, IScope parentScope)
        {
            this.parentBlock = parentBlock;
            this.parentScope = parentScope;
        }

        public override Statement VisitAssignment(RheaParser.AssignmentContext context)
        {
            var expression = new ExpressionBuilder(parentBlock).Visit(context.expression());

            return new Assignment
            {
                ParentBlock = parentBlock,
                VariableName = context.variableName.Text,
                Expression = expression
            };
        }

        public override Statement VisitMemberAssignment(RheaParser.MemberAssignmentContext context)
        {
            var expression = new ExpressionBuilder(parentBlock).Visit(context.expression());

            return new MemberAssignment
            {
                ParentBlock = parentBlock,
                VariableName = context.variableName.Text,
                MemberName = context.memberName.Text,
                Expression = expression
            };
        }

        public override Statement VisitVariableDeclaration(RheaParser.VariableDeclarationContext context)
        {
            return new VariableDeclaration
            {
                ParentBlock = parentBlock,
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText())
            };
        }

        public override Statement VisitVariableInitialization(RheaParser.VariableInitializationContext context)
        {
            var expression = new ExpressionBuilder(parentBlock).Visit(context.expression());
            
            return new VariableInitialization
            {
                ParentBlock = parentBlock,
                Name = context.name().GetText(),
                Type = expression.InferredType,
                Expression = expression
            };
        }

        public override Statement VisitReturnStatement(RheaParser.ReturnStatementContext context)
        {
            var returnStatement = new Return
            {
                Context = context,
                ParentBlock = parentBlock
            };

            if (context.expression() != null)
                returnStatement.Expression = new ExpressionBuilder(parentBlock).Visit(context.expression());

            return returnStatement;
        }

        public override Statement VisitIfStatement(RheaParser.IfStatementContext context)
        {
            var newIfStatement = new If
            {
                ParentBlock = parentBlock,
                Expression = new ExpressionBuilder(parentBlock).Visit(context.expression())
            };

            var newBlock = new Block
            {
                ParentBlock = parentBlock,
                ParentScope = parentScope
            };

            newBlock.Statements = context.block()._statements.Select(s => new StatementBuilder(newBlock, newIfStatement.Block).Visit(s));

            newIfStatement.Block = newBlock;

            return newIfStatement;
        }

        public override Statement VisitForRange(RheaParser.ForRangeContext context)
        {
            var range = new Range
            {
                Start = new ExpressionBuilder(parentBlock).Visit(context.range().start),
                End = new ExpressionBuilder(parentBlock).Visit(context.range().end)
            };

            var newForRange = new ForRange
            {
                ParentBlock = parentBlock,
                ParentScope = parentScope,
                Range = range,
                Iterator = new VariableDeclaration
                {
                    Name = context.name().GetText(),
                    ParentBlock = parentBlock,
                    Type = range.InferredType
                }
            };

            var newBlock = new Block
            {
                ParentBlock = parentBlock,
                ParentScope = newForRange
            };

            newBlock.Statements = context.block()._statements.Select(s => new StatementBuilder(newBlock, newForRange.Block).Visit(s));

            newForRange.Block = newBlock;

            return newForRange;
        }
    }
}