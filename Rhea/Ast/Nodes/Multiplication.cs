namespace Rhea.Ast.Nodes
{
    public class Multiplication : InfixExpression
    {
        public override string ToString()
        {
            return $"({Left} * {Right})";
        }
    }
}