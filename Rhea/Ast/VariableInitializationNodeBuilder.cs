using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class VariableInitializationNodeBuilder : RheaBaseListener
    {
        public VariableInitializationNode VariableInitialization { get; set; }

        public override void EnterVariableInitialization(RheaParser.VariableInitializationContext context)
        {
            VariableInitialization = new VariableInitializationNode
            {
                Name = context.name().GetText(),
                Type = new TypeNode(context.type().GetText())
            };
        }

        public override void EnterExpression(RheaParser.ExpressionContext context)
        {
            var builder = new ExpressionNodeBuilder();
            VariableInitialization.Expression = builder.Visit(context);
        }
    }
}
