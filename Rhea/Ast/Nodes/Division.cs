namespace Rhea.Ast.Nodes
{
    public class Division : Infix
    {
        public override string ToString()
        {
            return $"({Left} / {Right})";
        }
    }
}