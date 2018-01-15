namespace Rhea.Ast.Nodes
{
    public class Division : InfixExpression
    {
        public override string ToString()
        {
            return $"({Left} / {Right})";
        }
    }
}