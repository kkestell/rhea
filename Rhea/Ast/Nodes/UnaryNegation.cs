namespace Rhea.Ast
{
    class UnaryNegation : UnaryExpression
    {
        public override string ToString()
        {
            return $"(-{Expression})";
        }
    }
}