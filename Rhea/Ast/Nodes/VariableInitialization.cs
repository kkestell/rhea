namespace Rhea.Ast.Nodes
{
    class VariableInitialization : Statement
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public Expression Expression { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name} = {Expression};";
        }
    }
}
