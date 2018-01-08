using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class StatementNodeBuilder : RheaBaseVisitor<StatementNode>
    {
        public override StatementNode VisitVariableDeclaration(RheaParser.VariableDeclarationContext context)
        {
            return new VariableDeclarationNode
            {
                Name = context.name().GetText(),
                Type = new TypeNode(context.type().GetText())
            };
        }

        public override StatementNode VisitVariableInitialization(RheaParser.VariableInitializationContext context)
        {
            return new VariableInitializationNode
            {
                Name = context.name().GetText(),
                Type = new TypeNode(context.type().GetText()),
                Expression = new ExpressionNodeBuilder().Visit(context.expression())
            };
        }

        public override StatementNode VisitReturnStatement(RheaParser.ReturnStatementContext context)
        {
            return new ReturnStatementNode
            {
                Expression = new ExpressionNodeBuilder().Visit(context.expression())
            };
        }
    }
}
