namespace Rhea.Ast.Nodes
{
    public class Subtraction : InfixExpression
    {
        public override string ToString()
        {
            return $"({Left} - {Right})";
        }
    }
}