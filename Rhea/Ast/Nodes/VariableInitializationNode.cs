namespace Rhea.Ast.Nodes
{
    class VariableInitializationNode : StatementNode
    {
        public string Name { get; set; }
        public TypeNode Type { get; set; }
        public ExpressionNode Expression { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name} = {Expression};";
        }
    }
}
