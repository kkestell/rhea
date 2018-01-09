using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class VariableInitializationBuilder : RheaBaseListener
    {
        public VariableInitialization VariableInitialization { get; set; }

        public override void EnterVariableInitialization(RheaParser.VariableInitializationContext context)
        {
            VariableInitialization = new VariableInitialization
            {
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText())
            };
        }

        public override void EnterExpression(RheaParser.ExpressionContext context)
        {
            var builder = new ExpressionBuilder();
            VariableInitialization.Expression = builder.Visit(context);
        }
    }
}
