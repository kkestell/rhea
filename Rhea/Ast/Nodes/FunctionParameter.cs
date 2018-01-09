using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class FunctionParameter : Node
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}