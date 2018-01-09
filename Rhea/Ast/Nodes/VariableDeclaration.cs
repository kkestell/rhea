namespace Rhea.Ast.Nodes
{
    class VariableDeclaration : Statement
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name};";
        }
    }
}
