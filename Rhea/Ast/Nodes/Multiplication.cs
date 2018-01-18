namespace Rhea.Ast.Nodes
{
    public class Multiplication : Infix
    {
        public override string ToString()
        {
            return $"({Left} * {Right})";
        }
    }
}