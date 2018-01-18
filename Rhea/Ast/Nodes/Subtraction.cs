namespace Rhea.Ast.Nodes
{
    public class Subtraction : Infix
    {
        public override string ToString()
        {
            return $"({Left} - {Right})";
        }
    }
}