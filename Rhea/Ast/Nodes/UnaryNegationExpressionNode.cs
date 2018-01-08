namespace Rhea.Ast
{
    class UnaryNegationExpressionNode : UnaryExpressionNode
    {
        public override string ToString()
        {
            return $"(-{Expression})";
        }
    }
}