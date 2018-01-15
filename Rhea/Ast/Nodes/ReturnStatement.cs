namespace Rhea.Ast.Nodes
{
    public class ReturnStatement : Statement
    {
        public Expression Expression { get; set; }

        public override string ToString()
        {
            return $"return {Expression};";
        }
    }
}