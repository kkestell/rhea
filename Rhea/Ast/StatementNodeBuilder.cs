using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class StatementNodeBuilder : RheaBaseListener
    {
        public StatementNode Statement { get; private set; }

        public override void EnterVariableDeclaration(RheaParser.VariableDeclarationContext context)
        {
            Statement = new VariableDeclarationNode
            {
                Name = context.name().GetText(),
                Type = new TypeNode(context.type().GetText())
            };
        }

        public override void EnterVariableInitialization(RheaParser.VariableInitializationContext context)
        {
            var builder = new VariableInitializationNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Statement = builder.VariableInitialization;
        }

        public override void EnterReturnStatement(RheaParser.ReturnStatementContext context)
        {
            var builder = new ReturnStatementNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Statement = builder.ReturnStatement;
        }
    }
}
