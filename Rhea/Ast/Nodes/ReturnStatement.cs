namespace Rhea.Ast.Nodes
{
    class ReturnStatement : Statement
    {
        public Expression Expression { get; set; }

        public override string ToString()
        {
            return $"return {Expression};";
        }
    }
}
