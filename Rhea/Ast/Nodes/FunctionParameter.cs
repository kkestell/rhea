namespace Rhea.Ast.Nodes
{
    public class FunctionParameter : Node
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}