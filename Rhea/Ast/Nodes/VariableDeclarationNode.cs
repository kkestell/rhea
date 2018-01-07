namespace Rhea.Ast.Nodes
{
    class VariableDeclarationNode : StatementNode
    {
        public string Name { get; set; }
        public TypeNode Type { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name};";
        }
    }
}
