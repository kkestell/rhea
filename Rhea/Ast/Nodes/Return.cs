namespace Rhea.Ast.Nodes
{
    public class Return : Statement
    {
        public Expression Expression { get; set; }

        public override string ToString()
        {
            return $"return {Expression};";
        }
    }
}