using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class UnaryExpressionNode : ExpressionNode
    {
        public ExpressionNode Expression { get; set; }

        public override string ToString()
        {
            return $"({Expression})";
        }
    }
}